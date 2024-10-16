

using Models;
using System.Collections.Generic;

namespace Services
{

    public interface IApplicationService
    {
        Application GetApplication(int id);
        void CreateApplication(Application Application);
        void UpdateApplication(Application application);
        List<Application> GetAll();

        public void DeleteApplication(Application application);
        Application ApplicationLogin(string applicationEmail, string applicationPassword);
        void SaveApplication();
    }
}
