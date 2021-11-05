using Microsoft.EntityFrameworkCore;
using Studienuebersicht.Model;

namespace Studienuebersicht.EFCore
{
    public class StudienuebersichtDbContext : DbContext
    {
        private string connection_string;
        public DbSet<Module> Modules { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public StudienuebersichtDbContext()
        {
            connection_string = string.Format("Host={0};Username={1};Password={2};Database={3}",
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_HOST", "localhost"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_USER", "postgres"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_PASSWORD", "testtest"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_DATABASE", "postgres"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connection_string);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Module>();
            modelBuilder.Entity<Account>();
        }
    }
}