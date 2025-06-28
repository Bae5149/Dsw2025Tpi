using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services;

public class ProductsManagementService
{
    private readonly IRepository _repository;

    public ProductsManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductModel.Response?> GetProductById(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        return product != null ?
            new ProductModel.Response(product.Id, product.Sku, product.Name, product.CurrentUnitPrice) :
            null;
    }

    public async Task<IEnumerable<ProductModel.Response>?> GetProducts()
    {
        return (await _repository
            .GetFiltered<Product>(p => p.IsActive))?
            .Select(p => new ProductModel.Response(p.Id, p.Sku, p.Name, 
            p.CurrentUnitPrice));
    }

    public async Task<ProductModel.Response> AddProduct(ProductModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Sku)) throw new ArgumentException("Valor del Sku para el producto no válido");
        if(string.IsNullOrWhiteSpace(request.Description)) throw new ArgumentException("Valor para la descripcion del producto no válida");
        if(string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentException("Valor para el nombre del producto no válido");
        if(string.IsNullOrWhiteSpace(request.InternalCode)) throw new ArgumentException("Valor para el Internal Code para el producto no válidos");
        if(request.StockQuantity < 1) throw new ArgumentException("Valor para el Stock del producto no válido");
        if(request.Price < 0) throw new ArgumentException("Valor para el precio del producto no válido");
        
        var exist = await _repository.First<Product>(p => p.Sku == request.Sku);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
        var product = new Product(request.Sku, request.InternalCode, request.Name,request.Description, request.Price,request.StockQuantity);
        await _repository.Add(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name,
            product.CurrentUnitPrice);
    }

    public async Task<ProductModel.Response?> UpdateProduct(ProductModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Sku)) throw new ArgumentException("Valor del Sku para el producto no válido");
        if (string.IsNullOrWhiteSpace(request.Description)) throw new ArgumentException("Valor para la descripcion del producto no válida");
        if (string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentException("Valor para el nombre del producto no válido");
        if (string.IsNullOrWhiteSpace(request.InternalCode)) throw new ArgumentException("Valor para el Internal Code para el producto no válidos");
        if (request.StockQuantity < 1) throw new ArgumentException("Valor para el Stock del producto no válido");
        if (request.Price < 0) throw new ArgumentException("Valor para el precio del producto no válido");

        var product = await _repository.First<Product>(p => p.Sku == request.Sku);
        if (product == null) throw new EntityNotFoundException($"No se encontro el producto con Sku = {request.Sku} que se desea obtener");

        product = await _repository.Update(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name, product.CurrentUnitPrice);

    }

    public async Task<ProductModel.Response?> DisableProduct(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        if (product == null) throw new EntityNotFoundException($"No se encontro el producto {id} que se desea obtener");
        product.IsActive = false;
        product = await _repository.Update(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name, product.CurrentUnitPrice);
    }
}
