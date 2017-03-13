using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OpenCBS.CoreDomain.Messaging
{
    public partial class MessageTemplate
    {
        public MessageTemplate()
        {
            this.IsDefault = false; 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BccEmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailBody { get; set; }
        public bool IsActive { get; set; }

        public bool? SendEmail { get; set; }
        public bool? SendSMS { get; set; }
        public int EmailAccountId { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public bool Deleted { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }

        partial void CustomValidate(ValidationContext validationContext, ref IEnumerable<ValidationResult> validationResults);
        private IEnumerable<ValidationResult> CustomValidateWrapper(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var enumerable = results as IEnumerable<ValidationResult>;
            CustomValidate(validationContext, ref enumerable);
            return results;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CustomValidateWrapper(validationContext);
        }
    }
}

