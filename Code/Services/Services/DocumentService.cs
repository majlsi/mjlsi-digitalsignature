
using Connectors;
using Data.Infrastructure;
using Data.Repositories;
using Helpers;
using iTextSharp.text.pdf;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Document = Models.Document;

namespace Services
{

    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository DocumentRepository;
        private readonly IDocumentPageRepository DocumentPageRepository;
        private readonly IDocumentFieldRepository DocumentFieldRepository;
        private readonly IDocumentUserRepository DocumentUserRepository;
        private readonly IFieldTypeRepository FieldTypeRepository;
        private readonly IApplicationUserRepository ApplicationUserRepository;


        private readonly IUnitOfWork unitOfWork;
        private readonly ManageResources _manageRes;
        private readonly IUserRepository UserRepository;
        private PdfHelper _pdfHelper;
        private ControlsHelper _controlsHelper;
        private SecurityHelper _securityHelper;
        public DocumentService(IDocumentRepository DocumentRepository, IUserRepository UserRepository, IFieldTypeRepository FieldTypeRepository, IUnitOfWork unitOfWork, IDocumentPageRepository DocumentPageRepository,
            PdfHelper pdfHelper, ControlsHelper controlsHelper, SecurityHelper securityHelper, IDocumentFieldRepository DocumentFieldRepository, IDocumentUserRepository DocumentUserRepository, IApplicationUserRepository ApplicationUserRepository)
        {
            this.DocumentRepository = DocumentRepository;
            this.unitOfWork = unitOfWork;
            this.DocumentPageRepository = DocumentPageRepository;
            this.DocumentUserRepository = DocumentUserRepository;
            this.FieldTypeRepository = FieldTypeRepository;
            this.DocumentFieldRepository = DocumentFieldRepository;
            this.UserRepository = UserRepository;
            this.ApplicationUserRepository = ApplicationUserRepository;
            _controlsHelper = controlsHelper;
            _pdfHelper = pdfHelper;
            _securityHelper = securityHelper;
            _manageRes = new ManageResources();
        }

        #region IApplicationService Members


        public Document GetDocument(int id)
        {
            var Document = DocumentRepository.GetById(id);
            return Document;
        }

        public void CreateDocument(Document Document)
        {
            Document.Application = null;
            Document.DocumentPages = null;
            DocumentRepository.Add(Document);

        }
        public List<Document> GetAll()
        {
            List<Document> Documents = DocumentRepository.GetAll().ToList();
            return Documents;
        }
        public void UpdateDocument(Document document)
        {
            document.Application = null;
            document.DocumentPages = null;
            DocumentRepository.Update(document.DocumentID, document);
        }

