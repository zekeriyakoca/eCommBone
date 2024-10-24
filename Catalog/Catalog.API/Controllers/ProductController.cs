using Catalog.API.Commands;
using Catalog.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("/catalog/v{version:apiVersion}/product")]
public class ProductController : BaseController
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }
    
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id));
        if (product == default)
        {
            return NotFound("Product not found!");
        }

        return Ok(product);
    }
    
    [HttpPost("search")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> SearchCatalogItems([FromBody] SearchProductQuery searchRequestDto)
    {
        var productList = await _sender.Send(searchRequestDto);

        return Ok(productList);
    }
}