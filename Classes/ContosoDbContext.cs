using Microsoft.EntityFrameworkCore;
using contoso_receipt_backend.Classes.Receipts;
using contoso_receipt_backend.Classes.Statuses;
using contoso_receipt_backend.Classes.Users.Administrators;
using contoso_receipt_backend.Classes.Users.Employees;
using contoso_receipt_backend.Classes.Users.Reviewers;

namespace contoso_receipt_backend.Classes
{
    public class ContosoDbContext : DbContext
    {
        public ContosoDbContext(DbContextOptions<ContosoDbContext> options) : base(options)
        { }

        // DbSets for all entities
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusChange> StatusChanges { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Name);

            // Configure Merchant
            modelBuilder.Entity<Merchant>()
                .HasKey(m => m.Proper_name);

            // Configure Status
            modelBuilder.Entity<Status>()
                .HasKey(s => s.Name);

            // Configure User hierarchy - Table-per-Type (TPT) strategy
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);

            modelBuilder.Entity<Administrator>()
                .ToTable("Administrators");

            modelBuilder.Entity<Employee>()
                .ToTable("Employees");

            modelBuilder.Entity<Reviewer>()
                .ToTable("Reviewers");

            // Configure Receipt
            modelBuilder.Entity<Receipt>()
                .HasKey(r => r.Id);

            // Configure foreign key relationships for Receipt
            modelBuilder.Entity<Receipt>()
                .HasOne<Merchant>()
                .WithMany()
                .HasForeignKey(r => r.Proper_name)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.Email)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(r => r.Name)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure StatusChange - composite key
            modelBuilder.Entity<StatusChange>()
                .HasKey(sc => new { sc.Email, sc.Old_status, sc.New_status });

            // Configure StatusChange relationships
            modelBuilder.Entity<StatusChange>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(sc => sc.Email)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
