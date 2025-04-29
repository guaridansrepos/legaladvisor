using Advocate_Invoceing.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Advocate_Invoceing.Models.Entity
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserTypeEntites>().ToTable("UserType");
            modelBuilder.Entity<ClientEntity>().ToTable("Clients");
            modelBuilder.Entity<EmployeePunch>().ToTable("EmployeePunch");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");
            modelBuilder.Entity<EmployeeLog>().ToTable("EmployeeWorkLogs");
            modelBuilder.Entity<AdminApproval>().ToTable("AdminApprovals");


        }


        public DbSet<AdminApproval> AdminApprovals { get; set; }
        public DbSet<EmployeeLog> EmployeeLogs { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<UserEntity> userEntity { get; set; }
        public DbSet<UserTypeEntites> userTypeEntites { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<EmployeePunch> EmployeePunches { get; set; }

    }

}
