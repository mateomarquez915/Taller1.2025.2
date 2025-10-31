using Microsoft.EntityFrameworkCore;
using Taller1.Shared.Entities;

namespace Taller1.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Metodo que se ejecuta cuando se crea el modelo de datos//
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            disableCascadingDelete(modelBuilder);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .Property(x => x.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Employee>()
                .Property(e => e.HireDate)
                .HasDefaultValueSql("GETDATE()");
        }

        private void disableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relations = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relation in relations)
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}