using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppPB301Practice.DAL;
using ShopAppPB301Practice.DTOs.StudentDTOs;
using ShopAppPB301Practice.Entities;
using System.Runtime.CompilerServices;

namespace ShopAppPB301Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ShopAppDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(ShopAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get(int page = 1, string search = null)
        {
            var query = _context.Students.AsQueryable();
            if (search != null) query = query.Where(s => s.Name.ToLower().Contains(search.ToLower()));
            var datas = await query
                .Skip((page - 1) * 3)
                .Include(s => s.Group)
                .Take(3)
                .ToListAsync();
            var totalCount = await query.CountAsync();
            StudentListDTO studentListDTO = new();
            studentListDTO.Students = _mapper.Map<List<StudentReturnDTO>>(datas);
            studentListDTO.TotalCount = totalCount;
            studentListDTO.CurrentPage = page;

            return Ok(studentListDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existStudent = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync();
            if (existStudent is null) return NotFound();
            return Ok(_mapper.Map<StudentReturnDTO>(existStudent));
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(StudentCreateDTO studentDTO)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == studentDTO.GroupId);
            if (existGroup is null) return Conflict("Group not found");
            if (existGroup.Students.Count >= existGroup.Limit) return Conflict("group is full");
            Student student = _mapper.Map<Student>(studentDTO);
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, StudentCreateDTO studentDTO)
        {
            var existStudent = await _context.Students.Include(s=>s.Group).FirstOrDefaultAsync(s => s.Id == id);
            if (existStudent is null) return NotFound();
            if (existStudent.GroupId != studentDTO.GroupId)
            {
                var existGroup = await _context.Groups.Include(g=>g.Students).FirstOrDefaultAsync(g => g.Id == studentDTO.GroupId);
                if (existGroup is null) return Conflict("Group not found");
                if (existGroup.Students.Count >= existGroup.Limit) return Conflict("group is full");
            }
            existStudent = _mapper.Map<Student>(studentDTO);
            existStudent.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<StudentReturnDTO>(existStudent));
        }

    }
}
