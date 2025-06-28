using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Domain.Entities;

public class Order: EntityBase
{
    public Order()
    {

    }
    public Order(DateTime date, string shippingAdress, string billingAdress, string notes, decimal totalAmount, Guid customerId, OrderStatus status)
    {
        Date = date;
        ShippingAdress = shippingAdress;
        BillingAdress = billingAdress;
        Notes = notes;
        TotalAmount = totalAmount;
        CustomerId = customerId;
        Status = status;
    }
    public DateTime Date { get; set; }
    public string? ShippingAdress { get; set; }
    public string? BillingAdress { get; set; }
    public string? Notes { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid? CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; } = new HashSet<OrderItem>();
}
