using ClaudeTestRepo.Application.Commands.ListItems;
using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Queries.ListItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeTestRepo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ListItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListItemDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllListItemsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ListItemDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetListItemByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("bylist/{listId}")]
    public async Task<ActionResult<IEnumerable<ListItemDto>>> GetByListId(int listId)
    {
        var result = await _mediator.Send(new GetListItemsByListIdQuery(listId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ListItemDto>> Create(CreateListItemDto dto)
    {
        var result = await _mediator.Send(new CreateListItemCommand(dto));
        if (result == null) return BadRequest($"List with id {dto.ListId} does not exist.");
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateListItemDto dto)
    {
        var success = await _mediator.Send(new UpdateListItemCommand(id, dto));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteListItemCommand(id));
        if (!success) return NotFound();
        return NoContent();
    }
}
