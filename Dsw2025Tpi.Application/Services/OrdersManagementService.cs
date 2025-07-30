using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Enums;
using Dsw2025Tpi.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2025Tpi.Application.Services;

public class OrdersManagementService
{
    private readonly IRepository _repository;

    public OrdersManagementService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<OrderModel.OrderResponse> AddOrder(OrderModel.OrderRequest request)
    {
        var orderItems = new List<OrderItem>();
        decimal total = 0;

        var customer = await _repository.GetById<Customer>(request.customerId);
        if (customer == null)
            throw new Exception($"El cliente {request.customerId} no existe o no se encontro");

        foreach (var item in request.OrderItems)
        {
            var product = await _repository.GetById<Product>(item.productId);
            if (product == null)
                throw new Exception($"Producto {item.productId} no encontrado");

            if (product.StockQuantity < item.quantity)
                throw new InsufficientStockException($"Stock insuficiente para {product.Name}");


            product.StockQuantity -= item.quantity;
            await _repository.Update(product);

            var subtotal = item.quantity * item.currentUnitPrice;
            total = total + subtotal;
            orderItems.Add(new OrderItem(item.quantity, item.currentUnitPrice, product.Id,subtotal));
        }

        var order = new Order(DateTime.Today, request.shippingAdress, request.billingAdress, "notas", total, request.customerId);
        await _repository.Add(order);
        foreach (var item in orderItems)
        {
            item.OrderId = order.Id;
            await _repository.Add(item);
        }

        return new OrderModel.OrderResponse(order.Date,request.shippingAdress,request.billingAdress,order.Notes,total,request.customerId,order.Id);
    }
    public async Task<OrderModel.OrderResponse?> GetOrderById(Guid id)
    {
        var order = await _repository.GetById<Order>(id);
        return order != null ?
            new OrderModel.OrderResponse(order.Date, order.ShippingAdress, order.BillingAdress, order.Notes, order.TotalAmount, order.CustomerId,order.Id) :
            throw new EntityNotFoundException("No se encontro la orden que se desea obtener");
    }
}
