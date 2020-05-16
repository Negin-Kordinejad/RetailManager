using DesktopUILibrary.Models;
using System.Net.Http;
using System.Threading.Tasks;


namespace DesktopUILibrary.Api
{
    public interface IAPIHelper
    {
        HttpClient AppClient { get; }
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}