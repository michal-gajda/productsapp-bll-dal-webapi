namespace WebAPI.Controllers;

using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();

        return Ok(products.Select(p => (ProductResponse)p));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = $"Produkt o ID {id} nie został znaleziony", });
        }

        return Ok((ProductResponse)product);
    }

    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetByCategory(string category)
    {
        var products = await _productService.GetProductsByCategoryAsync(category);

        return Ok(products.Select(p => (ProductResponse)p));
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetActive()
    {
        var products = await _productService.GetActiveProductsAsync();

        return Ok(products.Select(p => (ProductResponse)p));
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<string>>> GetCategories()
    {
        var categories = await _productService.GetCategoriesAsync();

        return Ok(categories);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
    {
        var model = (BLL.Models.CreateProduct)request;
        var (validation, product) = await _productService.CreateProductAsync(model);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.ToDictionary(e => e.Field, e => e.Message);

            return BadRequest(new
            {
                message = "Walidacja nie powiodła się",
                errors = errors,
            });
        }

        var routeValues = new { id = product!.Id, };
        var response = (ProductResponse)product;

        return CreatedAtAction(nameof(GetById), routeValues, response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductResponse>> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var model = (BLL.Models.UpdateProduct)request;
        var (validation, product) = await _productService.UpdateProductAsync(id, model);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.ToDictionary(e => e.Field, e => e.Message);

            return BadRequest(new
            {
                message = "Walidacja nie powiodła się",
                errors = errors,
            });
        }

        var response = (ProductResponse)product!;

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = $"Produkt o ID {id} nie został znaleziony", });
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/toggle-active")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> ToggleActive(int id)
    {
        var product = await _productService.ToggleActiveAsync(id);

        if (product == null)
        {
            var errorResponse = new { message = $"Produkt o ID {id} nie został znaleziony", };

            return NotFound(errorResponse);
        }

        var response = (ProductResponse)product;

        return Ok(response);
    }
}
