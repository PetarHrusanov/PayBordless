namespace PayBordless.Data.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int VAT { get; set; }
    public string Owner { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }
    public virtual ICollection<Service> Services { get; set; }
}