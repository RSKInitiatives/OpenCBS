using System.Collections.Generic;
using System.Linq;

namespace OpenCBS.ArchitectureV2.Model
{
    public class Loan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContractCode { get; set; }
        public List<Installment> Schedule { get; set; }

        public Loan Copy()
        {
            var result = (Loan) MemberwiseClone();
            result.Schedule = Schedule.Select(x => x.Copy()).ToList();
            return result;
        }
    }
}
