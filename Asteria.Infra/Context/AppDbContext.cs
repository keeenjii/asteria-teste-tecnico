using Asteria.Domain.Entities;
using Asteria.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Asteria.Infra
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Vendas> Vendas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VendasConfiguration());
        }
    }
}
