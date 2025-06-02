using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApisLearning.Models;
using RestApisLearning.Data;
using Microsoft.EntityFrameworkCore;

namespace RestApisLearning.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _DBContext;
        public BooksController(AppDbContext context) {
            _DBContext = context;
        }
        [HttpGet("getbooks")]
        public async Task<IActionResult> GetBooks()
        {
            List<Book> books =await _DBContext.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("getbook/{id}")]
        public async Task<IActionResult> GetBookById(int id) {

            Book? searched_book =await _DBContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (searched_book != null)
                return Ok(searched_book);
            else 
                return NotFound("Didn't find that value");
        }
        [HttpPost("newbook")]
        public async Task<IActionResult> CreateBook([Bind("Id","Title","YearPublished","Author") ] Book book)
        {   if (ModelState.IsValid)
            {
                await _DBContext.Books.AddAsync(book);
                await _DBContext.SaveChangesAsync();
                return Ok("Updates are done");
            }
            return BadRequest(ModelState);
        }
    }
}
