using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Message;
using OpenCBS.ArchitectureV2.Model;
using OpenCBS.CoreDomain;
using OpenCBS.Extensions;
using OpenCBS.Shared;

namespace OpenCBS.ArchitectureV2.Presenter
{
    public class BatchRepaymentPresenter : IBatchRepaymentPresenter, IBatchRepaymentPresenterCallbacks
    {
        private readonly IBatchRepaymentView _view;
        private readonly ILoanRepository _loanRepository;
        private readonly IVillageBankRepository _villageBankRepository;
        private readonly IEventInterceptor _eventInterceptor;
        private readonly IConnectionProvider _connectionProvider;
        private readonly IApplicationController _applicationController;
        private List<Loan> _loans;
        private int _villageBankId;

        public BatchRepaymentPresenter(
            IBatchRepaymentView view,
            ILoanRepository loanRepository,
            IVillageBankRepository villageBankRepository,
            IEventInterceptor eventInterceptor,
            IConnectionProvider connectionProvider,
            IApplicationController applicationController)
        {
            _view = view;
            _loanRepository = loanRepository;
            _villageBankRepository = villageBankRepository;
            _eventInterceptor = eventInterceptor;
            _connectionProvider = connectionProvider;
            _applicationController = applicationController;
        }

        public void Run(int villageBankId)
        {
            _villageBankId = villageBankId;
            _view.Attach(this);
            _loans = _loanRepository.GetVillageBankLoans(villageBankId);
            _view.SetLoans(_loans);
            _view.Run();
        }

        public object View
        {
            get { return _view; }
        }

        private Loan GetLoan(int loanId)
        {
            var loan = _loans.Find(x => x.Id == loanId);
            if (loan == null)
            {
                throw new ApplicationException(string.Format("Loan identified by id={0} not found.", loanId));
            }
            return loan;
        }

        public decimal GetDuePrincipal(int loanId)
        {
            var loan = GetLoan(loanId);
            var date = TimeProvider.Today;
            return loan
                .Schedule
                .FindAll(x => x.ExpectedDate <= date)
                .Sum(x => x.Principal - x.PaidPrincipal);
        }

        public decimal GetDueInterest(int loanId)
        {
            var loan = GetLoan(loanId);
            var date = TimeProvider.Today;
            return loan
                .Schedule
                .FindAll(x => x.ExpectedDate <= date)
                .Sum(x => x.Interest - x.PaidInterest);
        }

        public decimal[] DistributeTotal(int loanId, decimal total)
        {
            var loan = GetLoan(loanId).Copy();
            var installments = loan.Schedule.Where(x => !x.Repaid).ToList();
            var principal = 0m;
            var interest = 0m;
            foreach (var installment in installments)
            {
                if (installment.UnpaidInterest > total)
                {
                    interest += total;
                    break;
                }
                total -= installment.UnpaidInterest;
                interest += installment.UnpaidInterest;
                installment.PaidInterest = installment.Interest;

                if (installment.UnpaidPrincipal > total)
                {
                    principal += total;
                    break;
                }
                total -= installment.UnpaidPrincipal;
                principal += installment.UnpaidPrincipal;
                installment.PaidPrincipal = installment.Principal;
            }

            return new[] { principal, interest };
        }

        private static void DistributeTotal(Loan loan, decimal total)
        {
            var installments = loan.Schedule.Where(x => !x.Repaid).ToList();
            var date = TimeProvider.Now;
            foreach (var installment in installments)
            {
                if (installment.UnpaidInterest > total)
                {
                    installment.PaidInterest += total;
                    installment.PaymentDate = date;
                    break;
                }
                total -= installment.UnpaidInterest;
                installment.PaidInterest = installment.Interest;
                installment.PaymentDate = date;

                if (installment.UnpaidPrincipal > total)
                {
                    installment.PaidPrincipal += total;
                    installment.PaymentDate = date;
                    break;
                }
                total -= installment.UnpaidPrincipal;
                installment.PaidPrincipal = installment.Principal;
                installment.PaymentDate = date;

                if (total == 0)
                {
                    break;
                }
            }
        }

        public decimal GetMaxDueTotal(int loanId)
        {
            var loan = GetLoan(loanId);
            return loan.Schedule.Sum(x => x.Principal + x.Interest - x.PaidPrincipal - x.PaidInterest);
        }

        private static RepaymentEvent GetRepaymentEvent(Loan loan, Loan repaidLoan)
        {
            var firstUnpaidInstallment = loan.Schedule.Find(x => !x.Repaid);
            var lateDays = (TimeProvider.Today - firstUnpaidInstallment.ExpectedDate.Date).Days;
            lateDays = lateDays < 0 ? 0 : lateDays;

            var repaymentEvent = new RepaymentEvent();
            repaymentEvent.LoanId = loan.Id;
            repaymentEvent.Code = lateDays > 0 ? "RBLE" : "RGLE";
            repaymentEvent.InstallmentNumber = firstUnpaidInstallment.Number;
            repaymentEvent.EventDate = TimeProvider.Now;
            repaymentEvent.UserId = User.CurrentUser.Id;
            repaymentEvent.LateDays = lateDays;

            repaymentEvent.Principal = repaidLoan.Schedule.Sum(x => x.PaidPrincipal) - loan.Schedule.Sum(x => x.PaidPrincipal);
            repaymentEvent.Interest = repaidLoan.Schedule.Sum(x => x.PaidInterest) - loan.Schedule.Sum(x => x.PaidInterest);
            
            return repaymentEvent;
        }

        private static CloseEvent GetCloseEvent(Loan loan)
        {
            var closeEvent = new CloseEvent();
            closeEvent.Code = "LOCE";
            closeEvent.LoanId = loan.Id;
            closeEvent.UserId = User.CurrentUser.Id;
            closeEvent.EventDate = TimeProvider.Now;
            return closeEvent;
        }

        public void Repay()
        {
            var tx = _connectionProvider.GetTransaction();
            try
            {
                foreach (var id in _view.SelectedLoanIds)
                {
                    var total = _view.GetTotal(id);
                    var loan = GetLoan(id);
                    var repaidLoan = loan.Copy();
                    DistributeTotal(repaidLoan, total);

                    var repaymentEvent = GetRepaymentEvent(loan, repaidLoan);
                    repaymentEvent = _loanRepository.SaveRepaymentEvent(repaymentEvent, tx);
                    if (_eventInterceptor != null)
                        _eventInterceptor.CallInterceptor(new Dictionary<string, object>
                            {
                                {"Loan", loan},
                                {"RepaymentEvent", repaymentEvent},
                                {"IDbTransaction", tx}
                            });
                    _loanRepository.ArchiveSchedule(loan.Id, repaymentEvent.Id, loan.Schedule, repaidLoan.Schedule, tx);

                    var closed = repaidLoan.Schedule.Last().Repaid;
                    if (closed)
                    {
                        var closeEvent = GetCloseEvent(repaidLoan);
                        _loanRepository.SaveCloseEvent(closeEvent, tx);
                        _loanRepository.SetLoanStatusToClosed(loan.Id, tx);
                        _loanRepository.UpdateBorrowerStatus(loan.Id, tx);
                    }
                }
                _villageBankRepository.SyncVillageBankStatus(_villageBankId, tx);
                tx.Commit();

                _view.Stop();
                _applicationController.Publish(new VillageBankRepayMessage(this, _villageBankId));
            }
            catch (Exception error)
            {
                tx.Rollback();
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
