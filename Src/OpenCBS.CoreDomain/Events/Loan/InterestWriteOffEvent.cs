using OpenCBS.Shared;

namespace OpenCBS.CoreDomain.Events.Loan
{
    public class InterestWriteOffEvent : Event
    {
        public override string Code
        {
            get { return "IWOE"; }
            set { base.Code = value; }
        }

        public OCurrency Amount { get; set; }
    }
}
