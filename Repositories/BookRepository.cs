using BookAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        public readonly BookContext _context;
        public BookRepository(BookContext context) 
        {
            _context = context;
        }
        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
           await _context.SaveChangesAsync();

            return book;
        }

        public async Task Delete(int Id)
        {
            Book bookToDelete = await _context.Books.FindAsync(Id);
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> Get(int pageNumber, int pageSize, string sortBy)
        {
            IQueryable<Book> booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "author":
                        booksQuery = booksQuery.OrderBy(b => b.Author);
                        break;
                    case "title":
                        booksQuery = booksQuery.OrderBy(b => b.Title);
                        break;
                    default:
                        booksQuery = booksQuery.OrderBy(b => b.Id);
                        sortBy = null;
                        break;
                }
            }

            return await booksQuery.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();
        }




        public async Task<Book> Get(int Id)
        {
            return await _context.Books.FindAsync(Id);
        }

        public async Task Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
