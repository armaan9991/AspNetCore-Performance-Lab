using Shared.Models;

namespace Api.Controllers.Services
{
    public interface IJwttokenGenerator
    {
        string GenerateToken(User user);
    }
}
