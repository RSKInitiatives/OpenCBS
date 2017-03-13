using System;

namespace OpenCBS.ArchitectureV2.Model
{
    public class Installment
    {
        public int ContractId { get; set; }
        public int Number { get; set; }
        public decimal Principal { get; set; }
        public decimal PaidPrincipal { get; set; }
        public decimal Interest { get; set; }
        public decimal PaidInterest { get; set; }
        public decimal Olb { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        public bool Repaid
        {
            get { return PaidPrincipal + PaidInterest >= Principal + Interest; }
        }

        public decimal UnpaidPrincipal
        {
            get { return Principal > PaidPrincipal ? Principal - PaidPrincipal : 0; }
        }

        public decimal UnpaidInterest
        {
            get { return Interest > PaidInterest ? Interest - PaidInterest : 0; }
        }

        public Installment Copy()
        {
            return (Installment) MemberwiseClone();
        }
    }
}
