using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Dsw2025Tpi.Application.Exceptions.ApplicationException;

namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductsManagementService _service;

    public ProductsController(ProductsManagementService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> AddProduct([FromBody] ProductModel.Request request, CreatedResult createdResult)
    {
        try
        {
            var product = await _service.AddProduct(request);
            return CreatedAtAction(nameof());

        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (Exception)
        {
            return Problem("Se produjo un error al guardar el producto");
        }
    }

    [HttpGet()]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.GetProducts();
        if (products == null || !products.Any()) return NoContent();
        return Ok(products);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductModel.Request request)
    {
        try
        {
            var product = await _service.UpdateProduct(id, request);
            return Ok(product);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (KeyNotFoundException nf)
        {
            return NotFound(nf.Message);
        }
        catch (Exception)
        {
            return Problem("Se produjo un error al actualizar el producto");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> DisableProduct(Guid productId)
    {
        try
        {
            var product = await _service.DisableProduct(productId);
            return Ok(product);
        }
        catch (EntityNotFoundException en)
        {

            return NotFound(en.Message);

        }

    }

}
