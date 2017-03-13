using System;
using System.Data;
using System.Data.SqlClient;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.CoreDomain;

namespace OpenCBS.ArchitectureV2
{
    public class ConnectionProvider : IConnectionProvider
    {
        public IDbConnection GetConnection()
        {
            return DatabaseConnection.GetConnection();
        }

        public IDbTransaction GetTransaction()
        {
            var connection = GetConnection();
            return connection.BeginTransaction();
        }
    }
}
