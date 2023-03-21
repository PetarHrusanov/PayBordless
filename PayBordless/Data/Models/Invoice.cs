namespace PayBordless.Data.Models;

public class Invoice
{
    public int Id { get; set; }
    
    public int ServiceId { get; set; }
    
    public virtual Service Service { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Total { get; set; }
    
    public int RecipientId { get; set; }
    
    public bool? Approved { get; set; }
    
    public virtual Company Recipient { get; set; }
    
    public virtual string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    // public virtual ICollection<Service> Services { get; set; }
}