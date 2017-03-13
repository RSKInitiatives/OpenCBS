using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;

namespace OpenCBS.ArchitectureV2.Command
{
    public class BatchRepayCommand : ICommand<BatchRepayCommandData>
    {
        private readonly IBatchRepaymentPresenter _presenter;

        public BatchRepayCommand(IBatchRepaymentPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Execute(BatchRepayCommandData commandData)
        {
            _presenter.Run(commandData.VillageBankId);
        }
    }
}
