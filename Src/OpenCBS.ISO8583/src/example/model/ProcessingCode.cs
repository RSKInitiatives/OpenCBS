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

namespace Free.iso8583.example.model
{
    public class ProcessingCode
    {
        public static readonly IDictionary<TransactionTypeCodeBytes, String> TRANSACTION_TYPE = new Dictionary<TransactionTypeCodeBytes, String>() {
            {TransactionTypeCodeBytes.LOGON, "Logon"},
            {TransactionTypeCodeBytes.CASH_WITHDRAWAL, "Cash Withdrawal"},
            {TransactionTypeCodeBytes.BALANCE_INQUIRY, "Balance Inquiry"},
            {TransactionTypeCodeBytes.TRANSFER_INQUIRY, "Transfer Inquiry"},
            {TransactionTypeCodeBytes.TRANSFER, "Transfer"},
            {TransactionTypeCodeBytes.PAYMENT_INQUIRY, "Payment Inquiry"},
            {TransactionTypeCodeBytes.PAYMENT, "Payment"},
            {TransactionTypeCodeBytes.PIN_CHANGE, "PIN/Password Change"},
        };
        public static readonly IDictionary<TransactionTypeCodeBytes, String> TRANSACTION_TYPE_CODE_STRING = new Dictionary<TransactionTypeCodeBytes, String>() {
            {TransactionTypeCodeBytes.LOGON, TransactionTypeCode.LOGON},
            {TransactionTypeCodeBytes.CASH_WITHDRAWAL, TransactionTypeCode.CASH_WITHDRAWAL},
            {TransactionTypeCodeBytes.BALANCE_INQUIRY, TransactionTypeCode.BALANCE_INQUIRY},
            {TransactionTypeCodeBytes.TRANSFER_INQUIRY, TransactionTypeCode.TRANSFER_INQUIRY},
            {TransactionTypeCodeBytes.TRANSFER, TransactionTypeCode.TRANSFER},
            {TransactionTypeCodeBytes.PAYMENT_INQUIRY, TransactionTypeCode.PAYMENT_INQUIRY},
            {TransactionTypeCodeBytes.PAYMENT, TransactionTypeCode.PAYMENT},
            {TransactionTypeCodeBytes.PIN_CHANGE, TransactionTypeCode.PIN_CHANGE},
        };
        public static readonly IDictionary<AccountCodeBytes, String> ACCOUNT = new Dictionary<AccountCodeBytes, String>() {
            {AccountCodeBytes.DEFAULT_ACCOUNT, "Default Code (Account Not Specified or Not Applicable)"},
            {AccountCodeBytes.SAVINGS_ACCOUNT, "Savings Account"},
            {AccountCodeBytes.CHECKING_ACCOUNT, "Checking Account"},
            {AccountCodeBytes.CREDIT_CARD_ACCOUNT, "Credit Card Account"},
            {AccountCodeBytes.CASH, "Cash"},
        };
        public static readonly IDictionary<AccountCodeBytes, String> ACCOUNT_CODE_STRING = new Dictionary<AccountCodeBytes, String>() {
            {AccountCodeBytes.DEFAULT_ACCOUNT, AccountCode.DEFAULT_ACCOUNT},
            {AccountCodeBytes.SAVINGS_ACCOUNT, AccountCode.SAVING_ACCOUNT},
            {AccountCodeBytes.CHECKING_ACCOUNT, AccountCode.CHECKING_ACCOUNT},
            {AccountCodeBytes.CREDIT_CARD_ACCOUNT, AccountCode.CREDIT_CARD_ACCOUNT},
            {AccountCodeBytes.CASH, AccountCode.CASH},
        };

        private byte[] _bytes = new byte[3];
        private TransactionTypeCodeBytes _transactionType;
        private String _transactionTypeCodeString;
        private String _transactionTypeDescription;
        private AccountCodeBytes _fromAccount;
        private String _fromAccountCodeString;
        private String _fromAccountDescription;
        private AccountCodeBytes _toAccount;
        private String _toAccountCodeString;
        private String _toAccountDescription;

        public ProcessingCode()
        {
            //Using default values
            TransactionType = _transactionType;
            FromAccount = _fromAccount;
            ToAccount = _toAccount;
        }

        public ProcessingCode(byte[] bytes)
        {
            this.BytesValue = bytes;
        }

