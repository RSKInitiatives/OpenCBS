namespace OpenCBS.ArchitectureV2.Model
{
    public class VillageBankMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LoanCycle { get; set; }
        public string Passport { get; set; }
        public bool Active { get; set; }
    }
}
