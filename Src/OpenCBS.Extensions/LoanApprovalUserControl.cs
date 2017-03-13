using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Guarantees;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.CoreDomain.Contracts.Savings;
using OpenCBS.Enums;
using OpenCBS.Reports;
using OpenCBS.Shared.Settings;

namespace OpenCBS.Extensions
{
    [Export(typeof(ILoanApprovalControl))]
    [ExportMetadata("Implementation", "Default")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LoanApprovalUserControl : UserControl, ILoanApprovalControl
    {
        public LoanApprovalUserControl()
        {
            InitializeComponent();

            var statuses = new[]
            {
                OContractStatus.Pending,
                OContractStatus.Validated,
                OContractStatus.Refused,
                OContractStatus.Abandoned,
                OContractStatus.Deleted,
                OContractStatus.Postponed
            };
            var resourceManager = new ResourceManager("OpenCBS.Extensions.Resources.Extensions", GetType().Assembly);
            var dict = statuses.ToDictionary(x => x, x => resourceManager.GetString(x.ToString()));
            _statusComboBox.DisplayMember = "Value";
            _statusComboBox.ValueMember = "Key";
            _statusComboBox.DataSource = new BindingSource(dict, null);
            _statusComboBox.SelectedIndex = 0;

            _saveButton.Click += (sender, args) =>
            {
                if (SaveLoanApproval == null) return;
                SaveLoanApproval();
            };

            this._dateTimePicker.Format = DateTimePickerFormat.Custom;
            this._dateTimePicker.CustomFormat = ApplicationSettings.GetInstance("").SHORT_DATE_FORMAT;
        }

        public Control GetControl()
        {
            return this;
        }

        public void Init(IClient client, Loan loan, Guarantee guarantee, SavingBookContract savings, IList<IPrintButtonContextMenuStrip> printMenus)
        {
            _printButton.AttachmentPoint = AttachmentPoint.CreditCommittee;
            Visibility visibility;
            switch (client.Type)
            {
                case OClientTypes.Person:
                    visibility = Visibility.Individual;
                    break;

                case OClientTypes.Group:
                    visibility = Visibility.Group;
                    break;

                case OClientTypes.Corporate:
                    visibility = Visibility.Corporate;
                    break;

                default:
                    visibility = Visibility.All;
                    break;
            }
            _printButton.Visibility = visibility;

            _printButton.ReportInitializer =
                report =>
                {
                    report.SetParamValue("user_id", User.CurrentUser.Id);
                    if (loan != null) report.SetParamValue("contract_id", loan.Id);
                };
            _printButton.LoadReports();

            foreach (var item in printMenus)
            {
                var menuItems = item.GetContextMenuStrip(client, loan, guarantee, savings, AttachmentPoint.CreditCommittee.ToString());
                if (menuItems == null) continue;

                foreach (var menuItem in menuItems)
                {
                    _printButton.Menu.Items.Add(menuItem);
                }
            }
        }

        public string Comment
        {
            get { return _commentTextBox.Text; }
            set { _commentTextBox.Text = value; }
        }

        public string Code
        {
            get { return _codeTextBox.Text; }
            set { _codeTextBox.Text = value; }
        }

        public DateTime Date
        {
            get { return _dateTimePicker.Value; }
            set { _dateTimePicker.Value = value; }
        }

        public Action SaveLoanApproval { get; set; }

        public OContractStatus Status
        {
            get { return (OContractStatus)_statusComboBox.SelectedValue; }
            set
            {
                switch (value)
                {
                    case OContractStatus.Pending:
                    case OContractStatus.Postponed:
                    case OContractStatus.Refused:
                    case OContractStatus.Abandoned:
                    case OContractStatus.Deleted:
                    case OContractStatus.NonAccrual:
                        _statusComboBox.SelectedValue = value;
                        break;

                    case OContractStatus.Active:
                    case OContractStatus.Closed:
                    case OContractStatus.WrittenOff:
                    case OContractStatus.Validated:
                        _statusComboBox.SelectedValue = OContractStatus.Validated;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(string.Format("No such status: {0}", value.GetName()));
                }

                switch (value)
                {
                    case OContractStatus.Active:
                    case OContractStatus.Closed:
                    case OContractStatus.WrittenOff:
                        SetEnabled(false);
                        break;

                    default:
                        SetEnabled(true);
                        break;
                }
            }
        }

        private void SetEnabled(bool enabled)
        {
            _statusComboBox.Enabled = enabled;
            _codeTextBox.Enabled = enabled;
            _commentTextBox.Enabled = enabled;
            _dateTimePicker.Enabled = enabled;
            _saveButton.Enabled = enabled;
        }
    }
}