        public void DeleteDocument(Document document)
        {
            DocumentRepository.Delete(document);
        }
        public List<DocumentPageDTO> GetDocumentPages(int documentID, string LangCode, int? currentUserID, int timeZone)
        {
            List<DocumentPageDTO> documentPages = DocumentPageRepository.GetAllByDocumentID(documentID, currentUserID);
            int userId = _securityHelper.getUserIDFromToken();
            for (int i = 0; i < documentPages.Count(); i++)
            {
                documentPages[i].DocumentFields = documentPages[i].DocumentFields
               .ToList();
                for (int j = 0; j < documentPages[i].DocumentFields.Count(); j++)
                {
                    documentPages[i].DocumentFields[j].DocumentFieldHtml = _controlsHelper.getFieldHtml(userId, documentPages[i].DocumentFields[j], timeZone, LangCode);
                }
            }
            return documentPages;
        }
        public Document UpdateDocumentWithFields(string documentPrefix, DocumentDTO documentDTO, string documentsPath, string webRootPath, string SplitAPIURL, string SplitActionName)
        {

            Document document = DocumentRepository.GetById(documentDTO.DocumentID);
            document.OriginalDocumentUrl = documentDTO.OriginalDocumentUrl;
            DocumentRepository.Update(document.DocumentID, document);
            SaveDocument();
            List<DocumentPageDTO> documentPages = GetDocumentPagesByAdmin(documentDTO.DocumentID);
            string documentPath = Path.Combine(documentsPath, "doc_" + documentPrefix);


            //delete prevous document pages  and document fields 
            foreach (DocumentPageDTO documentPage in documentPages)
            {

                List<int> documentFieldIDs = documentPage.DocumentFields.Select(d => d.DocumentFieldID).ToList();
                DocumentFieldRepository.Delete(d => d.DocumentPageID.Equals(documentPage.DocumentPageID));


            }

            DocumentPageRepository.Delete(ddd => ddd.DocumentID.Equals(document.DocumentID));
            SaveDocument();
            int PagesCount = _pdfHelper.splitDocumentToImages(documentPrefix, document.OriginalDocumentUrl, webRootPath, SplitAPIURL, SplitActionName);
            // add document pages
            for (int i = 1; i <= PagesCount; i++)
            {
                DocumentPage documentPage = new DocumentPage();
                documentPage.PageNumber = i;
                string imageFileName = "pdf_page_" + i.ToString() + ".jpeg";
                documentPage.DocumentPageUrl = Path.Combine(documentPath, imageFileName);
                documentPage.DocumentID = document.DocumentID;
                DocumentPageRepository.Add(documentPage);

            }
            SaveDocument();
            DocumentUserRepository.Delete(du => du.DocumentID.Equals(document.DocumentID));
            // add document fields for document pages 
            for (int d = 0; d < documentDTO.DocumentFields.Count; d++)
            {
                DocumentUser documentUser = new DocumentUser();
                User user = UserRepository.GetUser(documentDTO.DocumentFields[d].DocumentFieldUserEmail, document.ApplicationID);
                if (user != null)
                {
                    documentUser.UserID = user.UserID;
                    documentUser.DocumentID = document.DocumentID;
                    documentUser.User = null;
                    documentUser.Document = null;
                    DocumentUserRepository.Add(documentUser);

                }
                SaveDocument();
                DocumentField documentField = new DocumentField();
                documentField.DocumentFieldValue = documentDTO.DocumentFields[d].DocumentFieldValue;
                documentField.FieldTypeID = documentDTO.DocumentFields[d].FieldTypeID;
                documentField.UserID = user.UserID;
                documentField.XPosition = documentDTO.DocumentFields[d].XPosition;
                documentField.YPosition = documentDTO.DocumentFields[d].YPosition;
                documentField.DocumentPage = null;
                documentField.FieldType = null;
                documentField.User = null;
                documentField.DocumentPageID = DocumentPageRepository.GetDocumentPageByPageNumber(document.DocumentID, documentDTO.DocumentFields[d].PageNumber).DocumentPageID;
                DocumentFieldRepository.Add(documentField);




            }
            return document;
        }
        public async Task<ErrorDTO> ValidateDocument(DocumentDTO documentDTO, string root, string oraginalDoc)
        {

            ErrorDTO error = new ErrorDTO();

            if (documentDTO.DocumentID != 0)// EDIT MODE
            {
                Document document = DocumentRepository.GetById(documentDTO.DocumentID);
                if (document == null)
                {
                    error = _manageRes.GetErrorByName("FileNotExist");
                    return error;
                }
                List<DocumentPageDTO> documentPages = GetDocumentPagesByAdmin(documentDTO.DocumentID);

                foreach (DocumentPageDTO documentPage in documentPages) // validate there is no signed or rejected documentFields yet
                {
                    List<DocumentFieldDTO> SignedDocumentFields = documentPage.DocumentFields.Where(d => d.DocumentFieldValue != null).ToList();
                    if (SignedDocumentFields.Count != 0)
                    {
                        error = _manageRes.GetErrorByName("AlreadySigned");
                        return error;
                    }
                }
            }

            IStorageFactory file = StorageFactory.CreateFileConnetor();
            bool isExists = await file.CheckFileExists(root, oraginalDoc);
            // check file exists
            if (!isExists)
            {
                error = _manageRes.GetErrorByName("FileNotExist");
                return error;
            }
            string extension = Path.GetExtension(oraginalDoc);
            //check file type 
            if (extension != ".pdf")
            {
                error = _manageRes.GetErrorByName("FilePDF");
                return error;
            }
            //check all field types exist
            List<int> fieldtypeIDs = (from fieldtypes in documentDTO.DocumentFields select fieldtypes.FieldTypeID).Distinct().ToList();
            bool allFieldTypesExist = FieldTypeRepository.AllFieldTypesExist(fieldtypeIDs);
            if (!allFieldTypesExist)
            {
                error = _manageRes.GetErrorByName("FieldTypesNotFound");
                return error;
            }
            //check all user emails exist
            //bool allUserEmailsExist = UserRepository.AllUserEmailsExist((from documentfield in documentDTO.DocumentFields select documentfield.DocumentFieldUserEmail).Distinct().ToList());
            //if (!allUserEmailsExist)
            //{
            //    error = _manageRes.GetErrorByName("UserEmailsNotFound");
            //    return error;
            //}
            return error;
        }
        public List<DocumentPageDTO> GetDocumentPagesByAdmin(int documentID)
        {
            List<DocumentPageDTO> documentPages = DocumentPageRepository.GetAllByDocumentID(documentID, null);

            for (int i = 0; i < documentPages.Count(); i++)
            {
                documentPages[i].DocumentFields = documentPages[i].DocumentFields.ToList();

            }
            return documentPages;
        }
        public Document CreateDocumentPagesWithFields(string documentPrefix, DocumentDTO documentDTO, Document document, string documentsPath, string webRootPath, string SplitAPIURL, string SplitActionName)
        {
            document.Application = null;
            string documentPath = Path.Combine(documentsPath, "doc_" + documentPrefix);
            int PagesCount = _pdfHelper.splitDocumentToImages(documentPrefix, document.OriginalDocumentUrl, webRootPath, SplitAPIURL, SplitActionName);
            // add document pages
            MapDocumentPages(document, document, documentPath, PagesCount, documentPrefix);
            MapDocumentFields(documentDTO, document, document);
            DocumentRepository.Add(document);
            return document;
        }
        public async Task<Document> CreateDocumentPagesFromImagesWithFields(string documentPrefix, DocumentDTO documentDTO, Document document, string documentsPath, string webRootPath)
        {
            document.Application = null;
            int PagesCount = 0;

            string documentPath = Path.Combine(documentsPath, "doc_" + documentPrefix);
            string fullPath = Path.Combine(webRootPath, documentPath);
            IStorageFactory fileConnector = StorageFactory.CreateFileConnetor();
            PagesCount = await fileConnector.DirectoryFilesAsync(documentPath, fullPath);
            // add document pages
            MapDocumentPages(document, document, documentPath, PagesCount, documentPrefix);
            MapDocumentFields(documentDTO, document, document);
            DocumentRepository.Add(document);
            return document;
        }


