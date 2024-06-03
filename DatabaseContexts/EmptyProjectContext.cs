using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration;
using EmptyProject.Areas.BasicCore.Entities;
using EmptyProject.Areas.EmptyProject.Entities;
using EmptyProject.Areas.EmptyProject.EntitiesConfiguration;
using EmptyProject.Areas.BasicCore.EntitiesConfiguration;
using EmptyProject.Areas.CMSCore.EntitiesConfiguration;

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
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientStatus> ClientStatus { get; set; }

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
                modelBuilder.ApplyConfiguration(new RoleConfiguration());
                modelBuilder.Entity<Role>().ToTable("CMSCore.Role");
                modelBuilder.ApplyConfiguration(new MenuConfiguration());
                modelBuilder.Entity<Menu>().ToTable("CMSCore.Menu");
                modelBuilder.ApplyConfiguration(new RoleMenuConfiguration());
                modelBuilder.ApplyConfiguration(new FailureConfiguration());
                modelBuilder.Entity<Failure>().ToTable("BasicCore.Failure");
                modelBuilder.ApplyConfiguration(new ParameterConfiguration());
                modelBuilder.Entity<Parameter>().ToTable("BasicCore.Parameter");

                //EmptyProject
                modelBuilder.ApplyConfiguration(new ClientConfiguration());
                modelBuilder.Entity<Client>().ToTable("EmptyProject.Client");
                modelBuilder.ApplyConfiguration(new ClientStatusConfiguration());
                modelBuilder.Entity<ClientStatus>().ToTable("EmptyProject.ClientStatus");
            }
            catch (Exception) { throw; }
        }
    }
}
