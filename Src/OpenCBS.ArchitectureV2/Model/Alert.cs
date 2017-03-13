using System;
using System.Drawing;
using OpenCBS.Enums;

namespace OpenCBS.ArchitectureV2.Model
{
    public class Alert
    {
        public enum AlertKind
        {
            Loan = 1,
            Saving = 2,
            Client = 3
        }

        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BranchName { get; set; }
        public AlertKind Kind { get; set; }
        public string ContractCode { get; set; }
        public OContractStatus Status { get; set; }
        public int LateDays { get; set; }
        public string LoanOfficer { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        

        //slycode
        public AlertKind Type { get; set; }
        public OClientStatus ClientStatus { get; set; }
        public string IDNumber { get; set; }
        public string Active { get; set; }

        public Color BackColor
        {
            get
            {
                if (LateDays >= 365) return Color.FromArgb(226, 0, 26);
                if (LateDays >= 180) return Color.FromArgb(255, 92, 92);
                if (LateDays >= 90) return Color.FromArgb(255, 187, 120);
                if (LateDays >= 60) return Color.FromArgb(147, 181, 167);
                if (LateDays >= 30) return Color.FromArgb(188, 209, 199);
                if (LateDays > 0) return Color.FromArgb(217, 229, 223);
                //if (Status == OContractStatus.Pending && Kind == AlertKind.Saving) return Color.Orange;
                return Color.White;
            }
        }

        public Color ForeColor
        {
            get
            {
                if (LateDays >= 365) return Color.White;
                return Color.Black;
            }
        }

        public bool IsLoan
        {
            get { return Kind == AlertKind.Loan; }
        }

        public bool IsSaving
        {
            get { return Kind == AlertKind.Saving; }
        }

        public bool IsClient
        {
            get { return Kind == AlertKind.Client; }
        }
    }
}
