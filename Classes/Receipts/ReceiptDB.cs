using Microsoft.EntityFrameworkCore;

namespace contoso_receipt_backend.Classes.Receipts
{
    public class ReceiptDB : DbContext
    {
        public ReceiptDB(DbContextOptions<ReceiptDB> options) : base(options)
        { }

        // will create a set of type receipt (our cars table in the DB) using entity framework
        public DbSet<Receipt> Receipts { get; set; }
    }
}
