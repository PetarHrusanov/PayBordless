namespace PayBordless.Models.Invoice;

public class InvoiceInputModel
{
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public int RecipientId { get; set; }
    public bool? Approved { get; set; }
}