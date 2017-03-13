using System;
using System.Collections.Generic;
using Free.iso8583;
using Free.iso8583.example.model;

namespace Iso8583WebClient
{
    public static class EditedItems
    {
        private static Dictionary<String, EditedItem> _editedItems = new Dictionary<String, EditedItem>()
        {
            { "Transfer Inquiry",
                new EditedItem() {
                    Panel = "_PanelTrans",
                    DefaultModel = new ReqTransferInquiry0100() {
                        PrimaryAccountNumber = null,
                        ProcessingCode = new byte[] { 0x39, 0x10, 0x00 },
                        TransactionAmount = 7250000.00m,
                        SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
                        ExpirationDate = null,
                        PosEntryMode = new byte[] { 0x00, 0x21 },
                        NetworkInternationalId = "005",
                        PosConditionCode = new byte[] { 0x00 },
                        Track2Data = new byte[] { 0x49, 0x91, 0x87, 0x02, 0x73, 0x00, 0x27, 0x3C,
                            0xD6, 0x2B, 0x27, 0x1A, 0x0A, 0x38, 0x08, 0x00, 0x80, 0x12, 0x40 },
                        TerminalId = "12341234",
                        MerchantId = "123451234512345",
                        AdditionalData = null,
                        CardholderPinBlock = new byte[] { 0x77, 0xBB, 0xAA, 0x66, 0x78, 0x3B, 0xD7, 0xCC },
                        InvoiceNumber = "003107",
                        TransferData = new Bit63Content {
                            TableId = "78",
		                    BeneficiaryInstitutionId = "00000002314",
		                    BeneficiaryAccountNumber = "0123456789012345",
		                    BeneficiaryName = null,
		                    CustomerReferenceNumber = null,
		                    IssuerInstitutionId = null,
		                    CardholderAccountNumber = null,
		                    CardholderName = null,
                            InformationData = null
                        },
                        MessageAuthenticationCode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00 }
                    }
                }
            },

            { "Transfer",
                new EditedItem() {
                    Panel = "_PanelTrans",
                    DefaultModel = new ReqTransfer0200() {
                        PrimaryAccountNumber = null,
                        ProcessingCode = new byte[] { 0x40, 0x10, 0x00 },
                        TransactionAmount = 7250000.00m,
                        SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
                        ExpirationDate = null,
                        PosEntryMode = new byte[] { 0x00, 0x21 },
                        NetworkInternationalId = "005",
                        PosConditionCode = new byte[] { 0x00 },
                        Track2Data = new byte[] { 0x49, 0x91, 0x87, 0x02, 0x73, 0x00, 0x27, 0x3C,
                            0xD6, 0x2B, 0x27, 0x1A, 0x0A, 0x38, 0x08, 0x00, 0x80, 0x12, 0x40 },
                        TerminalId = "12341234",
                        MerchantId = "123451234512345",
                        AdditionalData = null,
                        CardholderPinBlock = new byte[] { 0x77, 0xBB, 0xAA, 0x66, 0x78, 0x3B, 0xD7, 0xCC },
                        InvoiceNumber = "003107",
                        TransferData = new Bit63Content {
                            TableId = "78",
		                    BeneficiaryInstitutionId = "00000002314",
		                    BeneficiaryAccountNumber = "0123456789012345",
		                    BeneficiaryName = null,
		                    CustomerReferenceNumber = null,
		                    IssuerInstitutionId = null,
		                    CardholderAccountNumber = null,
		                    CardholderName = null,
                            InformationData = null
                        },
                        MessageAuthenticationCode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00 }
                    }
                }
            },

