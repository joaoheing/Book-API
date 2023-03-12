using BookAPI.Model;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ClaimsPrincipal user = User;

            if (!user.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            Book book = await _bookRepository.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.Delete(id);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
            ClaimsPrincipal user = User;

            if (!user.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            if (id != book.Id)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);
            return NoContent();
        }
    }
}
