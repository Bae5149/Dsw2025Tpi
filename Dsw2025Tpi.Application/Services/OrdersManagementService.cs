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

    /*public async Task<OrderModel.Response> AddOrder(ProductModel.Request request) //pendiente de modificar
    {
        if (string.IsNullOrWhiteSpace(request.Sku) || 
            string.IsNullOrWhiteSpace(request.Name) ||
            request. < 0)
        {
            throw new ArgumentException("Valores para el producto no válidos");
        }

        var exist = await _repository.First<Order>(p => p.Sku == request.Sku);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");

        var order = new Order(request.);
        await _repository.Add(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name,product.CurrentUnitPrice);
    }*/

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

}
