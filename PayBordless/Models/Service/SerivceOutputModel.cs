namespace PayBordless.Models.Service;

public class SerivceOutputModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string CompanyName { get; set; }
    public int CompanyId { get; set; }
    public int Vat { get; set; }
    public string Owner { get; set; }
    public string UserId { get; set; }
}