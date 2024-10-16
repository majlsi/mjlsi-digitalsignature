
using Data.Infrastructure;
using Data.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository ApplicationRepository;
        private readonly IUnitOfWork unitOfWork;

        public ApplicationService(IApplicationRepository ApplicationRepository, IUnitOfWork unitOfWork)
        {
            this.ApplicationRepository = ApplicationRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IApplicationService Members


        public Application GetApplication(int id)
        {
            var Application = ApplicationRepository.GetById(id);
            return Application;
        }

        public void CreateApplication(Application Application)
        {
            ApplicationRepository.Add(Application);
        }
        public List<Application> GetAll()
        {
            List<Application> Applications = ApplicationRepository.GetAll().ToList();
            return Applications;
        }
        public void UpdateApplication(Application application)
        {
            ApplicationRepository.Update(application.ApplicationID, application);
        }

        public void DeleteApplication(Application application)
        {
            ApplicationRepository.Delete(application);
        }
        public Application ApplicationLogin(string applicationEmail, string applicationPassword)
        {
            Application application = ApplicationRepository.ApplicationLogin(applicationEmail, applicationPassword);
            return application;
        }

        public void SaveApplication()
        {
            unitOfWork.Commit();
        }



        #endregion
    }
}
