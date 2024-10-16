using Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DBEntities dbContext;
        private readonly IConfiguration Config;
        private readonly SecurityHelper _security;

        public DbFactory(IConfiguration configuration,SecurityHelper security)
        {
            Config = configuration;
            _security = security;
        }
        public DBEntities Init()
        {
            return dbContext ?? (dbContext = CreateDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        public DBEntities CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBEntities>();
            string connectionString = Config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            int? CompanyID = _security.getCompanyIDFromToken();
            if (CompanyID.HasValue)
            {
                DBEntities db= new DBEntities(optionsBuilder.Options);
                db._companyId = CompanyID.Value;
                return db;
            }
            else
            {
                return new DBEntities(optionsBuilder.Options);
            }
        }
    }
}
