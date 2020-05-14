using DataMamagerClassLibrary.Internal.DataAccess;
using DataMamagerClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMamagerClassLibrary.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { Id = Id };
            var output = sql.LoadData<UserModel, dynamic>("spUserLookUp", p, "DataManager");
            return output;
        }
    }
}
