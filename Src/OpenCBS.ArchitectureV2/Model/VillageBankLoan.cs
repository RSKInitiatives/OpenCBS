namespace OpenCBS.ArchitectureV2.Model
{
    public class VillageBankLoan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoanProductName { get; set; }
        public string InstallmentTypeName { get; set; }
        public int Duration { get; set; }
        public string ContractCode { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public decimal Olb { get; set; }
    }
}
