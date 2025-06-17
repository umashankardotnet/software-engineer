using CRUD_API_Demo.Models;
using CRUD_API_Demo.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public List<BookModel> Get()
        {
            return bookService.GetAllBooks().ToList();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public BookModel Get(int id)
        {
            return bookService.GetBookById(id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult Post([FromBody] BookModel book)
        {
            if (book != null)
            {
                bookService.CreateBook(book);
            }
            return Ok();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(bookService.DeleteBook(id));
        }
    }
}
