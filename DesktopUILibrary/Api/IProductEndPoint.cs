using DesktopUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopUILibrary.Api
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}