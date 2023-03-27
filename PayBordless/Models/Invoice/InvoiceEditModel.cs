namespace PayBordless.Models.Invoice;

public class InvoiceEditModel
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public int RecipientId { get; set; }
    public bool? Approved { get; set; }
}