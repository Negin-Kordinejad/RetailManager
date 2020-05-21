using DesktopUILibrary.Models;
using System.Threading.Tasks;

namespace DesktopUILibrary.Api
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}