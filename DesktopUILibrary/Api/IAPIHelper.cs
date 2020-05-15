using DesktopUILibrary.Models;
using System.Threading.Tasks;


namespace DesktopUILibrary.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}