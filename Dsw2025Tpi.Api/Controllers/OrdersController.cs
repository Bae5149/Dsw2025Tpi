using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/orders")]
    
public class OrdersController : ControllerBase
{
    private readonly OrdersManagementService _service;

    public OrdersController(OrdersManagementService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] OrderModel.Request request)
    {
        if (!request.OrderItems.Any())
            return BadRequest("La orden debe contener al menos un ítem.");

        try
        {
            var result = await _service.CreateOrderAsync(request);
            return CreatedAtAction(nameof(GetOrderById), new {id = result.});
        }
        catch (InsufficientStockException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

