namespace OpenCBS.ArchitectureV2.Interface.Presenter
{
    public interface IBatchRepaymentPresenterCallbacks
    {
        decimal GetDuePrincipal(int loanId);
        decimal GetDueInterest(int loanId);
        decimal GetMaxDueTotal(int loanId);
        decimal[] DistributeTotal(int loanId, decimal total);
        void Repay();
    }
}
