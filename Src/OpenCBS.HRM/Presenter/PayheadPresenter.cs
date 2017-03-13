using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;
using OpenCBS.HRM.Interface.Presenter;
using OpenCBS.HRM.Interface.View;
using OpenCBS.HRM.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.HRM.Presenter
{
    public class PayHeadPresenter : IPayHeadPresenter, IPayHeadPresenterCallbacks
    {
        private readonly IfrmPayHead _view;
        private readonly IApplicationController _applicationController;

        public PayHeadPresenter(IfrmPayHead view, IApplicationController applicationController)
        {
            _view = view;
            _applicationController = applicationController;
        }

        public object View
        {
            get { return _view; }
        }

        public void Run()
        {
            _view.Attach(this);
            _applicationController.Publish(new ShowViewMessage(this, _view));
            _applicationController.Publish(new PayHeadShownMessage(this));
        }
    }
}
