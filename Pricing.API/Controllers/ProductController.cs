using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pricing.API.Controllers;

[Route("/pricing/v{version:apiVersion}/product")]
public class PricingController : BaseController
{
    private readonly ISender _sender;

    public PricingController(ISender sender)
    {
        _sender = sender;
    }


    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult Test()
    {
        return Ok(new
        {
            message = "Hello from Pricing API"
        });
    }
}