using Api.Controllers.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;
namespace Api.Controllers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(_service.GetAllProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var prod = _service.GetProductById(id);
        if(prod == null)
        {
            return NotFound();
        }
        return Ok(prod);
    }

    [HttpPost]
    public IActionResult CreateProduct(Product prod)
    {
        var created = _service.AddProduct(prod);
        return CreatedAtAction(nameof(GetProduct),
            new { id = created.Id },
            created);
    }
}

