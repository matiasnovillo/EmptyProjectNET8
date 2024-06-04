using EmptyProject.Areas.CMS.MenuBack.Entities;
using EmptyProject.Areas.CMS.MenuBack.EntitiesConfiguration;
using EmptyProject.Areas.CMS.RoleBack.Entities;
using EmptyProject.Areas.CMS.RoleBack.EntitiesConfiguration;
using EmptyProject.Areas.CMS.RoleMenuBack.Entities;
using EmptyProject.Areas.CMS.RoleMenuBack.EntitiesConfiguration;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.CMS.UserBack.EntitiesConfiguration;
using EmptyProject.Areas.System.FailureBack.Entities;
using EmptyProject.Areas.System.FailureBack.EntitiesConfiguration;
using EmptyProject.Areas.System.ParameterBack.Entities;
using EmptyProject.Areas.System.ParameterBack.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.DatabaseContexts
{
    public class EmptyProjectContext : DbContext
    {
        protected IConfiguration _configuration { get; }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }
        public DbSet<Failure> Failure { get; set; }
        public DbSet<Parameter> Parameter { get; set; }

        //EmptyProject

        public EmptyProjectContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string ConnectionString = "";
#if DEBUG
                ConnectionString = "data source =.; " +
                    "initial catalog = EmptyProject; " +
                    "Integrated Security = SSPI;" +
                    " MultipleActiveResultSets=True;" +
                    "Pooling=false;" +
                    "Persist Security Info=True;" +
                    "App=EntityFramework;" +
                    "TrustServerCertificate=True;";
#else
                ConnectionString = "Password=!a8248Sjt;" +
                    "Persist Security Info=True;" +
                    "User ID=fiyista1_EmptyProjectAdmin;" +
                    "Initial Catalog=fiyista1_EmptyProject;" +
                    "Data Source=sql4.baehost.com;" +
                    "TrustServerCertificate=True";
#endif

                optionsBuilder
                    .UseSqlServer(ConnectionString);
            }
            catch (Exception) { throw; }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new UserConfiguration());
                modelBuilder.Entity<User>().ToTable("CMS.User");
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
                modelBuilder.Entity<Role>().ToTable("CMS.Role");
                modelBuilder.ApplyConfiguration(new MenuConfiguration());
                modelBuilder.Entity<Menu>().ToTable("CMS.Menu");
                modelBuilder.ApplyConfiguration(new RoleMenuConfiguration());
                modelBuilder.Entity<RoleMenu>().ToTable("CMS.RoleMenu");
                modelBuilder.ApplyConfiguration(new FailureConfiguration());
                modelBuilder.Entity<Failure>().ToTable("System.Failure");
                modelBuilder.ApplyConfiguration(new ParameterConfiguration());
                modelBuilder.Entity<Parameter>().ToTable("System.Parameter");

                //EmptyProject
            }
            catch (Exception) { throw; }
        }
    }
}
