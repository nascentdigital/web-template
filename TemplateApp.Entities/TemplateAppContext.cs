using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace TemplateApp.Entities
{
    public class TemplateAppContext : DbContext
    {
        #region constructors

        public TemplateAppContext()
        {
            // set timeout
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = 300;

            // configure context
            DbContextConfiguration configuration = Configuration;
            configuration.LazyLoadingEnabled = false;
        }

        #endregion


        #region properties

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        #endregion


        #region overridden methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // call base implementation
            base.OnModelCreating(modelBuilder);

            // setup general conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // configure user
            EntityTypeConfiguration<User> userConfig =
                modelBuilder.Entity<User>();
            userConfig.Property(e => e.Email)
                .IsRequired();
            userConfig.Property(e => e.FirstName)
                .IsRequired();
            userConfig.Property(e => e.LastName)
                .IsRequired();
            userConfig.Ignore(e => e.Name);

            // configure project
            EntityTypeConfiguration<Project> projectConfig =
                modelBuilder.Entity<Project>();
            projectConfig.Property(e => e.Name)
                .IsRequired();
            projectConfig.Property(e => e.Description)
                .IsRequired();
        }

        #endregion

    }
}
