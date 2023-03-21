namespace PayBordless.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBordless.Data.Models;


public class ServiceConfiguration: IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .HasKey(c => c.Id);
        
        builder
            .Property(c => c.Name)
            .IsRequired();
        
        builder
            .Property(c => c.Price)
            .IsRequired();

        builder
            .HasOne(c => c.Company)
            .WithMany(c => c.Services)
            .HasForeignKey(c => c.CompanyId);

    }
}