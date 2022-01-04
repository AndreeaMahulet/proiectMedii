using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Data
{
    public class CompanyContext:DbContext
    {
       
        public CompanyContext(DbContextOptions<CompanyContext> options) :base(options){}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EmplyeesForClient> EmplyeesForClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Technology>().ToTable("Technology");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<EmplyeesForClient>().ToTable("EmployeesForClient");
            modelBuilder.Entity<EmplyeesForClient>()
            .HasKey(c => new { c.EmployeeID, c.ClientID });//configureaza cheia primara compusa

        }
    }
}

