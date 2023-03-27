namespace PayBordless.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PayBordless.Models.Invoice;
using PayBordless.Services.Invoice;

public class InvoiceController : ApiController
{
    private readonly ICurrentUserService _currentUser;
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(ICurrentUserService currentUser, IInvoiceService invoiceService)
    {
        _currentUser = currentUser;
        _invoiceService = invoiceService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Upload(InvoiceInputModel inputModel)
    {
        var userId = _currentUser.UserId;
        await _invoiceService.Upload(inputModel, userId);
        return Result.Success;
    }

    [HttpGet]
    [Authorize]
    public async Task<ICollection<InvoiceOutputModel>> GetByUser()
    {
        var userId = _currentUser.UserId;
        var invoices = await _invoiceService.GetAll();
        var userInvoices = invoices.Where(c => c.UserId == userId).ToList();
        return userInvoices;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ICollection<InvoiceOutputModel>> GetUnapprovedByUserId()
    {
        var userId = _currentUser.UserId;
        var invoices = await _invoiceService.GetByCompanyCreatorId(userId);
        var userInvoices = invoices.Where(c => c.Approved==null).ToList();
        return userInvoices;
    }

    [HttpGet]
    [Authorize]
    public async Task<ICollection<InvoiceOutputModel>> GetAll()
        => await _invoiceService.GetAll();

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Edit(InvoiceEditModel inputModel)
    {
        var userId = _currentUser.UserId;
        // if (userId != inputModel.UserId) return BadRequest("Incorrect user");
        await _invoiceService.Edit(inputModel);
        return Result.Success;
    }

    [HttpDelete]
    [Route(Id)]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = _currentUser.UserId;
        // if (userId != inputModel.UserId) return BadRequest("Incorrect user");
        await _invoiceService.Delete(id);
        return Result.Success;
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Approve(InvoiceApprovalModel approvalModel)
    {
        var userId = _currentUser.UserId;
        // Add any necessary authorization checks here, e.g., checking user roles

        await _invoiceService.SetApprovalStatus(approvalModel.Id, approvalModel.IsApproved);
        return Result.Success;
    }
    
}