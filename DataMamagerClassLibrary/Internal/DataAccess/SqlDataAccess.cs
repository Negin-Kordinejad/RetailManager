using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMamagerClassLibrary.Internal.DataAccess
{
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectinStringName)
        {
            string connectrinString = GetConnectionString(connectinStringName);
            using (IDbConnection connection = new SqlConnection(connectrinString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }
        public void SaveData<T>(string storedProcedure, T parameters, string connectinStringName)
        {
            string connectrinString = GetConnectionString(connectinStringName);
            using (IDbConnection connection = new SqlConnection(connectrinString))
            {
                connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
