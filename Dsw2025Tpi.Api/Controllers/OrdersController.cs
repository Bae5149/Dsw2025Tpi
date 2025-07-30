using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ApplicationException = Dsw2025Tpi.Application.Exceptions.ApplicationException;


namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/orders")]
[Tags("Orders")]    
public class OrdersController : ControllerBase
{
    private readonly OrdersManagementService _service;

    public OrdersController(OrdersManagementService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] OrderModel.OrderRequest request)
    {
        if (!request.OrderItems.Any())
            return BadRequest("La orden debe contener al menos un ítem.");

        try
        {
            var result = await _service.AddOrder(request);
            return CreatedAtAction(nameof(_service.AddOrder), new {id = result.OrderId});
        }
        catch (InsufficientStockException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        try
        {
            var order = await _service.GetOrderById(id);
            return Ok(order);
        }
        catch (EntityNotFoundException en)
        {

            return NotFound(en.Message);

        }
    }
}

