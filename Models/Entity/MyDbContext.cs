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


        }


        public DbSet<UserEntity> userEntity { get; set; }
        public DbSet<UserTypeEntites> userTypeEntites { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }

    }

}
