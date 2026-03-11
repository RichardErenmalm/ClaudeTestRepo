using ClaudeTestRepo.Application.Commands.Lists;
using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Queries.Lists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeTestRepo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ListsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllListsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ListDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetListByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ListDto>> Create(CreateListDto dto)
    {
        var result = await _mediator.Send(new CreateListCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateListDto dto)
    {
        var success = await _mediator.Send(new UpdateListCommand(id, dto));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteListCommand(id));
        if (!success) return NotFound();
        return NoContent();
    }
}
