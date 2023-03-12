using BookAPI.Model;
using BookAPI.Repositories;
using BookAPI;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;
    private readonly AuthenticationSettings _authenticationSettings;

    public UserRepository(UserContext context, IOptions<AuthenticationSettings> authenticationSettings)
    {
        _context = context;
        _authenticationSettings = authenticationSettings.Value;
    }
    public async Task<User> Authenticate(string username, string password)
    {
        if (username != _authenticationSettings.UserName || password != _authenticationSettings.Password)
        {
            return null;
        }

        return new User { Username = username };
    }

}
