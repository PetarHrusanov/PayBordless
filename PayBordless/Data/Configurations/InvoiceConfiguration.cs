namespace PayBordless.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayBordless.Data.Models;

public class InvoiceConfiguration: IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .HasOne(c => c.Service)
            .WithMany(c => c.Invoices)
            .HasForeignKey(c => c.ServiceId);
        
        builder
            .Property(c => c.Quantity)
            .IsRequired();
        
        builder
            .Property(c => c.Total)
            .IsRequired();
    
        builder
            .HasOne(c => c.Recipient)
            .WithMany(c => c.Invoices)
            .HasForeignKey(c => c.RecipientId);
        
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.Invoices)
            .HasForeignKey(c => c.UserId);

    }
}