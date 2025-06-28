namespace Dsw2025Tpi.Domain.Entities;

public class OrderItem: EntityBase
{
    public OrderItem()
    {

    }
    public OrderItem(int quantity, decimal unitPrice, Guid productId, decimal subtotal)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = subtotal;
        ProductId = productId;
    }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }


}
