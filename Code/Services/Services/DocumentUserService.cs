
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Microsoft.Extensions.Configuration;
using System;

namespace Services
{

    public class DocumentUserService : IDocumentUserService
    {
        private readonly IDocumentUserRepository DocumentUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _configuration;

        public DocumentUserService(
            IDocumentUserRepository DocumentUserRepository,
            IUnitOfWork unitOfWork,
            IConfiguration configuration
            )
        {
            this.DocumentUserRepository = DocumentUserRepository;
            this.unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        #region IApplicationService Members


        public DocumentUser GetDocumentUser(int id)
        {
            var DocumentUser = DocumentUserRepository.GetById(id);
            return DocumentUser;
        }

        public void CreateDocumentUser(DocumentUser DocumentUser)
        {
            DocumentUser.User = null;
            DocumentUser.Document = null;
            DocumentUserRepository.Add(DocumentUser);
        }
        public List<DocumentUser> GetAll()
        {
            List<DocumentUser> DocumentUsers = DocumentUserRepository.GetAll().ToList();
            return DocumentUsers;
        }
        public void UpdateDocumentUser(DocumentUser documentUser)
        {
            DocumentUserRepository.Update(documentUser.DocumentUserID, documentUser);
        }

        public void DeleteDocumentUser(DocumentUser documentUser)
        {
            DocumentUserRepository.Delete(documentUser);
        }
        public void SaveDocumentUser()
        {
            unitOfWork.Commit();
        }

		public DocumentUser GetDocumentUser(int UserID, int DocumentID)
		{
            return DocumentUserRepository.Get(du => du.UserID == UserID && du.DocumentID == DocumentID);
		}

		#endregion
	}
}
