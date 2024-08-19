using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppPB301Practice.DAL;
using ShopAppPB301Practice.DTOs.GroupDTOs;
using ShopAppPB301Practice.Entities;
using ShopAppPB301Practice.Extensions;
using ShopAppPB301Practice.Helpers;

namespace ShopAppPB301Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class GroupController : ControllerBase
    {
        private readonly ShopAppDbContext _context;
        private readonly IMapper _mapper;

        public GroupController(ShopAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var groups = await _context.Groups
                .Include(g => g.Students)
                .ToListAsync();
            List<GroupReturnDTO> list = new();
            foreach (var group in groups)
            {
                list.Add(_mapper.Map<GroupReturnDTO>(group));
            }
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup == null) return NotFound();
            return Ok(_mapper.Map<GroupReturnDTO>(existGroup));
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] GroupCreateDTO groupDTO)
        {
            if (await _context.Groups.AnyAsync(g => g.Name.Trim().ToLower() == groupDTO.Name.Trim().ToLower())) return BadRequest("Group name already exists");
            var file = groupDTO.File;

            var group = _mapper.Map<Group>(groupDTO);
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupUpdateDTO groupDTO)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup == null) return NotFound();
            if (existGroup.Name != groupDTO.Name && await _context.Groups.AnyAsync(g => g.Name.Trim().ToLower() == groupDTO.Name.Trim().ToLower() && g.Id != id)) return BadRequest("Group name already exists");
            existGroup.Name = groupDTO.Name;
            existGroup.Limit = groupDTO.Limit;
            if(groupDTO.File!= null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/images", existGroup.Image);
                FileHelper.Delete(Directory.GetCurrentDirectory()+"wwwroot/uploads/images");
                existGroup.Image = groupDTO.File.Save(Directory.GetCurrentDirectory(), "wwwroot/uploads/images");
            }
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
