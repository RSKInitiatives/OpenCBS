using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Guarantees;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Enums;

namespace OpenCBS.Extensions
{
    public interface ILoanApprovalControl
    {
        Control GetControl();

        void Init(IClient client, Loan loan, Guarantee guarantee, SavingBookContract savings,
                  IList<IPrintButtonContextMenuStrip> printMenus);

        string Comment { get; set; }
        string Code { get; set; }
        DateTime Date { get; set; }
        OContractStatus Status { get; set; }
        Action SaveLoanApproval { get; set; }
    }
}
