using Api.Controllers.Responses;
using Api.Controllers.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Api.Controllers.Controllers;

using Microsoft.AspNetCore.Authorization;
/* 
 * tells that this class is controller
 * automatically model validation , better error respose,  API specific behaviors.
 * GET-> retreive data
 * get by id  return one product
 * PUT -> update existing data
 * DELETE -> delete existing data
*/

[ApiController]
[Route("api/[controller]")]    // here controller is replaced with controller name which is ProductController so the route will be api/product

[Authorize] // now each  endpoint requires authencation
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
        return  Ok(new ApiResponse<
            IEnumerable<ProductReadDto>>
        {
            Success = true,
            Message = "Products retrieved successfully",
            Data = products
        });
    }
    //
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var prod = await _service.GetProductByIdAsync(id);
        //if(prod == null)   // 404 not found status code
        //{
        //    return NotFound(new ApiResponse<ProductReadDto>
        //    {
        //        Success =false,
        //        Message = $"Product with id {id} not found",
        //        Data = null
        //    });
        //}
        return Ok(new ApiResponse<ProductReadDto>
        {
            Success = true,
            Message = $"Product with id {id} retrieved successfully",
            Data = prod
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateDto prod)
    {
        var created = await _service.AddProductAsync(prod);
        var Response = new ApiResponse<ProductReadDto>
        {
            Success = true,
            Message = "Product created successfully",
            Data = created
        };
        return CreatedAtAction(nameof(GetProduct),
            new { id = created.Id },
            Response);    
    }

    [HttpPut("{id}")] // Middleware returns 409 if exception occurs
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto prod)
    {
        var updated = await _service.UpdateProductAsync(id, prod);
        //if (updated == null)
        //{
        //    return NotFound(new ApiResponse<ProductReadDto>
        //    {
        //        Success = false,
        //        Message = $"Product with id {id} not found",
        //        Data = null
        //    });
        //}
        return Ok(new ApiResponse<ProductReadDto>
        {
            Success = true,
            Message = $"Product with id {id} updated successfully",
            Data = updated
        });
    }

    [HttpPut("delete/{id}")] // Middleware returns 404 if exception occurs
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _service.DeleteProductAsync(id);
        //if (!deleted)
        //{
        //    return NotFound(new ApiResponse<ProductReadDto>
        //    {
        //        Success = false,
        //        Message = $"Product with id {id} not found",
        //        Data = null
        //    });
        //}
        return Ok(new ApiResponse<ProductReadDto>
        {
            Success = true,
            Message = $"Product with id {id} deleted successfully",
            Data = deleted ? new ProductReadDto { Id = id } : null
        });
    }

    /* Need to update this and fix 
        need to return Enumnerable<ProductReadDto> instead of ProductReadDto
     */

    [HttpGet("category")] // Middleware returns 404 if exception occurs
    public async Task<IActionResult> GetByCategory(string category)
    {
        var created = await _service.GetByCategoryAsync(category);
        //if (created == null)
        //{
        //    return NotFound(new ApiResponse<ProductReadDto>
        //    {
        //        Success = false,
        //        Message = $"Product with {category} not found",
        //        Data = null
        //    });
        //}
        return Ok(new ApiResponse<IEnumerable<ProductReadDto>>
        {
            Success = true,
            Message = $"Product with {category} found  successfully",
            Data = created
        });
    }

    // NEED TO UPDATE
    //need to return Enumnerable<ProductReadDto> instead of ProductReadDto

    [HttpGet("price")] // Middleware returns 404 if exception occurs
    public async Task<IActionResult> GetExpensiveProduct(decimal price)
    {
            var created = await _service.GetExpensiveProductsAsync(price);
            //if(created == null)
            //{
            //    return NotFound(new ApiResponse<ProductReadDto>
            //    {
            //        Success = false,
            //        Message = $"Product expensive than {price} not found",
            //        Data = null
            //    });
            //}
            return Ok(new ApiResponse<IEnumerable<ProductReadDto>>
            {
                Success = true,
                Message = $"Product expensive than {price} found",
                Data = created
            });
    }

    // NEED TO UPDATE
    // it depends if one or many items returned.
    [HttpGet("name")] // Middleware returns 404 if exception occurs
    public async Task<IActionResult> SearchByName(string name)
    {
        var created = await _service.SearchByNameAsync(name);
       /* if (created == null)
        {
            return NotFound(new ApiResponse<ProductReadDto>
            {
                Success = false,
                Message = $"Product named {name} not found",
                Data = null
            });
        }*/
        return Ok(new ApiResponse<ProductReadDto>
        {
            Success = true,
            Message = $"Product named :{name}  found",
            Data = created
        });
    }
}