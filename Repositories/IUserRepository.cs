using BookAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
    }
}
