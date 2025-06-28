namespace Dsw2025Tpi.Domain.Entities;

public class OrderItem: EntityBase
{
    public OrderItem()
    {

    }
    public OrderItem(int quantity, decimal unitPrice, int subtotal, Guid productId, Guid orderId)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = subtotal;
        ProductId = productId;
        OrderId = orderId;
    }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Subtotal { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
}
