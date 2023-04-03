using Microsoft.EntityFrameworkCore;

namespace PayBordless.Services.Invoice;

// InvoiceService.cs
using Data;
using Data.Models;
using Models.Invoice;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext _db;

    public InvoiceService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Result> Upload(InvoiceInputModel invoiceInputModel, string userId)
    {
        var invoice = new Invoice
        {
            ServiceId = invoiceInputModel.ServiceId,
            Quantity = invoiceInputModel.Quantity,
            Total = invoiceInputModel.Total,
            RecipientId = invoiceInputModel.RecipientId,
            Approved = invoiceInputModel.Approved,
            UserId = userId
        };

        await _db.Invoices.AddAsync(invoice);
        await _db.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<ICollection<InvoiceOutputModel>> GetAll()
        =>  await _db.Invoices.Select(i => new InvoiceOutputModel 
        {
            Id = i.Id,
            ServiceId = i.ServiceId,
            ServiceName = i.Service.Name,
            Quantity = i.Quantity,
            Total = i.Total,
            IssuerId = i.Service.Company.Id,
            IssuerName = i.Service.Company.Name,
            RecipientId = i.RecipientId,
            RecipientName = i.Recipient.Name,
            Approved = i.Approved,
            UserId = i.UserId
        }).ToListAsync();

    public async Task<ICollection<InvoiceOutputModel>> GetByCompanyCreatorId(string userId)
        =>  await _db.Invoices.Where(i=>i.Service.Company.UserId==userId).Select(i => new InvoiceOutputModel 
        {
            Id = i.Id,
            ServiceId = i.ServiceId,
            ServiceName = i.Service.Name,
            Quantity = i.Quantity,
            Total = i.Total,
            IssuerId = i.Service.Company.Id,
            IssuerName = i.Service.Company.Name,
            RecipientId = i.RecipientId,
            RecipientName = i.Recipient.Name,
            Approved = i.Approved,
            UserId = i.UserId
        }).ToListAsync();

    public async Task<Result> Edit(InvoiceEditModel editModel)
    {
        var invoice = await _db.Invoices.FindAsync(editModel.Id);

        if (invoice == null)
        {
            return Result.Failure("Invoice not found.");
        }

        invoice.ServiceId = editModel.ServiceId;
        invoice.Quantity = editModel.Quantity;
        invoice.Total = editModel.Total;
        invoice.RecipientId = editModel.RecipientId;
        invoice.Approved = editModel.Approved;

        await _db.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<Result> Delete(int id)
    {
        var invoice = await _db.Invoices.FindAsync(id);

        if (invoice == null)
        {
            return Result.Failure("Invoice not found.");
        }

        _db.Invoices.Remove(invoice);
        await _db.SaveChangesAsync();

        return Result.Success;
    }
    
    public async Task SetApprovalStatus(int invoiceId, bool isApproved)
    {
        var invoice = await _db.Invoices.FindAsync(invoiceId);
        if (invoice == null)
        {
            throw new ArgumentException("Invoice not found");
        }

        invoice.Approved = isApproved;
        _db.Invoices.Update(invoice);
        await _db.SaveChangesAsync();
    }
}