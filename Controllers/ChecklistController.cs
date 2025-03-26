using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Db.Entities;
using RecruitmentTest.Model.Checklist;
using RecruitmentTest.Services;

namespace RecruitmentTest.Controllers;

[ApiController]
[Route("checklist")]
public class ChecklistController : ControllerBase
{
    private readonly IUserServices _user;
    private readonly IChecklistServices _checklist;
    public ChecklistController(IUserServices user, IChecklistServices checklist)
    {
        _user = user;
        _checklist = checklist;
    }

    [HttpPost("")]
    public async Task<IActionResult> ChecklistCreate(ChecklistCreateRequest request)
    {
        await _checklist.Create(request);
        return Ok();
    }

    [HttpDelete("{checklistId}")]
    public async Task<IActionResult> ChecklistDelete([FromRoute] int checklistId)
    {
        await _checklist.Delete(checklistId);
        return Ok();
    }

    [HttpGet("")]
    public async Task<IActionResult> ChecklistGetAll()
    {
        var result = await _checklist.GetAll();
        return Ok(result);
    }

    [HttpGet("{checklistId}")]
    public async Task<IActionResult> ChecklistGetById([FromRoute] int checklistId)
    {
        var result = await _checklist.GetById(checklistId);
        return Ok(result);
    }

    [HttpPost("{checklistId}/item")]
    public async Task<IActionResult> ItemCreate([FromRoute] int checklistId, [FromBody] ChecklistItemRequest request)
    {
        await _checklist.ItemCreate(checklistId, request);
        return Ok();
    }

    [HttpPut("{checklistId}/item/rename/{checklistItemId}")]
    public async Task<IActionResult> ItemUpdate([FromRoute] int checklistId, [FromRoute] int checklistItemId, [FromBody] ChecklistItemRequest request)
    {
        await _checklist.ItemUpdate(checklistId, checklistItemId, request);
        return Ok();
    }

    [HttpPut("{checklistId}/item/{checklistItemId}")]
    public async Task<IActionResult> ItemUpdateStatus([FromRoute] int checklistId, [FromRoute] int checklistItemId)
    {
        await _checklist.ItemUpdteStatus(checklistId, checklistItemId);
        return Ok();
    }

    [HttpDelete("{checklistId}/item/{checklistItemId}")]
    public async Task<IActionResult> ItemDelete([FromRoute] int checklistId, [FromRoute] int checklistItemId)
    {
        await _checklist.ItemDelete(checklistId, checklistItemId);
        return Ok();
    }

    [HttpGet("{checklistId}/item")]
    public async Task<IActionResult> ItemGetList([FromRoute] int checklistId)
    {
        var result = await _checklist.ItemGetList(checklistId);
        return Ok(result);
    }

    [HttpGet("{checklistId}/item/{checklistItemId}")]
    public async Task<IActionResult> ItemGetById([FromRoute] int checklistId, [FromRoute] int checklistItemId)
    {
        var result = await _checklist.ItemGetById(checklistId, checklistItemId);
        return Ok(result);
    }
}
