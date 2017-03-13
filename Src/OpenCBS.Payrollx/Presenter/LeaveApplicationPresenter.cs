using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Message;
using OpenCBS.Payroll.Interface.Presenter;
using OpenCBS.Payroll.Interface.View;
using OpenCBS.Payroll.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.Payroll.Presenter
{
    public class LeaveApplicationPresenter : ILeaveApplicationPresenter, ILeaveApplicationPresenterCallbacks
    {
        private readonly ILeaveApplicationView _view;
        private readonly IApplicationController _applicationController;

        public LeaveApplicationPresenter(ILeaveApplicationView view, IApplicationController applicationController)
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
            _applicationController.Publish(new LeaveApplicationShownMessage(this));
        }
    }
}
