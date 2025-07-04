using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ApplicationException = Dsw2025Tpi.Application.Exceptions.ApplicationException;

namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/customers")]
[Tags("Customers")]
public class CustomersController : ControllerBase
{
    private readonly CustomersManagementService _service;

    public CustomersController(CustomersManagementService service)
    {
        _service = service;
    }

    [HttpGet()]
    public async Task<IActionResult> GetCustomers()
    {
        try
        { 
            var customers = await _service.GetCustomers();
            if (customers == null || !customers.Any()) return NoContent();
            return Ok(customers);
        }
        catch (Exception e)
        {

            return Problem($"Se produjo un error al obtener los clientes: {e.Message}");

        }
    }

}
