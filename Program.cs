using System.Runtime.CompilerServices;
using contoso_receipt_backend.Classes.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
receipts.MapPut("/{id}", UpdateReceipt);
receipts.MapDelete("/{id}", DeleteReceiptById);
// to do next return the receipts that are marked as pending for review


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

static async Task<IResult> UpdateReceipt(int id, ReceiptDTO recDTO, ReceiptDB db)
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
        receipt.Name = recDTO.Name;

        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}

static async Task<IResult> GetReceiptById(int id, ReceiptDB db)
{
    return await db.Receipts.FindAsync(id) //EF method to find by its ID (PK)
        is Receipt rec // if that is a receipt
            ? TypedResults.Ok(new ReceiptDTO(rec)) //return the DTO
            : TypedResults.NotFound(); //else return not found
}

static async Task<IResult> DeleteReceiptById(int id, ReceiptDB db)
{
    if (await db.Receipts.FindAsync(id) is Receipt rec)
    {
        db.Receipts.Remove(rec);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}