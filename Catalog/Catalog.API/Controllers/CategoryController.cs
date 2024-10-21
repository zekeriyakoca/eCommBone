using Catalog.API.Commands;
using Catalog.API.Queries;
using MediatR;
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
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var category = await _sender.Send(new GetCategoryByIdQuery(id));
        if (category == default)
        {
            return NotFound("Category not found!");
        }

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryCommand command)
    {
        var categoryId = await _sender.Send(command);
        return Ok(categoryId);
    }


    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryCommand command)
    {
        var category = await _sender.Send(command);
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var isSuccess = await _sender.Send(new DeleteCategoryCommand(id));
        if (!isSuccess)
        {
            return NotFound();
        }

        return NoContent();
    }
}