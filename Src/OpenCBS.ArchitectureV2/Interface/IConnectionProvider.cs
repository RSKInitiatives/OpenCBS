using System.Data;

namespace OpenCBS.ArchitectureV2.Interface
{
    public interface IConnectionProvider
    {
        IDbConnection GetConnection();
        IDbTransaction GetTransaction();
    }
}
