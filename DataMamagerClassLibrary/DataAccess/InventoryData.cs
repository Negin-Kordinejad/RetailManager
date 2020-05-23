using DataMamagerClassLibrary.Internal.DataAccess;
using DataMamagerClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMamagerClassLibrary.DataAccess
{
    public class InventoryData
    {
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<InventoryModel, dynamic>("spInventory_GetAll", new { }, "DataManager");
            return output;
        }
        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData<InventoryModel>("spInventory_Insert", item, "DataManager");
        }
    }
}
