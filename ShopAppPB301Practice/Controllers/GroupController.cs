using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppPB301Practice.DAL;
using ShopAppPB301Practice.Entities;

namespace ShopAppPB301Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ShopAppDbContext _context;

        public GroupController(ShopAppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var groups= await _context.Groups
                .Include(g=>g.Students)
                .ToListAsync();
            return Ok(await _context.Groups.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup == null) return NotFound();
            return Ok(existGroup);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(Group group)
        {
            if (await _context.Groups.AnyAsync(g => g.Name.Trim().ToLower() == group.Name.Trim().ToLower())) return BadRequest("Group name already exists");
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Group group)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup == null) return NotFound();
            if (existGroup.Name != group.Name && await _context.Groups.AnyAsync(g => g.Name.Trim().ToLower() == group.Name.Trim().ToLower() && g.Id != id)) return BadRequest("Group name already exists");
            existGroup.Name = group.Name;
            existGroup.Limit = group.Limit;
            await _context.SaveChangesAsync();
            return Ok(existGroup);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup == null) return NotFound();
            _context.Groups.Remove(existGroup);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
