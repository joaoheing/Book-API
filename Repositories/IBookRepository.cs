using BookAPI.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get(int pageNumber, int pageSize, string sortBy);

        Task<Book> Get(int Id);

        Task<Book> Create(Book book);

        Task Update(Book book);

        Task Delete(int Id);
    }
}
