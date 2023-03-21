namespace PayBordless.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBordless.Data.Models;

public class CompanyConfiguration: IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired();
        
        builder
            .Property(c => c.VAT)
            .IsRequired();
        
        builder
            .Property(c => c.Owner)
            .IsRequired();

        builder
            .HasOne(c => c.User)
            .WithMany(c => c.Companies)
            .HasForeignKey(c => c.UserId);
        
    }
}