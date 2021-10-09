using Microsoft.EntityFrameworkCore;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.AccountId, ar.RoleId });

            //modelBuilder.Entity<JobEmployee>()
            //    .HasKey(je => new { je.EmployeeId, je.JobId });

            modelBuilder.Entity<JobHistory>()
                .HasKey(jh => new { jh.EmployeeId, jh.JobId });

            modelBuilder.Entity<Employee>()
                .HasOne<Account>(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a => a.Id);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRoles)
                .WithOne(ar => ar.Account);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.AccountRoles)
                .WithOne(ar => ar.Role);

            modelBuilder.Entity<Employee>()
               .HasOne<Department>(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Job>()
               .HasOne<Company>(j => j.Company)
               .WithMany(c => c.Jobs)
               .HasForeignKey(j => j.CompanyId);

            modelBuilder.Entity<Employee>()
               .HasMany(e => e.JobEmployees)
               .WithOne(je => je.Employee)
               .HasForeignKey(je => je.EmployeeId);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.JobEmployees)
                .WithOne(je => je.Job)
                .HasForeignKey(je => je.JobId);

            modelBuilder.Entity<Employee>()
               .HasMany(e => e.JobHistories)
               .WithOne(jh => jh.Employee);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.JobHistories)
                .WithOne(jh => jh.Job);


        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobEmployee> JobEmployees { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
