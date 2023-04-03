namespace PayBordless.Data.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }
}