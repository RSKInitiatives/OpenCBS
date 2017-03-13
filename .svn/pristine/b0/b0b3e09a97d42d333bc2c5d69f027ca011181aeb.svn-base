using OpenCBS.ArchitectureV2.CommandData;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Presenter;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Interface.View;
using OpenCBS.ArchitectureV2.Message;

namespace OpenCBS.ArchitectureV2.Presenter
{
    public class VillageBankPresenter : IVillageBankPresenter, IVillageBankPresenterCallbacks
    {
        private readonly IVillageBankView _view;
        private readonly IApplicationController _applicationController;
        private readonly IVillageBankRepository _villageBankRepository;
        private int _villageBankId;

        public VillageBankPresenter(
            IVillageBankView view,
            IApplicationController applicationController,
            IVillageBankRepository villageBankRepository)
        {
            _view = view;
            _applicationController = applicationController;
            _villageBankRepository = villageBankRepository;
        }

        public void Run(int villageBankId)
        {
            _villageBankId = villageBankId;
            _view.Attach(this);
            _view.SetVillageBank(_villageBankRepository.Get(villageBankId));
            _applicationController.Publish(new ShowViewMessage(this, _view));
            _applicationController.Subscribe<VillageBankRepayMessage>(this, m =>
                {
                    if (m.VillageBankId == _villageBankId)
                        _view.SetVillageBank(_villageBankRepository.Get(_villageBankId));
                });
        }

        public object View
        {
            get { return _view; }
        }

        public void Repay()
        {
            _applicationController.Execute(new BatchRepayCommandData { VillageBankId = _villageBankId });
        }
    }
}
