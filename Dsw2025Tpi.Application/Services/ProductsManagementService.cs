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

}
