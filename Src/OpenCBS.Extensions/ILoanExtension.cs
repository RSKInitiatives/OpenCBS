using System.Data.SqlClient;
using OpenCBS.CoreDomain.Contracts.Loans;

namespace OpenCBS.Extensions
{
    public interface ILoanExtension
    {
        void Save(Loan loan, SqlTransaction transaction);
    }
}
