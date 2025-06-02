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
                _DBContext.Books.Add(book);
                await _DBContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBookById) , new {book.Id}, book );
                // this return type will give response that will give a detailed version of the book.
            }
            return BadRequest(ModelState);
        }


        [HttpPut("updatebook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book Updates)
        {
            Book the_book = await _DBContext.Books.FirstOrDefaultAsync( x => x.Id == id);

            if (the_book != null)
            {
                foreach (var prop in typeof(Book).GetProperties())
                {
                    string name = prop.Name;
                    object value = prop.GetValue(the_book);
                    Console.WriteLine($"{name}: {value}");
                    

                    
                }
                    //the_book.Id= Updates.Id;
                the_book.Title = Updates.Title ?? the_book.Title;
                the_book.Author = Updates.Author ?? the_book.Author;
                the_book.YearPublished = Updates.YearPublished ;
                await _DBContext.SaveChangesAsync();

                return   CreatedAtAction(nameof(GetBookById) , new {the_book
                    .Id
                }, Updates
                )  ;
            }
            else
            {
                return BadRequest(  ModelState +" \n the book isn't found");
            }
            
        }


        [HttpDelete("delbook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                Book? book = await _DBContext.Books.FindAsync(id);

                if (book != null) {
                     _DBContext.Books.Remove(book);
                     await _DBContext.SaveChangesAsync();


                    return Ok(book+"  \n deleted successfully");
                }
                else
                {
                    return BadRequest("The book is'nt found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("" + ex);
            }

            return BadRequest("Some error is there");
        }   
    }
}
