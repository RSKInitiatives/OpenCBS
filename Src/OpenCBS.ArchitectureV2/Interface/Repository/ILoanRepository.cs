using System;
using System.Collections.Generic;
using System.Data;
using OpenCBS.ArchitectureV2.Model;

namespace OpenCBS.ArchitectureV2.Interface.Repository
{
    public interface ILoanRepository
    {
        List<Alert> GetAlerts(DateTime date, int userId);
        List<Loan> GetVillageBankLoans(int villageBankId);
        RepaymentEvent SaveRepaymentEvent(RepaymentEvent repaymentEvent, IDbTransaction tx);
        CloseEvent SaveCloseEvent(CloseEvent closeEvent, IDbTransaction tx);
        void ArchiveSchedule(int loanId, int eventId, List<Installment> oldSchedule, List<Installment> newSchedule, IDbTransaction tx);
        void SetLoanStatusToClosed(int loanId, IDbTransaction tx);
        void UpdateBorrowerStatus(int loanId, IDbTransaction tx);
    }
}
