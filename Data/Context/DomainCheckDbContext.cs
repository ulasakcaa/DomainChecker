using System.Data;
using System.Reflection;
using Application;
using Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.Common;

namespace Data.Context
{
    public class DomainCheckDbContext : Microsoft.EntityFrameworkCore.DbContext, IDomainCheckDbContext
    {
        public DomainCheckDbContext(DbContextOptions<DomainCheckDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = MyConfiguration.Configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DomainCheckDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && typeof(BaseContextEntity).IsAssignableFrom(i.GenericTypeArguments[0])));
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<T> Set<T>() where T : BaseContextEntity
        {
            return base.Set<T>();
        }

        public DbSet<DomainCheck> DomainChecks { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public override ValueTask DisposeAsync()
        {
            Dispose();
            GC.SuppressFinalize(this);
            return ValueTask.CompletedTask;
        }

    }
}
