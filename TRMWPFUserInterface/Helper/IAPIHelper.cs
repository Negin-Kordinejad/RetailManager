using System.Threading.Tasks;
using TRMWPFUserInterface.Models;

namespace TRMWPFUserInterface.Helper
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}