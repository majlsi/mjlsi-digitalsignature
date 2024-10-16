using Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Linq;

namespace Data
{
    public class DBEntities : DbContext
    {

        public DBEntities(DbContextOptions<DBEntities> options)
: base(options)
        {

            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this._companyId = null;
        }
        public  int? _companyId;

        public DbSet<User> Users { get; set; }

        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentSignatureCode> DocumentSignatureCodes { get; set; }
        public DbSet<DocumentField> DocumentFields { get; set; }
        public DbSet<DocumentUser> DocumentUsers { get; set; }
        public DbSet<DocumentUserAction> DocumentUserActions { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<DocumentPage> DocumentPages { get; set; }
       
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //notifications
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<NotificationAction> NotificationActions { get; set; }
        public DbSet<NotificationSetting> NotificationSettings { get; set; }

         public DbSet<SignatureType> SignatureTypes { get; set; }

         public DbSet<UserSignature> UserSignatures { get; set; }

        public void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            //Disable Cascade delete
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.ApplyConfiguration(new ActionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FieldTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentFieldConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentUserConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentUserActionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentPageConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentSignatureCodeConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            //notifications
            modelBuilder.ApplyConfiguration(new NotificationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationActionConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationSettingConfiguration());
            modelBuilder.ApplyConfiguration(new SignatureTypeConfiguartion());
            modelBuilder.ApplyConfiguration(new UserSignatureConfiguration());
        }
    }
}
