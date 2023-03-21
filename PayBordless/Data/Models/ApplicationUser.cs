namespace PayBordless.Data.Models;

using Microsoft.AspNetCore.Identity;
using PayBordless.Data.Interfaces;

public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid().ToString();
        Roles = new HashSet<IdentityUserRole<string>>();
        Claims = new HashSet<IdentityUserClaim<string>>();
        Logins = new HashSet<IdentityUserLogin<string>>();
    }

    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    
    public virtual ICollection<Company> Companies { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }

}

