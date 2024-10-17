using Catalog.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("/catalog/v{version:apiVersion}/category")]
public class CategoryController : BaseController
{
    private readonly ISender _sender;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _sender.Send(new GetCategoriesQuery()));
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return Ok(await _sender.Send(new GetCategoryByIdQuery(id)));
    }
}

