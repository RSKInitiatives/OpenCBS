/**
 *  Distributed as part of Free.iso8583
 *  
 *  Free.iso8583 is ISO 8583 Message Processor library that makes message parsing/compiling esier.
 *  It will convert ISO 8583 message to a model object and vice versa. So, the other parts of
 *  application will only do the rest effort to make business process done.
 *  
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License or (at your option) any later version. 
 *  See http://gnu.org/licenses/lgpl.html
 *
 *  Developed by AT Mulyana (atmulyana@yahoo.com) 2009-2015
 *  The latest update can be found at sourceforge.net
 **/
using System;
using System.Collections.Generic;
using System.Text;
using Free.iso8583.config.attribute;

namespace Free.iso8583.example.model
{
    public class Bit63Content
    {
        [BitContentField(Length = 2, PadChar = '0', Align = FieldAlignment.Right)]
        public String TableId { get; set; }

        [BitContentField(Length = 11, PadChar = '0', Align = FieldAlignment.Right, NullChar = ' ')]
		public String BeneficiaryInstitutionId { get; set; }

        [BitContentField(Length = 28, PadChar = ' ', Align = FieldAlignment.Left, NullChar = ' ', IsTrim = true)]
		public String BeneficiaryAccountNumber { get; set; }

        [BitContentField(Length = 30, PadChar = ' ', Align = FieldAlignment.Left, NullChar = ' ', IsTrim = true)]
		public String BeneficiaryName { get; set; }

        [BitContentField(Length = 16, PadChar = ' ', Align = FieldAlignment.Left, NullChar = ' ', IsTrim = true)]
		public String CustomerReferenceNumber { get; set; }

        [BitContentField(Length = 11, PadChar = '0', Align = FieldAlignment.Right, NullChar = ' ')]
		public String IssuerInstitutionId { get; set; }

        [BitContentField(Length = 28, PadChar = ' ', Align = FieldAlignment.Left, NullChar = ' ', IsTrim = true)]
		public String CardholderAccountNumber { get; set; }

        [BitContentField(Length = 30, PadChar = ' ', Align = FieldAlignment.Left, NullChar = ' ', IsTrim = true)]
		public String CardholderName { get; set; }

        [BitContentField(Length = 100, PadChar = ' ', Align = FieldAlignment.Left, NullChar = ' ', IsTrim = true, IsOptional = true)]
        public String InformationData { get; set; }
    }
}
