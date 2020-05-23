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
    internal class SqlDataAccess : IDisposable
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
        IDbConnection _connection;
        IDbTransaction _transaction;
        public void StartTransaction(string connectinStringName)
        {
            string connectrinString = GetConnectionString(connectinStringName);
            _connection = new SqlConnection(connectrinString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            isClosed = false;
        }
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                 commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }
        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction);
        }
        private bool isClosed = false;
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
            isClosed = true;
        }
        public void RolbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
            isClosed = true;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch
                {

                    //TODO :Log This.
                }
            }
            _transaction = null;
            _connection = null;
        }
    }
}
