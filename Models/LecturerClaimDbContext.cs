using Microsoft.EntityFrameworkCore;

namespace W98.Models
{
    public class LecturerClaimDbContext : DbContext
    {
        public DbSet<register> Users { get; set; }

        public DbSet<Check_login> log { get; set; }
        public DbSet<LecturerClaim> LecturerClaims { get; set; }
        public LecturerClaimDbContext(DbContextOptions<LecturerClaimDbContext> options) : base(options) { }

    



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Other configurations...

            modelBuilder.Entity<Check_login>()
                .HasKey(c => c.Id);
            // Specify the primary key
            modelBuilder.Entity<register>()
    .HasKey(r => r.Id); // Specify the primary key
        }

    }
}
