using System.Runtime.CompilerServices;
using contoso_receipt_backend.Classes;
using contoso_receipt_backend.Classes.Receipts;
using contoso_receipt_backend.Classes.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);
// dependency injection for the SQLite database context, it will construct the ContosoDbContext object when needed
// will allow the database to be injected into the methods that need it
builder.Services.AddDbContext<ContosoDbContext>(options =>
    options.UseSqlite("Data Source=contoso_receipts.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContosoDbContext>();
    
    // Ensure database is created
    db.Database.EnsureCreated();
    
    // Seed Categories if none exist
    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category("Office Supplies"),
            new Category("Software & Subscriptions"),
            new Category("Cloud & Infrastructure"),
            new Category("IT & Electronics (Accessories & Peripherals)"),
            new Category("Shipping & Logistics"),
            new Category("Business Travel & Transportation"),
            new Category("Parking & Tolls"),
            new Category("Office Pantry & Refreshments"),
            new Category("Facilities & Maintenance Services"),
            new Category("Office Furniture"),
            new Category("Professional Development (Training & Education)"),
            new Category("Marketing & Advertising"),
            new Category("Telecommunications (Internet & Phone)"),
            new Category("Team Events & Employee Engagement")
        );
    }
    
    // Seed Merchants if none exist
    if (!db.Merchants.Any())
    {
        db.Merchants.AddRange(
            new Merchant("Starbucks"),
            new Merchant("Amazon"),
            new Merchant("Uber")
        );
    }
    
    // Seed Employees if none exist
    if (!db.Employees.Any())
    {
        db.Employees.AddRange(
            new Employee("john.doe@contoso.com", "password123", "John", "Doe"),
            new Employee("jane.smith@contoso.com", "password123", "Jane", "Smith")
        );
    }
    
    db.SaveChanges();
}

var receipts = app.MapGroup("/receipts");
receipts.MapGet("/", GetAllReceipts);
receipts.MapPost("/", CreateReceipt);
receipts.MapGet("/{id}", GetReceiptById);
receipts.MapPut("/{id}", UpdateReceipt);
receipts.MapDelete("/{id}", DeleteReceiptById);
// to do next return the receipts that are marked as pending for review


app.Run();

//method to get all receipts
static async Task<IResult> GetAllReceipts(ContosoDbContext db)
{ //x is the equivalent of the * in sql, we are selecting all columns, DTO stands for Data Transfer Object
    return TypedResults.Ok(await db.Receipts.Select(x => new ReceiptDTO(x)).ToArrayAsync());
}

static async Task<IResult> CreateReceipt(Receipt rec, ContosoDbContext db)
{
    db.Receipts.Add(rec);
    await db.SaveChangesAsync(); //EF method to save changes to the database

    //construct a DTO to return
    var dto = new ReceiptDTO(rec);

    return TypedResults.Created($"/receipts/{rec.Id}", dto);  
}

static async Task<IResult> UpdateReceipt(int id, ReceiptDTO recDTO, ContosoDbContext db)
{
    var receipt = await db.Receipts.FindAsync(id);
    if (receipt is null) return TypedResults.NotFound();
    else
    {
        //map the updated values from the DTO to the entity
        receipt.Receipt_date = recDTO.Receipt_date;
        receipt.Total_amount = recDTO.Total_amount;
        receipt.Proper_name = recDTO.Proper_name;
        receipt.Email = recDTO.Email;
        receipt.CategoryName = recDTO.CategoryName;

        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}

static async Task<IResult> GetReceiptById(int id, ContosoDbContext db)
{
    return await db.Receipts.FindAsync(id) //EF method to find by its ID (PK)
        is Receipt rec // if that is a receipt
            ? TypedResults.Ok(new ReceiptDTO(rec)) //return the DTO
            : TypedResults.NotFound(); //else return not found
}

static async Task<IResult> DeleteReceiptById(int id, ContosoDbContext db)
{
    if (await db.Receipts.FindAsync(id) is Receipt rec)
    {
        db.Receipts.Remove(rec);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}