        private void MapDocumentFields(DocumentDTO documentDTO, Document document, Document createdDocument)
        {
            int MaxPageNumber = document.DocumentPages.Count;
            int signPages = 0;
            if (documentDTO.DocumentFields.Count > 8)
            {
                signPages = Convert.ToInt32((documentDTO.DocumentFields.Count / 8));
                int reminder = documentDTO.DocumentFields.Count % 8;
                if (reminder > 0)
                {
                    signPages = signPages + 1;
                }
            }
            // add document fields for document pages 
            for (int d = 0; d < documentDTO.DocumentFields.Count; d++)
            {
                DocumentField documentField = new DocumentField();
                DocumentUser documentUser = new DocumentUser();
                User user = UserRepository.GetUser(documentDTO.DocumentFields[d].DocumentFieldUserEmail, document.ApplicationID);

                if (user == null) //create user if not found
                {
                    user = new User();
                    user.UserEmail = documentDTO.DocumentFields[d].DocumentFieldUserEmail;
                    user.UserName = documentDTO.DocumentFields[d].DocumentFieldUserEmail;
                    user.UserPhoneNumber = documentDTO.DocumentFields[d].DocumentFieldUserPhoneNumber;
                    user.UserPassword = _securityHelper.Md5Encryption(ConfigurationHelper.NewUserDefaultPassword);
                    user.FullName = documentDTO.DocumentFields[d].DocumentFieldUserEmail;
                    UserRepository.Add(user);
                    SaveDocument();
                    ApplicationUser applicationUser = new ApplicationUser();
                    applicationUser.UserID = user.UserID;
                    applicationUser.ApplicationID = document.ApplicationID;
                    ApplicationUserRepository.Add(applicationUser);
                    SaveDocument();
                }
                else
                {
                    user.UserEmail = documentDTO.DocumentFields[d].DocumentFieldUserEmail;
                    user.UserName = documentDTO.DocumentFields[d].DocumentFieldUserEmail;
                    user.UserPhoneNumber = documentDTO.DocumentFields[d].DocumentFieldUserPhoneNumber;
                    user.UserPassword = _securityHelper.Md5Encryption(ConfigurationHelper.NewUserDefaultPassword);
                    user.FullName = documentDTO.DocumentFields[d].DocumentFieldUserEmail;
                    UserRepository.Update(user.UserID, user);
                    SaveDocument();
                }
                documentUser.UserID = user.UserID;
                documentUser.DocumentID = document.DocumentID;
                documentUser.User = null;
                documentUser.Document = null;
                createdDocument.DocumentUsers.Add(documentUser);
                documentField.UserID = user.UserID;


                documentField.DocumentFieldValue = documentDTO.DocumentFields[d].DocumentFieldValue;
                documentField.FieldTypeID = documentDTO.DocumentFields[d].FieldTypeID;
                documentField.XPosition = documentDTO.DocumentFields[d].XPosition;
                documentField.YPosition = documentDTO.DocumentFields[d].YPosition;
                documentField.DocumentPage = null;
                documentField.FieldType = null;
                if (!documentDTO.IsApproval)
                {
                    //more than one sign page
                    if (signPages > 0)
                    {

                        int originalPages = MaxPageNumber - signPages;
                        int pageReminder = d / 8;
                        if (pageReminder > 0)
                        {
                            int pageNumber = originalPages + pageReminder + 1;
                            createdDocument.DocumentPages.Where(dp => dp.PageNumber.Equals(pageNumber)).FirstOrDefault().DocumentFields.Add(documentField);
                        }
                        else
                        {
                            int pageNumber = originalPages + 1;
                            createdDocument.DocumentPages.Where(dp => dp.PageNumber.Equals(pageNumber)).FirstOrDefault().DocumentFields.Add(documentField);
                        }
                    }
                    //1 sign page .. place all sign fields in last page
                    else
                    {
                        createdDocument.DocumentPages.Where(dp => dp.PageNumber.Equals(MaxPageNumber)).FirstOrDefault().DocumentFields.Add(documentField);
                    }
                }
                else
                {
                    createdDocument.DocumentPages.Where(dp => dp.PageNumber.Equals(documentDTO.DocumentFields[d].PageNumber)).FirstOrDefault().DocumentFields.Add(documentField);
                }


            }
        }

