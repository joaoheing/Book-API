using BookAPI.Model;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortBy = "Title")
        {
            IEnumerable<Book> books = await _bookRepository.Get(pageNumber, pageSize, sortBy);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            Book book = await _bookRepository.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book newBook = await _bookRepository.Create(book);
            return RedirectToAction(nameof(GetById), new { id = newBook.Id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Book book = await _bookRepository.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
           if (id != book.Id)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);
            return NoContent();
        }
    }
}
