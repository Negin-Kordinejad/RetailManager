using DataMamagerClassLibrary.Internal.DataAccess;
using DataMamagerClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMamagerClassLibrary.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProductModel, dynamic>("spProduct_GetAll", new { }, "DataManager");
            return output;
        }
        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProductModel, dynamic>("spProduct_GetById", new {Id=productId }, "DataManager").FirstOrDefault();
            return output;
        }
    }
}
