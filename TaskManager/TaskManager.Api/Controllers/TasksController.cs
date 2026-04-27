using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models.Entities;

namespace TaskManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        task.UserId = userId;
        task.CreatedAt = DateTime.UtcNow;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

    [HttpPut("{id}/toggle")]
    public async Task<IActionResult> ToggleTask(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null) return NotFound();

        task.IsCompleted = !task.IsCompleted;
        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}