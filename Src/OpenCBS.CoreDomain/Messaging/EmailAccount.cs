using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OpenCBS.CoreDomain.Messaging
{
    public partial class EmailAccount
    {
        public EmailAccount() 
        {
            this.MessageTemplates = new HashSet<MessageTemplate>();
        }
        
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public Nullable<bool> IsDefaultEmailAccount { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }

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

