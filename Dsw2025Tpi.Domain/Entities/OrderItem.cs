namespace Dsw2025Tpi.Domain.Entities;

public class OrderItem: EntityBase
{
    public OrderItem()
    {

    }
    public OrderItem(int quantity, decimal unitPrice, int subtotal, Product productId, Guid orderId)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = subtotal;
        Product = productId;
        OrderId = orderId;
    }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Subtotal { get; set; }
    public Product? Product { get; set; }
    public Guid OrderId { get; set; }
}
