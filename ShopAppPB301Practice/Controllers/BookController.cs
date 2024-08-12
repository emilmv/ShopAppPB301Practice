using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppDll.Entities;
using ShopAppPB301Practice.DAL;
using ShopAppPB301Practice.DTOs.BookDTOs;

namespace ShopAppPB301Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ShopAppDbContext _context;
        private readonly IMapper _mapper;
        public BookController(ShopAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(BookCreateDTO bookDTO)
        {
            foreach (var authorId in bookDTO.AuthorIds)
            {
                if (await _context.Authors.AnyAsync(a => a.Id == authorId))
                    return BadRequest($"author with id of:{authorId}not found");
            }
            Book book = new();
            book.Name = bookDTO.Name;
            book.PageCount = bookDTO.PageCount;
            foreach (var authorId in bookDTO.AuthorIds)
            {
                BookAuthor bookAuthor = new();
                bookAuthor.AuthorId = authorId;
                bookAuthor.BookId = book.Id;
                book.BookAuthors.Add(bookAuthor);
            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b=>b.Id==id);
            BookReturnDTO bookReturnDTO = _mapper.Map<BookReturnDTO>(book);
            return Ok(bookReturnDTO);
        }
    }
}