        private void MapDocumentPages(Document document, Document createdDocument, string documentPath, int PagesCount, string documentPrefix)
        {
            for (int i = 1; i <= PagesCount; i++)
            {
                DocumentPage documentPage = new DocumentPage();
                documentPage.PageNumber = i;
                string imageFileName = "pdf_page_" + i.ToString() + ".jpeg";
                documentPage.DocumentPageUrl = Path.Combine(documentPath, imageFileName);
                createdDocument.DocumentPages.Add(documentPage);
            }
        }

        public void SaveDocument()
        {
            unitOfWork.Commit();
        }


        public async Task<MemoryStream> insertSignaturesToDocument(string webRootPath, Document inputDocument, List<DocumentPageDTO> documentPages, int timeZone)
        {
            var memory = new MemoryStream();
            List<DocumentFieldDTO> SignedDocumentFields = new List<DocumentFieldDTO>();
            foreach (DocumentPageDTO documentPage in documentPages)
            {
                if (inputDocument.IsApproval)
                {
                    SignedDocumentFields.AddRange(documentPage.DocumentFields.Where(d => d.DocumentFieldValue != null && d.DocumentFieldValue != "false").ToList());
                }
                else
                {
                    SignedDocumentFields = documentPage.DocumentFields.Where(d => d.DocumentFieldValue != null && d.DocumentFieldValue != "false").ToList();
                }
            }

            string newPdfFileName = Path.Combine("uploads", "document_" + inputDocument.DocumentID.ToString() + ".pdf");
            string newPdfFilePath = Path.Combine(webRootPath, newPdfFileName);
            IStorageFactory fileConnector = StorageFactory.CreateFileConnetor();
            using (Stream inputPdfStream = await fileConnector.GetFile(webRootPath, inputDocument.OriginalDocumentUrl))
            {

                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(inputPdfStream);
                var stamper = new iTextSharp.text.pdf.PdfStamper(reader, memory);
                foreach (DocumentFieldDTO SignedDocumentField in SignedDocumentFields)
                {
                    List<DocumentPageDTO> currentDocumentPage = documentPages.Where(d => d.DocumentPageID.Equals(SignedDocumentField.DocumentPageID)).ToList();
                    int pageNumber = currentDocumentPage[0].PageNumber;
                    var pdfContentByte = stamper.GetOverContent(pageNumber);
                    iTextSharp.text.Rectangle rectangle = reader.GetPageSizeWithRotation(pageNumber);
                    if (!string.IsNullOrEmpty(SignedDocumentField.DocumentFieldValue))
                    {
                        string img_url = SignedDocumentField.DocumentFieldValue.Split("base64,", StringSplitOptions.RemoveEmptyEntries)[1];
                        byte[] bytes = Convert.FromBase64String(img_url);
                        iTextSharp.text.Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = iTextSharp.text.Image.GetInstance(ms);
                        }
                        //image.ScaleAbsolute((float)(rectangle.Width * 0.40), (float)(rectangle.Height * 0.06));
                        double multipier = 0.01;
                        int x = SignedDocumentField.XPosition + 14;
                        int y = SignedDocumentField.YPosition + 4;
                        image.SetAbsolutePosition((float)(rectangle.Width * multipier * x), (float)(rectangle.Height * (1 - (multipier * y))));
                        pdfContentByte.AddImage(image);
                        //imageDate.ScaleAbsolute(200, 50);
                        float xDate = (float)(rectangle.Width * multipier * (x + 10));
                        float yDate = (float)(rectangle.Height * (1 - (multipier * (y + 2))));
                        pdfContentByte.BeginText();
                        BaseFont bfTimesRoman = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                        pdfContentByte.SetFontAndSize(bfTimesRoman, 10);
                        string signDate = SignedDocumentField.SignDate.HasValue ? SignedDocumentField.SignDate.Value.AddHours(timeZone).ToString() : DateTime.UtcNow.AddHours(timeZone).ToString();
                        pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, signDate, xDate, yDate, 0);
                        pdfContentByte.EndText();
                    }
                }
                stamper.Close();

            }
            memory.Position = 0;
            return memory;

        }
        #endregion
    }
}
