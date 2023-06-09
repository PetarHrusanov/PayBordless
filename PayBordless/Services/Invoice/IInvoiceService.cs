using PayBordless.Models.Invoice;

namespace PayBordless.Services.Invoice;

public interface IInvoiceService
{
    Task<Result> Upload(InvoiceInputModel invoiceInputModel, string userId);
    Task<ICollection<InvoiceOutputModel>> GetAll();
    Task<ICollection<InvoiceOutputModel>> GetByCompanyCreatorId(string userId);
    Task<Result> Edit(InvoiceEditModel editModel);
    Task<Result> Delete(int id);
    
    Task SetApprovalStatus(int invoiceId, bool isApproved);
}