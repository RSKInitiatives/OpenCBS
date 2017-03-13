//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenCBS.CoreDomain.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class QueuedEmail
    {
        public int Id { get; set; }
        public Nullable<int> Priority { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToName { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> SentOnUtc { get; set; }
        public int EmailAccountId { get; set; }
        public Nullable<int> SentTries { get; set; }
        public string Subject { get; set; }
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