            { "TransferSB",
                new EditedItem() {
                    Panel = "_PanelTrans",
                    DefaultModel = new Request0200SB() {
                        PrimaryAccountNumber = null,
                        ProcessingCode = new byte[] { 0x40, 0x10, 0x00 },
                        TransactionAmount = 7250000.00m,
                        SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
                        ExpirationDate = null,
                        PosEntryMode = new byte[] { 0x00, 0x21 },
                        NetworkInternationalId = "005",
                        PosConditionCode = new byte[] { 0x00 },
                        Track2Data = new byte[] { 0x49, 0x91, 0x87, 0x02, 0x73, 0x00, 0x27, 0x3C,
                            0xD6, 0x2B, 0x27, 0x1A, 0x0A, 0x38, 0x08, 0x00, 0x80, 0x12, 0x40 },
                        TerminalId = "12341234",
                        MerchantId = "123451234512345",
                        AdditionalData = null,
                        CardholderPinBlock = new byte[] { 0x77, 0xBB, 0xAA, 0x66, 0x78, 0x3B, 0xD7, 0xCC },
                        InvoiceNumber = "003107",
                        TransferData = new Bit63Content {
                            TableId = "78",
		                    BeneficiaryInstitutionId = "00000002314",
		                    BeneficiaryAccountNumber = "0123456789012345",
		                    BeneficiaryName = null,
		                    CustomerReferenceNumber = null,
		                    IssuerInstitutionId = null,
		                    CardholderAccountNumber = null,
		                    CardholderName = null,
                            InformationData = null
                        },
                        MessageAuthenticationCode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00 },
                        TransactionDescription = "TRANSACTION NOTE"
                    }
                }
            },

            { "TransferTB",
                new EditedItem() {
                    Panel = "_PanelTrans",
                    DefaultModel = new Request0200TB() {
                        PrimaryAccountNumber = null,
                        ProcessingCode = new byte[] { 0x40, 0x10, 0x00 },
                        TransactionAmount = 7250000.00m,
                        SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
                        ExpirationDate = null,
                        PosEntryMode = new byte[] { 0x00, 0x21 },
                        NetworkInternationalId = "005",
                        PosConditionCode = new byte[] { 0x00 },
                        Track2Data = new byte[] { 0x49, 0x91, 0x87, 0x02, 0x73, 0x00, 0x27, 0x3C,
                            0xD6, 0x2B, 0x27, 0x1A, 0x0A, 0x38, 0x08, 0x00, 0x80, 0x12, 0x40 },
                        TerminalId = "12341234",
                        MerchantId = "123451234512345",
                        AdditionalData = null,
                        CardholderPinBlock = new byte[] { 0x77, 0xBB, 0xAA, 0x66, 0x78, 0x3B, 0xD7, 0xCC },
                        InvoiceNumber = "003107",
                        TransferData = new Bit63Content {
                            TableId = "78",
		                    BeneficiaryInstitutionId = "00000002314",
		                    BeneficiaryAccountNumber = "0123456789012345",
		                    BeneficiaryName = null,
		                    CustomerReferenceNumber = null,
		                    IssuerInstitutionId = null,
		                    CardholderAccountNumber = null,
		                    CardholderName = null,
                            InformationData = null
                        },
                        MessageAuthenticationCode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00 },
                        TransactionDescription = "TRANSACTION NOTE",
                        AdditionalField1 = "Extension attribute for this transaction",
                        AdditionalField2 = 888
                    }
                }
            },

            { "Balance Inquiry",
                new EditedItem() {
                    Panel = "_PanelTrans",
                    DefaultModel = new ReqBalanceInquiry0100() {
                        PrimaryAccountNumber = null,
                        ProcessingCode = new byte[] { 0x31, 0x10, 0x00 },
                        TransactionAmount = null,
                        SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
                        ExpirationDate = null,
                        PosEntryMode = new byte[] { 0x00, 0x21 },
                        NetworkInternationalId = "005",
                        PosConditionCode = (byte)0x00,
                        Track2Data = new byte[] { 0x49, 0x91, 0x87, 0x02, 0x73, 0x00, 0x27, 0x3C,
                            0xD6, 0x2B, 0x27, 0x1A, 0x0A, 0x38, 0x08, 0x00, 0x80, 0x12, 0x40 },
                        TerminalId = "12341234",
                        MerchantId = "123451234512345",
                        AdditionalData = null,
                        CardholderPinBlock = new byte[] { 0x77, 0xBB, 0xAA, 0x66, 0x78, 0x3B, 0xD7, 0xCC },
                        InvoiceNumber = "003107",
                        TransferData = null,
                        MessageAuthenticationCode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00 }
                    }
                }
            },

            { "Logon",
                new EditedItem() {
                    Panel = "_PanelLogon",
                    DefaultModel = new ReqLogon0800() {
                        ProcessingCode = new byte[] { 0x92, 0x00, 0x00 },
                        SystemAuditTraceNumber = new byte[] { 0x00, 0x31, 0x07 },
                        NetworkInternationalId = "005",
                        TerminalId = "12341234",
                    }
                }
            }
        };

        static EditedItems()
        {
            Dictionary<String, EditedItem> items = new Dictionary<String, EditedItem>();
            foreach (KeyValuePair<String, EditedItem> kvp in _editedItems)
            {
                MessageCompiler compiler = new MessageCompiler(kvp.Value.DefaultModel);
                compiler.Compile();
                EditedItem editedItem = kvp.Value;
                editedItem.RawMessageString = MessageUtility.HexToReadableString(compiler.CompiledMessage.GetAllBytes());
                items[kvp.Key] = editedItem;
            }
            _editedItems = items;
        }

        public static IDictionary<String, EditedItem> Item
        {
            get { return _editedItems; }
        }
    }

    public struct EditedItem
    {
        public String Panel;
        public Object DefaultModel;
        public String RawMessageString;
    }
}