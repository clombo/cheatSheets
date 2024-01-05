using CQRSExample.API.Commands;
using CQRSExample.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet]
    public async Task<IActionResult> GetProduct([FromQuery] GetProductQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
    
}