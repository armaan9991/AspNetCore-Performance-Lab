using Api.Controllers.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;
namespace Api.Controllers.Controllers;


/*  tells that this class is controller
 *  automatically model validation , better error respose,  API specific behaviors.
 * GET-> retreive data
 * get by id  return one product
 * PUT -> update existing data
 * DELETE -> delete existing data
 */

[ApiController]
[Route("api/[controller]")]    // here controller is replaced with controller name which is ProductController so the route will be api/product

public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }
    // first end point
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.GetAllProductsAsync();
        return  Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var prod = await _service.GetProductByIdAsync(id);
        if(prod == null)   // 404 not found status code
        {
            return NotFound();
        }
        return Ok(prod);   // is 200 status code with data in JSON
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product prod)
    {
        var created = await _service.AddProductAsync(prod);
        return CreatedAtAction(nameof(GetProduct),
            new { id = created.Id },
            created);    // 201 created status code with data in JSON
    }
}

