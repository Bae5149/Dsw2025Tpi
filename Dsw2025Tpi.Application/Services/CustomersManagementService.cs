using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services;

public class CustomersManagementService
{
    private readonly IRepository _repository;

    public CustomersManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerModel>?> GetCustomers()
    {
        return (await _repository
            .GetAll<Customer>())?
            .Select(p => new CustomerModel(p.Id, p.Email, p.Name, p.PhoneNumber));
    }
}
