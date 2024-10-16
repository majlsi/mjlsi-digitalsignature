using System.IO;
using Ghostscript.NET;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections.Generic;
using Models.DTO;
using System.Linq;
using RestSharp;
using Newtonsoft.Json;
using System;

namespace Helpers
{
    public class PdfHelper
    {
        public int splitDocumentToImages(string documentPrefix, string pdfDocumentUrl, string webRootPath, string SplitAPIURL, string SplitActionName)
        {
            string folderName = "uploads";
            string documentFolderName = "docs";
            string documentsFolderUrl = Path.Combine(folderName, documentFolderName);
            string uploadsPath = Path.Combine(webRootPath, documentsFolderUrl);
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }
            string documentUploadsPath = Path.Combine(uploadsPath, "doc_" + documentPrefix);
            if (!Directory.Exists(documentUploadsPath))
            {
                Directory.CreateDirectory(documentUploadsPath);
            }


            string pdfPath = Path.Combine(webRootPath, pdfDocumentUrl);
            List<string> urls = SplitPDF(pdfPath, SplitAPIURL, SplitActionName);
            for (int u = 1; u <= urls.Count; u++)
            {
                var req = System.Net.WebRequest.Create(urls[u - 1]);
                using (Stream imgStream = req.GetResponse().GetResponseStream())
                {
                    string imageFileName = "pdf_page_" + u.ToString() + ".jpeg";
                    string imagePath = Path.Combine(documentUploadsPath, imageFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imgStream.CopyTo(stream);
                    }
                }
            }


            return urls.Count;
        }

        

        public List<string> SplitPDF(string pdfPath, string SplitAPIURL, string SplitActionName)
        {
            //Create Rest Client
            RestClient client = new RestClient(SplitAPIURL);

            var restRequest = new RestRequest("/" + SplitActionName, Method.Post);

            restRequest.AddHeader("Content-Type", "multipart/form-data");

            //Convert File to Byte Array
            byte[] byteArray1 = File.ReadAllBytes(pdfPath);

            //Add Files in Rest Request
            restRequest.AddFile("file", byteArray1.ToArray(), "custom");

            RestResponse response = client.Execute(restRequest);

            SplitPDFResponse content = JsonConvert.DeserializeObject<SplitPDFResponse>(response.Content);
            return content.urls;
        }

        private byte[] file_get_byte_contents(string pdfPath)
        {
            byte[] sContents;
            // Get file size
            FileInfo fi = new FileInfo(pdfPath);
            // Disk
            using (FileStream fs = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    sContents = br.ReadBytes((int)fi.Length);
                    br.Close();
                }
                fs.Close();
            }

            return sContents;
        }


    }

}