        public TransactionTypeCodeBytes TransactionType
        { 
            get { return _transactionType; }
            set
            {
                _transactionType = value;
                _transactionTypeCodeString = MessageUtility.HexToString((byte)value);
                TransactionTypeDescription = null; //null is dummy value. The real value will depend on _transactionType
            }
        }
        public String TransactionTypeCodeString
        { 
            get { return _transactionTypeCodeString; }
            set
            {
                byte[] val = MessageUtility.StringToHex(value);
                if (val == null || val.Length != 1)
                {
                    throw new MessageProcessorException("ProcessingCode.TransactionTypeCodeString: Invalid value.");
                }
                _transactionType = (TransactionTypeCodeBytes)val[0];
                _transactionTypeCodeString = value.ToUpper().PadLeft(2, '0');
                TransactionTypeDescription = null; //null is dummy value. The real value will depend on _transactionType
            }
        }
        public String TransactionTypeDescription
        {
            get { return _transactionTypeDescription; }
            private set
            {
                _transactionTypeDescription = TRANSACTION_TYPE.ContainsKey(_transactionType) ? TRANSACTION_TYPE[_transactionType] : null;
                _bytes[0] = (byte)_transactionType;
            }
        }
        
        public AccountCodeBytes FromAccount
        { 
            get { return _fromAccount; }
            set
            {
                _fromAccount = value;
                _fromAccountCodeString = MessageUtility.HexToString((byte)value);
                FromAccountDescription = null; //null is dummy value. The real value will depend on _fromAccount
            }
        }
        public String FromAccountCodeString
        {
            get { return _fromAccountCodeString; }
            set
            {
                byte[] val = MessageUtility.StringToHex(value);
                if (val == null || val.Length != 1)
                {
                    throw new MessageProcessorException("ProcessingCode.FromAccountCodeString: Invalid value.");
                }
                _fromAccount = (AccountCodeBytes)val[0];
                _fromAccountCodeString = value.ToUpper().PadLeft(2, '0');
                FromAccountDescription = null; //null is dummy value. The real value will depend on _fromAccount
            }
        }
        public String FromAccountDescription
        {
            get { return _fromAccountDescription; }
            private set
            {
                _fromAccountDescription = ACCOUNT.ContainsKey(_fromAccount) ? ACCOUNT[_fromAccount] : null;
                _bytes[1] = (byte)_fromAccount;
            }
        }
        
        public AccountCodeBytes ToAccount
        {
            get { return _toAccount; }
            set
            {
                _toAccount = value;
                _toAccountCodeString = MessageUtility.HexToString((byte)value);
                ToAccountDescription = null; //null is dummy value. The real value will depend on _toAccount
            }
        }
        public String ToAccountCodeString
        {
            get { return _toAccountCodeString; }
            set
            {
                byte[] val = MessageUtility.StringToHex(value);
                if (val == null || val.Length != 1)
                {
                    throw new MessageProcessorException("ProcessingCode.ToAccountCodeString: Invalid value.");
                }
                _toAccount = (AccountCodeBytes)val[0];
                _toAccountCodeString = value.ToUpper().PadLeft(2, '0');
                ToAccountDescription = null; //null is dummy value. The real value will depend on _toAccount
            }
        }
        public String ToAccountDescription
        {
            get { return _toAccountDescription; }
            private set
            {
                _toAccountDescription = ACCOUNT.ContainsKey(_toAccount) ? ACCOUNT[_toAccount] : null;
                _bytes[2] = (byte)_toAccount;
            }
        }
        

        public byte[] BytesValue
        {
            get { return _bytes;  }
            set
            {
                if (value == null || value.Length < 3)
                    throw new MessageProcessorException("ProcessingCode.BytesValue: Bytes array length must be 3.");
                Array.Copy(value, 0, _bytes, 0, 3);
                TransactionType = (TransactionTypeCodeBytes)_bytes[0];
                FromAccount = (AccountCodeBytes)_bytes[1];
                ToAccount = (AccountCodeBytes)_bytes[2];
            }
        }

        public override string ToString()
        {
            return TransactionTypeCodeString + FromAccountCodeString + ToAccountCodeString;
        }

        public static implicit operator byte[](ProcessingCode pc)
        {
            return pc.BytesValue;
        }

        public static implicit operator ProcessingCode(byte[] bytes)
        {
            return new ProcessingCode(bytes);
        }
    }
}
