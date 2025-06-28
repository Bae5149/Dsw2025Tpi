using Microsoft.EntityFrameworkCore;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Data;

public class Dsw2025TpiContext: DbContext
{
    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>(eb =>
        {
            eb.ToTable("Customers");
            eb.HasKey(p => p.Id);
            eb.Property(p => p.Email)
            .HasMaxLength(255)
            .IsRequired();
            eb.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
            eb.Property(p => p.PhoneNumber)
            .HasMaxLength(10);
        });
        modelBuilder.Entity<Product>(eb =>
        {
            eb.ToTable("Products");
            eb.HasKey(p => p.Id);
            eb.Property(p => p.Sku)
            .HasMaxLength(10)
            .IsRequired();
            eb.Property(p => p.InternalCode)
            .HasMaxLength(10)
            .IsRequired();
            eb.Property(p => p.Name)
            .HasMaxLength(30)
            .IsRequired();
            eb.Property(p => p.Description)
            .HasMaxLength(100);
            eb.Property(p => p.CurrentUnitPrice)
            .HasPrecision(10, 2)
            .IsRequired();
            eb.Property(p => p.StockQuantity)
            .IsRequired();
      
        });
        modelBuilder.Entity<Order>(eb =>
        {
            eb.ToTable("Orders");
            eb.HasKey(p => p.Id);
            eb.Property(p => p.ShippingAdress)
            .HasMaxLength(255)
            .IsRequired();
            eb.Property(p => p.BillingAdress)
            .HasMaxLength(255)
            .IsRequired();
            eb.Property(p => p.Date)
            .IsRequired();
            eb.Property(p => p.CustomerId)
            .IsRequired();
        });
        modelBuilder.Entity<OrderItem>(eb =>
        {
            eb.ToTable("OrderItems");
            eb.HasKey(p => p.Id);
            eb.Property(p => p.OrderId)
            .IsRequired();
            eb.Property(p => p.ProductId)
            .IsRequired();
            eb.Property(p => p.Quantity)
            .IsRequired();
            eb.Property(p => p.UnitPrice)
            .IsRequired();
        });
    }
}
