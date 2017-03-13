using System.Data.SqlClient;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.Extension.Tasks;


namespace OpenCBS.Extensions
{
    public class LoanExtension : ILoanExtension
    {
        public LoanExtension() { }

        public void Save(Loan loan, SqlTransaction transaction)
        {
            var taskBuilder = new TaskBuilder();
            taskBuilder.CreateLoanApprovalTasks(loan);
        }
    }
}
