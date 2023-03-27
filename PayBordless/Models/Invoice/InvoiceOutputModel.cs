namespace PayBordless.Models.Invoice;

public class InvoiceOutputModel
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public int RecipientId { get; set; }
    public string RecipientName { get; set; }
    public bool? Approved { get; set; }
    public string UserId { get; set; }
}