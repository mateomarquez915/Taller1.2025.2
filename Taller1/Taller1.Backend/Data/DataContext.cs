﻿using Microsoft.EntityFrameworkCore;
using Taller1.Shared.Entities;

namespace Taller1.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .Property(x => x.Salary)
                .HasColumnType("decimal(18,2)");
        }
    }
}