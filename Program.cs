using contoso_receipt_backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// dependency injection for the inmemory database context, it will construct the receiptdb object when needed
// will allow the database to be injected into the methods that need it
builder.Services.AddDbContext<ReceiptDB>(options =>
    options.UseInMemoryDatabase("ReceiptList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

var receipts = app.MapGroup("/receipts");
receipts.MapGet("/", GetAllReceipts);
receipts.MapPost("/", CreateReceipt);
receipts.MapGet("/{id}", GetReceiptById);

app.Run();

//method to get all receipts
static async Task<IResult> GetAllReceipts(ReceiptDB db)
{ //x is the equivalent of the * in sql, we are selecting all columns, DTO stands for Data Transfer Object
    return TypedResults.Ok(await db.Receipts.Select(x => new ReceiptDTO(x)).ToArrayAsync());
}

static async Task<IResult> CreateReceipt(Receipt rec, ReceiptDB db)
{
    db.Receipts.Add(rec);
    await db.SaveChangesAsync(); //EF method to save changes to the database

    //construct a DTO to return
    var dto = new ReceiptDTO(rec);

    return TypedResults.Created($"/receipts/{rec.Id}", dto);  
}

static async Task<IResult> GetReceiptById(int id, ReceiptDB db)
{
    return await db.Receipts.FindAsync(id) //EF method to find by its ID (PK)
        is Receipt rec // if that is a receipt
            ? TypedResults.Ok(new ReceiptDTO(rec)) //return the DTO
            : TypedResults.NotFound(); //else return not found
}