namespace Iso8583Client
{
    partial class MessageEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Free.iso8583.example.model.RequestDataEntry48 requestDataEntry481 = new Free.iso8583.example.model.RequestDataEntry48();
            Free.iso8583.example.model.Bit63Content bit63Content1 = new Free.iso8583.example.model.Bit63Content();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageEditor));
            this.pnlTrans = new System.Windows.Forms.Panel();
            this.chkAdditionalField2 = new Iso8583Client.NullCheckBox();
            this.AdditionalField2 = new Iso8583Client.IntegerTextBox();
            this.lblAdditionalField2 = new System.Windows.Forms.Label();
            this.chkAdditionalField1 = new Iso8583Client.NullCheckBox();
            this.AdditionalField1 = new Iso8583Client.StringTextBox();
            this.lblAdditionalField1 = new System.Windows.Forms.Label();
            this.chkTransactionDescription = new Iso8583Client.NullCheckBox();
            this.TransactionDescription = new Iso8583Client.StringTextBox();
            this.lblTransactionDescription = new System.Windows.Forms.Label();
            this.AdditionalData = new Iso8583Client.RequestDataEntry48Box();
            this.ProcessingCode = new Iso8583Client.ProcessingCodeBox();
            this.lblPrimaryAccountNumber = new System.Windows.Forms.Label();
            this.PrimaryAccountNumber = new Iso8583Client.IntegerTextBox();
            this.lblProcessingCode = new System.Windows.Forms.Label();
            this.TransactionAmount = new Iso8583Client.DecimalTextBox();
            this.lblTransactionAmount = new System.Windows.Forms.Label();
            this.lblSystemAuditTraceNumber = new System.Windows.Forms.Label();
            this.SystemAuditTraceNumber = new Iso8583Client.IntegerTextBox();
            this.ExpirationDate = new Iso8583Client.DateTextBox();
            this.lblExpirationDate = new System.Windows.Forms.Label();
            this.PosEntryMode = new Iso8583Client.BytesComboBox();
            this.lblPosEntryMode = new System.Windows.Forms.Label();
            this.lblNetworkInternationalId = new System.Windows.Forms.Label();
            this.NetworkInternationalId = new Iso8583Client.IntegerTextBox();
            this.PosConditionCode = new Iso8583Client.BytesComboBox();
            this.lblPosConditionCode = new System.Windows.Forms.Label();
            this.lblTrack2Data = new System.Windows.Forms.Label();
            this.Track2Data = new Iso8583Client.HexaDecimalTextBox();
            this.lblTerminalId = new System.Windows.Forms.Label();
            this.TerminalId = new Iso8583Client.IntegerTextBox();
            this.lblMerchantId = new System.Windows.Forms.Label();
            this.MerchantId = new Iso8583Client.IntegerTextBox();
            this.lblAdditionalData = new System.Windows.Forms.Label();
            this.lblCardholderPinBlock = new System.Windows.Forms.Label();
            this.CardholderPinBlock = new Iso8583Client.HexaDecimalTextBox();
            this.InvoiceNumber = new Iso8583Client.IntegerTextBox();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.lblTransferData = new System.Windows.Forms.Label();
            this.lblMessageAuthenticationCode = new System.Windows.Forms.Label();
            this.MessageAuthenticationCode = new Iso8583Client.HexaDecimalTextBox();
            this.chkPrimaryAccountNumber = new Iso8583Client.NullCheckBox();
            this.chkExpirationDate = new Iso8583Client.NullCheckBox();
            this.TransferData = new Iso8583Client.Bit63ContentBox();
            this.chkTrack2Data = new Iso8583Client.NullCheckBox();
            this.chkAdditionalData = new Iso8583Client.NullCheckBox();
            this.chkTransferData = new Iso8583Client.NullCheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblModel = new System.Windows.Forms.Label();
            this.pnlLogon = new System.Windows.Forms.Panel();
            this.lblTerminalId2 = new System.Windows.Forms.Label();
            this.lblNetworkInternationalId2 = new System.Windows.Forms.Label();
            this.lblSystemAuditTraceNumber2 = new System.Windows.Forms.Label();
            this.SystemAuditTraceNumber2 = new Iso8583Client.IntegerTextBox();
            this.lblProcessingCode2 = new System.Windows.Forms.Label();
            this.ProcessingCode2 = new Iso8583Client.ProcessingCodeBox();
            this.NetworkInternationalId2 = new Iso8583Client.IntegerTextBox();
            this.TerminalId2 = new Iso8583Client.IntegerTextBox();
            this.pnlTrans.SuspendLayout();
            this.pnlLogon.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTrans
            // 
            this.pnlTrans.AutoScroll = true;
            this.pnlTrans.Controls.Add(this.chkAdditionalField2);
            this.pnlTrans.Controls.Add(this.AdditionalField2);
            this.pnlTrans.Controls.Add(this.lblAdditionalField2);
            this.pnlTrans.Controls.Add(this.chkAdditionalField1);
            this.pnlTrans.Controls.Add(this.AdditionalField1);
            this.pnlTrans.Controls.Add(this.lblAdditionalField1);
            this.pnlTrans.Controls.Add(this.chkTransactionDescription);
            this.pnlTrans.Controls.Add(this.TransactionDescription);
            this.pnlTrans.Controls.Add(this.lblTransactionDescription);
            this.pnlTrans.Controls.Add(this.AdditionalData);
            this.pnlTrans.Controls.Add(this.ProcessingCode);
            this.pnlTrans.Controls.Add(this.lblPrimaryAccountNumber);
            this.pnlTrans.Controls.Add(this.PrimaryAccountNumber);
            this.pnlTrans.Controls.Add(this.lblProcessingCode);
            this.pnlTrans.Controls.Add(this.TransactionAmount);
            this.pnlTrans.Controls.Add(this.lblTransactionAmount);
            this.pnlTrans.Controls.Add(this.lblSystemAuditTraceNumber);
            this.pnlTrans.Controls.Add(this.SystemAuditTraceNumber);
            this.pnlTrans.Controls.Add(this.ExpirationDate);
            this.pnlTrans.Controls.Add(this.lblExpirationDate);
            this.pnlTrans.Controls.Add(this.PosEntryMode);
            this.pnlTrans.Controls.Add(this.lblPosEntryMode);
            this.pnlTrans.Controls.Add(this.lblNetworkInternationalId);
            this.pnlTrans.Controls.Add(this.NetworkInternationalId);
            this.pnlTrans.Controls.Add(this.PosConditionCode);
            this.pnlTrans.Controls.Add(this.lblPosConditionCode);
            this.pnlTrans.Controls.Add(this.lblTrack2Data);
            this.pnlTrans.Controls.Add(this.Track2Data);
            this.pnlTrans.Controls.Add(this.lblTerminalId);
            this.pnlTrans.Controls.Add(this.TerminalId);
            this.pnlTrans.Controls.Add(this.lblMerchantId);
            this.pnlTrans.Controls.Add(this.MerchantId);
            this.pnlTrans.Controls.Add(this.lblAdditionalData);
            this.pnlTrans.Controls.Add(this.lblCardholderPinBlock);
            this.pnlTrans.Controls.Add(this.CardholderPinBlock);
            this.pnlTrans.Controls.Add(this.InvoiceNumber);
            this.pnlTrans.Controls.Add(this.lblInvoiceNumber);
            this.pnlTrans.Controls.Add(this.lblTransferData);
            this.pnlTrans.Controls.Add(this.lblMessageAuthenticationCode);
            this.pnlTrans.Controls.Add(this.MessageAuthenticationCode);
            this.pnlTrans.Controls.Add(this.chkPrimaryAccountNumber);
            this.pnlTrans.Controls.Add(this.chkExpirationDate);
            this.pnlTrans.Controls.Add(this.TransferData);
            this.pnlTrans.Controls.Add(this.chkTrack2Data);
            this.pnlTrans.Controls.Add(this.chkAdditionalData);
            this.pnlTrans.Controls.Add(this.chkTransferData);
            this.pnlTrans.Location = new System.Drawing.Point(0, 32);
            this.pnlTrans.Name = "pnlTrans";
            this.pnlTrans.Size = new System.Drawing.Size(828, 443);
            this.pnlTrans.TabIndex = 0;
            // 
            // chkAdditionalField2
            // 
            this.chkAdditionalField2.AutoSize = true;
            this.chkAdditionalField2.Location = new System.Drawing.Point(245, 488);
            this.chkAdditionalField2.Name = "chkAdditionalField2";
            this.chkAdditionalField2.NullControlName = "AdditionalField2";
            this.chkAdditionalField2.Size = new System.Drawing.Size(54, 17);
            this.chkAdditionalField2.TabIndex = 51;
            this.chkAdditionalField2.Text = "NULL";
            this.chkAdditionalField2.UseVisualStyleBackColor = true;
            this.chkAdditionalField2.Visible = false;
            // 
            // AdditionalField2
            // 
            this.AdditionalField2.AcceptNegative = false;
            this.AdditionalField2.BytesValue = new byte[] {
        ((byte)(0))};
            this.AdditionalField2.IsNull = false;
            this.AdditionalField2.Length = 5;
            this.AdditionalField2.Location = new System.Drawing.Point(170, 486);
            this.AdditionalField2.MaxLength = 5;
            this.AdditionalField2.Name = "AdditionalField2";
            this.AdditionalField2.Size = new System.Drawing.Size(69, 20);
            this.AdditionalField2.StringValue = "0";
            this.AdditionalField2.TabIndex = 50;
            this.AdditionalField2.Text = "0";
            this.AdditionalField2.Value = 0;
            this.AdditionalField2.Visible = false;
            // 
            // lblAdditionalField2
            // 
            this.lblAdditionalField2.AutoSize = true;
            this.lblAdditionalField2.Location = new System.Drawing.Point(13, 489);
            this.lblAdditionalField2.Name = "lblAdditionalField2";
            this.lblAdditionalField2.Size = new System.Drawing.Size(81, 13);
            this.lblAdditionalField2.TabIndex = 49;
            this.lblAdditionalField2.Text = "AdditionalField2";
            this.lblAdditionalField2.Visible = false;
            // 
            // chkAdditionalField1
            // 
            this.chkAdditionalField1.AutoSize = true;
            this.chkAdditionalField1.Location = new System.Drawing.Point(691, 466);
            this.chkAdditionalField1.Name = "chkAdditionalField1";
            this.chkAdditionalField1.NullControlName = "AdditionalField1";
            this.chkAdditionalField1.Size = new System.Drawing.Size(54, 17);
            this.chkAdditionalField1.TabIndex = 48;
            this.chkAdditionalField1.Text = "NULL";
            this.chkAdditionalField1.UseVisualStyleBackColor = true;
            this.chkAdditionalField1.Visible = false;
            // 
            // AdditionalField1
            // 
            this.AdditionalField1.BytesValue = new byte[0];
            this.AdditionalField1.IsNull = false;
            this.AdditionalField1.Location = new System.Drawing.Point(170, 464);
            this.AdditionalField1.MaxLength = 99;
            this.AdditionalField1.Name = "AdditionalField1";
            this.AdditionalField1.Size = new System.Drawing.Size(515, 20);
            this.AdditionalField1.StringValue = "";
            this.AdditionalField1.TabIndex = 47;
            this.AdditionalField1.Value = "";
            this.AdditionalField1.Visible = false;
            // 
            // lblAdditionalField1
            // 
            this.lblAdditionalField1.AutoSize = true;
            this.lblAdditionalField1.Location = new System.Drawing.Point(13, 467);
            this.lblAdditionalField1.Name = "lblAdditionalField1";
            this.lblAdditionalField1.Size = new System.Drawing.Size(81, 13);
            this.lblAdditionalField1.TabIndex = 46;
            this.lblAdditionalField1.Text = "AdditionalField1";
            this.lblAdditionalField1.Visible = false;
            // 
            // chkTransactionDescription
            // 
            this.chkTransactionDescription.AutoSize = true;
            this.chkTransactionDescription.Location = new System.Drawing.Point(691, 444);
            this.chkTransactionDescription.Name = "chkTransactionDescription";
            this.chkTransactionDescription.NullControlName = "TransactionDescription";
            this.chkTransactionDescription.Size = new System.Drawing.Size(54, 17);
            this.chkTransactionDescription.TabIndex = 45;
            this.chkTransactionDescription.Text = "NULL";
            this.chkTransactionDescription.UseVisualStyleBackColor = true;
            this.chkTransactionDescription.Visible = false;
            // 
            // TransactionDescription
            // 
            this.TransactionDescription.BytesValue = new byte[0];
            this.TransactionDescription.IsNull = false;
            this.TransactionDescription.Location = new System.Drawing.Point(170, 442);
            this.TransactionDescription.Name = "TransactionDescription";
            this.TransactionDescription.Size = new System.Drawing.Size(515, 20);
            this.TransactionDescription.StringValue = "";
            this.TransactionDescription.TabIndex = 44;
            this.TransactionDescription.Value = "";
            this.TransactionDescription.Visible = false;
            this.TransactionDescription.WordWrap = false;
            // 
            // lblTransactionDescription
            // 
            this.lblTransactionDescription.AutoSize = true;
            this.lblTransactionDescription.Location = new System.Drawing.Point(13, 445);
            this.lblTransactionDescription.Name = "lblTransactionDescription";
            this.lblTransactionDescription.Size = new System.Drawing.Size(116, 13);
            this.lblTransactionDescription.TabIndex = 43;
            this.lblTransactionDescription.Text = "TransactionDescription";
            this.lblTransactionDescription.Visible = false;
            // 
            // AdditionalData
            // 
            this.AdditionalData.BytesValue = new byte[] {
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(70)),
        ((byte)(68)),
        ((byte)(49)),
        ((byte)(48))};
            this.AdditionalData.IsNull = false;
            this.AdditionalData.Location = new System.Drawing.Point(170, 244);
            this.AdditionalData.Name = "AdditionalData";
            this.AdditionalData.Size = new System.Drawing.Size(425, 64);
            this.AdditionalData.StringValue = "00000                                                  FD10";
            this.AdditionalData.TabIndex = 42;
            requestDataEntry481.Fee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            requestDataEntry481.Note = "";
            requestDataEntry481.ProductCode = "";
            this.AdditionalData.Value = requestDataEntry481;
            // 
            // ProcessingCode
            // 
            this.ProcessingCode.BytesValue = null;
            this.ProcessingCode.IsNull = false;
            this.ProcessingCode.Location = new System.Drawing.Point(170, 24);
            this.ProcessingCode.Name = "ProcessingCode";
            this.ProcessingCode.Size = new System.Drawing.Size(640, 21);
            this.ProcessingCode.StringValue = null;
            this.ProcessingCode.TabIndex = 41;
            this.ProcessingCode.TransactionTypeCode = Free.iso8583.example.TransactionTypeCodeBytes.BALANCE_INQUIRY;
            this.ProcessingCode.Value = null;
            // 
            // lblPrimaryAccountNumber
            // 
            this.lblPrimaryAccountNumber.AutoSize = true;
            this.lblPrimaryAccountNumber.Location = new System.Drawing.Point(13, 5);
            this.lblPrimaryAccountNumber.Name = "lblPrimaryAccountNumber";
            this.lblPrimaryAccountNumber.Size = new System.Drawing.Size(118, 13);
            this.lblPrimaryAccountNumber.TabIndex = 0;
            this.lblPrimaryAccountNumber.Text = "PrimaryAccountNumber";
            // 
            // PrimaryAccountNumber
            // 
            this.PrimaryAccountNumber.AcceptNegative = false;
            this.PrimaryAccountNumber.BytesValue = new byte[] {
        ((byte)(0))};
            this.PrimaryAccountNumber.IsNull = false;
            this.PrimaryAccountNumber.Length = 0;
            this.PrimaryAccountNumber.Location = new System.Drawing.Point(170, 2);
            this.PrimaryAccountNumber.MaxLength = 16;
            this.PrimaryAccountNumber.Name = "PrimaryAccountNumber";
            this.PrimaryAccountNumber.Size = new System.Drawing.Size(317, 20);
            this.PrimaryAccountNumber.StringValue = "0";
            this.PrimaryAccountNumber.TabIndex = 1;
            this.PrimaryAccountNumber.Text = "0";
            this.PrimaryAccountNumber.Value = 0;
            // 
            // lblProcessingCode
            // 
            this.lblProcessingCode.AutoSize = true;
            this.lblProcessingCode.Location = new System.Drawing.Point(13, 27);
            this.lblProcessingCode.Name = "lblProcessingCode";
            this.lblProcessingCode.Size = new System.Drawing.Size(84, 13);
            this.lblProcessingCode.TabIndex = 2;
            this.lblProcessingCode.Text = "ProcessingCode";
            // 
            // TransactionAmount
            // 
            this.TransactionAmount.AcceptNegative = false;
            this.TransactionAmount.BytesValue = new byte[0];
            this.TransactionAmount.FracDigits = 2;
            this.TransactionAmount.IsNull = false;
            this.TransactionAmount.Length = 0;
            this.TransactionAmount.Location = new System.Drawing.Point(170, 46);
            this.TransactionAmount.MaxLength = 12;
            this.TransactionAmount.Name = "TransactionAmount";
            this.TransactionAmount.Size = new System.Drawing.Size(317, 20);
            this.TransactionAmount.StringValue = "0";
            this.TransactionAmount.TabIndex = 7;
            this.TransactionAmount.Text = "0";
            this.TransactionAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblTransactionAmount
            // 
            this.lblTransactionAmount.AutoSize = true;
            this.lblTransactionAmount.Location = new System.Drawing.Point(13, 49);
            this.lblTransactionAmount.Name = "lblTransactionAmount";
            this.lblTransactionAmount.Size = new System.Drawing.Size(99, 13);
            this.lblTransactionAmount.TabIndex = 6;
            this.lblTransactionAmount.Text = "TransactionAmount";
            // 
            // lblSystemAuditTraceNumber
            // 
            this.lblSystemAuditTraceNumber.AutoSize = true;
            this.lblSystemAuditTraceNumber.Location = new System.Drawing.Point(13, 71);
            this.lblSystemAuditTraceNumber.Name = "lblSystemAuditTraceNumber";
            this.lblSystemAuditTraceNumber.Size = new System.Drawing.Size(130, 13);
            this.lblSystemAuditTraceNumber.TabIndex = 8;
            this.lblSystemAuditTraceNumber.Text = "SystemAuditTraceNumber";
            // 
            // SystemAuditTraceNumber
            // 
            this.SystemAuditTraceNumber.AcceptNegative = false;
            this.SystemAuditTraceNumber.BytesValue = new byte[] {
        ((byte)(0))};
            this.SystemAuditTraceNumber.IsNull = false;
            this.SystemAuditTraceNumber.Length = 0;
            this.SystemAuditTraceNumber.Location = new System.Drawing.Point(170, 68);
            this.SystemAuditTraceNumber.MaxLength = 6;
            this.SystemAuditTraceNumber.Name = "SystemAuditTraceNumber";
            this.SystemAuditTraceNumber.Size = new System.Drawing.Size(69, 20);
            this.SystemAuditTraceNumber.StringValue = "0";
            this.SystemAuditTraceNumber.TabIndex = 9;
            this.SystemAuditTraceNumber.Text = "0";
            this.SystemAuditTraceNumber.Value = 0;
            // 
            // ExpirationDate
            // 
            this.ExpirationDate.BytesValue = new byte[] {
        ((byte)(18)),
        ((byte)(7))};
            this.ExpirationDate.IsNull = false;
            this.ExpirationDate.Location = new System.Drawing.Point(170, 90);
            this.ExpirationDate.Mode = 1;
            this.ExpirationDate.Name = "ExpirationDate";
            this.ExpirationDate.Size = new System.Drawing.Size(86, 20);
            this.ExpirationDate.StringValue = "1207";
            this.ExpirationDate.TabIndex = 12;
            this.ExpirationDate.Value = new System.DateTime(12, 7, 29, 0, 0, 0, 0);
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.AutoSize = true;
            this.lblExpirationDate.Location = new System.Drawing.Point(13, 93);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(76, 13);
            this.lblExpirationDate.TabIndex = 11;
            this.lblExpirationDate.Text = "ExpirationDate";
            // 
            // PosEntryMode
            // 
            this.PosEntryMode.BytesValue = null;
            this.PosEntryMode.FormattingEnabled = true;
            this.PosEntryMode.IsNull = false;
            this.PosEntryMode.Location = new System.Drawing.Point(170, 112);
            this.PosEntryMode.Name = "PosEntryMode";
            this.PosEntryMode.Size = new System.Drawing.Size(236, 21);
            this.PosEntryMode.StringValue = null;
            this.PosEntryMode.TabIndex = 14;
            this.PosEntryMode.Value = null;
            // 
            // lblPosEntryMode
            // 
            this.lblPosEntryMode.AutoSize = true;
            this.lblPosEntryMode.Location = new System.Drawing.Point(13, 115);
            this.lblPosEntryMode.Name = "lblPosEntryMode";
            this.lblPosEntryMode.Size = new System.Drawing.Size(76, 13);
            this.lblPosEntryMode.TabIndex = 13;
            this.lblPosEntryMode.Text = "PosEntryMode";
            // 
            // lblNetworkInternationalId
            // 
            this.lblNetworkInternationalId.AutoSize = true;
            this.lblNetworkInternationalId.Location = new System.Drawing.Point(13, 137);
            this.lblNetworkInternationalId.Name = "lblNetworkInternationalId";
            this.lblNetworkInternationalId.Size = new System.Drawing.Size(114, 13);
            this.lblNetworkInternationalId.TabIndex = 15;
            this.lblNetworkInternationalId.Text = "NetworkInternationalId";
            // 
            // NetworkInternationalId
            // 
            this.NetworkInternationalId.AcceptNegative = false;
            this.NetworkInternationalId.BytesValue = new byte[] {
        ((byte)(0))};
            this.NetworkInternationalId.IsNull = false;
            this.NetworkInternationalId.Length = 3;
            this.NetworkInternationalId.Location = new System.Drawing.Point(170, 134);
            this.NetworkInternationalId.MaxLength = 3;
            this.NetworkInternationalId.Name = "NetworkInternationalId";
            this.NetworkInternationalId.Size = new System.Drawing.Size(45, 20);
            this.NetworkInternationalId.StringValue = "0";
            this.NetworkInternationalId.TabIndex = 16;
            this.NetworkInternationalId.Text = "0";
            this.NetworkInternationalId.Value = 0;
            // 
            // PosConditionCode
            // 
            this.PosConditionCode.BytesValue = null;
            this.PosConditionCode.FormattingEnabled = true;
            this.PosConditionCode.IsNull = false;
            this.PosConditionCode.Location = new System.Drawing.Point(170, 156);
            this.PosConditionCode.Name = "PosConditionCode";
            this.PosConditionCode.Size = new System.Drawing.Size(236, 21);
            this.PosConditionCode.StringValue = null;
            this.PosConditionCode.TabIndex = 18;
            this.PosConditionCode.Value = null;
            // 
            // lblPosConditionCode
            // 
            this.lblPosConditionCode.AutoSize = true;
            this.lblPosConditionCode.Location = new System.Drawing.Point(13, 159);
            this.lblPosConditionCode.Name = "lblPosConditionCode";
            this.lblPosConditionCode.Size = new System.Drawing.Size(94, 13);
            this.lblPosConditionCode.TabIndex = 17;
            this.lblPosConditionCode.Text = "PosConditionCode";
            // 
            // lblTrack2Data
            // 
            this.lblTrack2Data.AutoSize = true;
            this.lblTrack2Data.Location = new System.Drawing.Point(13, 181);
            this.lblTrack2Data.Name = "lblTrack2Data";
            this.lblTrack2Data.Size = new System.Drawing.Size(64, 13);
            this.lblTrack2Data.TabIndex = 19;
            this.lblTrack2Data.Text = "Track2Data";
            // 
            // Track2Data
            // 
            this.Track2Data.BytesValue = new byte[0];
            this.Track2Data.IsLowerCase = false;
            this.Track2Data.IsNull = false;
            this.Track2Data.LeftAligned = false;
            this.Track2Data.Length = 0;
            this.Track2Data.Location = new System.Drawing.Point(170, 178);
            this.Track2Data.MaxLength = 99;
            this.Track2Data.Name = "Track2Data";
            this.Track2Data.Size = new System.Drawing.Size(317, 20);
            this.Track2Data.StringValue = "";
            this.Track2Data.TabIndex = 20;
            this.Track2Data.Value = new byte[0];
            // 
            // lblTerminalId
            // 
            this.lblTerminalId.AutoSize = true;
            this.lblTerminalId.Location = new System.Drawing.Point(13, 203);
            this.lblTerminalId.Name = "lblTerminalId";
            this.lblTerminalId.Size = new System.Drawing.Size(56, 13);
            this.lblTerminalId.TabIndex = 21;
            this.lblTerminalId.Text = "TerminalId";
            // 
            // TerminalId
            // 
            this.TerminalId.AcceptNegative = false;
            this.TerminalId.BytesValue = new byte[] {
        ((byte)(0))};
            this.TerminalId.IsNull = false;
            this.TerminalId.Length = 8;
            this.TerminalId.Location = new System.Drawing.Point(170, 200);
            this.TerminalId.MaxLength = 8;
            this.TerminalId.Name = "TerminalId";
            this.TerminalId.Size = new System.Drawing.Size(69, 20);
            this.TerminalId.StringValue = "0";
            this.TerminalId.TabIndex = 22;
            this.TerminalId.Text = "0";
            this.TerminalId.Value = 0;
            // 
            // lblMerchantId
            // 
            this.lblMerchantId.AutoSize = true;
            this.lblMerchantId.Location = new System.Drawing.Point(13, 225);
            this.lblMerchantId.Name = "lblMerchantId";
            this.lblMerchantId.Size = new System.Drawing.Size(61, 13);
            this.lblMerchantId.TabIndex = 23;
            this.lblMerchantId.Text = "MerchantId";
            // 
            // MerchantId
            // 
            this.MerchantId.AcceptNegative = false;
            this.MerchantId.BytesValue = new byte[] {
        ((byte)(0))};
            this.MerchantId.IsNull = false;
            this.MerchantId.Length = 15;
            this.MerchantId.Location = new System.Drawing.Point(170, 222);
            this.MerchantId.MaxLength = 15;
            this.MerchantId.Name = "MerchantId";
            this.MerchantId.Size = new System.Drawing.Size(108, 20);
            this.MerchantId.StringValue = "0";
            this.MerchantId.TabIndex = 24;
            this.MerchantId.Text = "0";
            this.MerchantId.Value = 0;
            // 
            // lblAdditionalData
            // 
            this.lblAdditionalData.AutoSize = true;
            this.lblAdditionalData.Location = new System.Drawing.Point(13, 257);
            this.lblAdditionalData.Name = "lblAdditionalData";
            this.lblAdditionalData.Size = new System.Drawing.Size(76, 13);
            this.lblAdditionalData.TabIndex = 25;
            this.lblAdditionalData.Text = "AdditionalData";
            // 
            // lblCardholderPinBlock
            // 
            this.lblCardholderPinBlock.AutoSize = true;
            this.lblCardholderPinBlock.Location = new System.Drawing.Point(13, 313);
            this.lblCardholderPinBlock.Name = "lblCardholderPinBlock";
            this.lblCardholderPinBlock.Size = new System.Drawing.Size(100, 13);
            this.lblCardholderPinBlock.TabIndex = 26;
            this.lblCardholderPinBlock.Text = "CardholderPinBlock";
            // 
            // CardholderPinBlock
            // 
            this.CardholderPinBlock.BytesValue = new byte[] {
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0))};
            this.CardholderPinBlock.IsLowerCase = false;
            this.CardholderPinBlock.IsNull = false;
            this.CardholderPinBlock.LeftAligned = false;
            this.CardholderPinBlock.Length = 16;
            this.CardholderPinBlock.Location = new System.Drawing.Point(170, 310);
            this.CardholderPinBlock.MaxLength = 16;
            this.CardholderPinBlock.Name = "CardholderPinBlock";
            this.CardholderPinBlock.Size = new System.Drawing.Size(129, 20);
            this.CardholderPinBlock.StringValue = "0000000000000000";
            this.CardholderPinBlock.TabIndex = 27;
            this.CardholderPinBlock.Text = "0000000000000000";
            this.CardholderPinBlock.Value = new byte[] {
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0))};
            // 
            // InvoiceNumber
            // 
            this.InvoiceNumber.AcceptNegative = false;
            this.InvoiceNumber.BytesValue = new byte[] {
        ((byte)(0))};
            this.InvoiceNumber.IsNull = false;
            this.InvoiceNumber.Length = 0;
            this.InvoiceNumber.Location = new System.Drawing.Point(170, 332);
            this.InvoiceNumber.MaxLength = 6;
            this.InvoiceNumber.Name = "InvoiceNumber";
            this.InvoiceNumber.Size = new System.Drawing.Size(69, 20);
            this.InvoiceNumber.StringValue = "0";
            this.InvoiceNumber.TabIndex = 29;
            this.InvoiceNumber.Text = "0";
            this.InvoiceNumber.Value = 0;
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.AutoSize = true;
            this.lblInvoiceNumber.Location = new System.Drawing.Point(13, 335);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(79, 13);
            this.lblInvoiceNumber.TabIndex = 28;
            this.lblInvoiceNumber.Text = "InvoiceNumber";
            // 
            // lblTransferData
            // 
            this.lblTransferData.AutoSize = true;
            this.lblTransferData.Location = new System.Drawing.Point(13, 357);
            this.lblTransferData.Name = "lblTransferData";
            this.lblTransferData.Size = new System.Drawing.Size(69, 13);
            this.lblTransferData.TabIndex = 30;
            this.lblTransferData.Text = "TransferData";
            // 
            // lblMessageAuthenticationCode
            // 
            this.lblMessageAuthenticationCode.AutoSize = true;
            this.lblMessageAuthenticationCode.Location = new System.Drawing.Point(13, 423);
            this.lblMessageAuthenticationCode.Name = "lblMessageAuthenticationCode";
            this.lblMessageAuthenticationCode.Size = new System.Drawing.Size(143, 13);
            this.lblMessageAuthenticationCode.TabIndex = 31;
            this.lblMessageAuthenticationCode.Text = "MessageAuthenticationCode";
            // 
            // MessageAuthenticationCode
            // 
            this.MessageAuthenticationCode.BytesValue = new byte[] {
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0))};
            this.MessageAuthenticationCode.IsLowerCase = false;
            this.MessageAuthenticationCode.IsNull = false;
            this.MessageAuthenticationCode.LeftAligned = false;
            this.MessageAuthenticationCode.Length = 16;
            this.MessageAuthenticationCode.Location = new System.Drawing.Point(170, 420);
            this.MessageAuthenticationCode.MaxLength = 16;
            this.MessageAuthenticationCode.Name = "MessageAuthenticationCode";
            this.MessageAuthenticationCode.Size = new System.Drawing.Size(108, 20);
            this.MessageAuthenticationCode.StringValue = "0000000000000000";
            this.MessageAuthenticationCode.TabIndex = 32;
            this.MessageAuthenticationCode.Text = "0000000000000000";
            this.MessageAuthenticationCode.Value = new byte[] {
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0)),
        ((byte)(0))};
            // 
            // chkPrimaryAccountNumber
            // 
            this.chkPrimaryAccountNumber.AutoSize = true;
            this.chkPrimaryAccountNumber.Location = new System.Drawing.Point(493, 4);
            this.chkPrimaryAccountNumber.Name = "chkPrimaryAccountNumber";
            this.chkPrimaryAccountNumber.NullControlName = "PrimaryAccountNumber";
            this.chkPrimaryAccountNumber.Size = new System.Drawing.Size(54, 17);
            this.chkPrimaryAccountNumber.TabIndex = 34;
            this.chkPrimaryAccountNumber.Text = "NULL";
            this.chkPrimaryAccountNumber.UseVisualStyleBackColor = true;
            // 
            // chkExpirationDate
            // 
            this.chkExpirationDate.AutoSize = true;
            this.chkExpirationDate.Location = new System.Drawing.Point(262, 92);
            this.chkExpirationDate.Name = "chkExpirationDate";
            this.chkExpirationDate.NullControlName = "ExpirationDate";
            this.chkExpirationDate.Size = new System.Drawing.Size(54, 17);
            this.chkExpirationDate.TabIndex = 35;
            this.chkExpirationDate.Text = "NULL";
            this.chkExpirationDate.UseVisualStyleBackColor = true;
            // 
            // TransferData
            // 
            this.TransferData.BytesValue = new byte[] {
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(48)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32)),
        ((byte)(32))};
            this.TransferData.IsNull = false;
            this.TransferData.Location = new System.Drawing.Point(170, 354);
            this.TransferData.Name = "TransferData";
            this.TransferData.Size = new System.Drawing.Size(455, 64);
            this.TransferData.StringValue = "0000000000000                            ";
            this.TransferData.TabIndex = 37;
            bit63Content1.BeneficiaryAccountNumber = "";
            bit63Content1.BeneficiaryInstitutionId = "00000000000";
            bit63Content1.BeneficiaryName = null;
            bit63Content1.CardholderAccountNumber = null;
            bit63Content1.CardholderName = null;
            bit63Content1.CustomerReferenceNumber = null;
            bit63Content1.InformationData = null;
            bit63Content1.IssuerInstitutionId = null;
            bit63Content1.TableId = "00";
            this.TransferData.Value = bit63Content1;
            // 
            // chkTrack2Data
            // 
            this.chkTrack2Data.AutoSize = true;
            this.chkTrack2Data.Location = new System.Drawing.Point(493, 180);
            this.chkTrack2Data.Name = "chkTrack2Data";
            this.chkTrack2Data.NullControlName = "Track2Data";
            this.chkTrack2Data.Size = new System.Drawing.Size(54, 17);
            this.chkTrack2Data.TabIndex = 38;
            this.chkTrack2Data.Text = "NULL";
            this.chkTrack2Data.UseVisualStyleBackColor = true;
            // 
            // chkAdditionalData
            // 
            this.chkAdditionalData.AutoSize = true;
            this.chkAdditionalData.Location = new System.Drawing.Point(601, 246);
            this.chkAdditionalData.Name = "chkAdditionalData";
            this.chkAdditionalData.NullControlName = "AdditionalData";
            this.chkAdditionalData.Size = new System.Drawing.Size(54, 17);
            this.chkAdditionalData.TabIndex = 39;
            this.chkAdditionalData.Text = "NULL";
            this.chkAdditionalData.UseVisualStyleBackColor = true;
            // 
            // chkTransferData
            // 
            this.chkTransferData.AutoSize = true;
            this.chkTransferData.Location = new System.Drawing.Point(631, 356);
            this.chkTransferData.Name = "chkTransferData";
            this.chkTransferData.NullControlName = "TransferData";
            this.chkTransferData.Size = new System.Drawing.Size(54, 17);
            this.chkTransferData.TabIndex = 40;
            this.chkTransferData.Text = "NULL";
            this.chkTransferData.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 528);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(784, 78);
            this.label17.TabIndex = 1;
            this.label17.Text = resources.GetString("label17.Text");
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(301, 488);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(412, 488);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(13, 9);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(246, 13);
            this.lblModel.TabIndex = 4;
            this.lblModel.Text = "Free.iso8583.example.model.Request0200";
            // 
            // pnlLogon
            // 
            this.pnlLogon.Controls.Add(this.lblTerminalId2);
            this.pnlLogon.Controls.Add(this.lblNetworkInternationalId2);
            this.pnlLogon.Controls.Add(this.lblSystemAuditTraceNumber2);
            this.pnlLogon.Controls.Add(this.SystemAuditTraceNumber2);
            this.pnlLogon.Controls.Add(this.lblProcessingCode2);
            this.pnlLogon.Controls.Add(this.ProcessingCode2);
            this.pnlLogon.Controls.Add(this.NetworkInternationalId2);
            this.pnlLogon.Controls.Add(this.TerminalId2);
            this.pnlLogon.Location = new System.Drawing.Point(0, 32);
            this.pnlLogon.Name = "pnlLogon";
            this.pnlLogon.Size = new System.Drawing.Size(828, 443);
            this.pnlLogon.TabIndex = 44;
            // 
            // lblTerminalId2
            // 
            this.lblTerminalId2.AutoSize = true;
            this.lblTerminalId2.Location = new System.Drawing.Point(13, 71);
            this.lblTerminalId2.Name = "lblTerminalId2";
            this.lblTerminalId2.Size = new System.Drawing.Size(56, 13);
            this.lblTerminalId2.TabIndex = 49;
            this.lblTerminalId2.Text = "TerminalId";
            // 
            // lblNetworkInternationalId2
            // 
            this.lblNetworkInternationalId2.AutoSize = true;
            this.lblNetworkInternationalId2.Location = new System.Drawing.Point(13, 49);
            this.lblNetworkInternationalId2.Name = "lblNetworkInternationalId2";
            this.lblNetworkInternationalId2.Size = new System.Drawing.Size(114, 13);
            this.lblNetworkInternationalId2.TabIndex = 47;
            this.lblNetworkInternationalId2.Text = "NetworkInternationalId";
            // 
            // lblSystemAuditTraceNumber2
            // 
            this.lblSystemAuditTraceNumber2.AutoSize = true;
            this.lblSystemAuditTraceNumber2.Location = new System.Drawing.Point(13, 27);
            this.lblSystemAuditTraceNumber2.Name = "lblSystemAuditTraceNumber2";
            this.lblSystemAuditTraceNumber2.Size = new System.Drawing.Size(130, 13);
            this.lblSystemAuditTraceNumber2.TabIndex = 45;
            this.lblSystemAuditTraceNumber2.Text = "SystemAuditTraceNumber";
            // 
            // SystemAuditTraceNumber2
            // 
            this.SystemAuditTraceNumber2.AcceptNegative = false;
            this.SystemAuditTraceNumber2.BytesValue = new byte[] {
        ((byte)(0))};
            this.SystemAuditTraceNumber2.IsNull = false;
            this.SystemAuditTraceNumber2.Length = 0;
            this.SystemAuditTraceNumber2.Location = new System.Drawing.Point(170, 24);
            this.SystemAuditTraceNumber2.MaxLength = 6;
            this.SystemAuditTraceNumber2.Name = "SystemAuditTraceNumber2";
            this.SystemAuditTraceNumber2.Size = new System.Drawing.Size(69, 20);
            this.SystemAuditTraceNumber2.StringValue = "0";
            this.SystemAuditTraceNumber2.TabIndex = 44;
            this.SystemAuditTraceNumber2.Text = "0";
            this.SystemAuditTraceNumber2.Value = 0;
            // 
            // lblProcessingCode2
            // 
            this.lblProcessingCode2.AutoSize = true;
            this.lblProcessingCode2.Location = new System.Drawing.Point(13, 5);
            this.lblProcessingCode2.Name = "lblProcessingCode2";
            this.lblProcessingCode2.Size = new System.Drawing.Size(84, 13);
            this.lblProcessingCode2.TabIndex = 43;
            this.lblProcessingCode2.Text = "ProcessingCode";
            // 
            // ProcessingCode2
            // 
            this.ProcessingCode2.BytesValue = null;
            this.ProcessingCode2.IsNull = false;
            this.ProcessingCode2.Location = new System.Drawing.Point(170, 2);
            this.ProcessingCode2.Name = "ProcessingCode2";
            this.ProcessingCode2.Size = new System.Drawing.Size(647, 21);
            this.ProcessingCode2.StringValue = null;
            this.ProcessingCode2.TabIndex = 42;
            this.ProcessingCode2.TransactionTypeCode = Free.iso8583.example.TransactionTypeCodeBytes.BALANCE_INQUIRY;
            this.ProcessingCode2.Value = null;
            // 
            // NetworkInternationalId2
            // 
            this.NetworkInternationalId2.AcceptNegative = false;
            this.NetworkInternationalId2.BytesValue = new byte[] {
        ((byte)(0))};
            this.NetworkInternationalId2.IsNull = false;
            this.NetworkInternationalId2.Length = 3;
            this.NetworkInternationalId2.Location = new System.Drawing.Point(170, 46);
            this.NetworkInternationalId2.MaxLength = 3;
            this.NetworkInternationalId2.Name = "NetworkInternationalId2";
            this.NetworkInternationalId2.Size = new System.Drawing.Size(45, 20);
            this.NetworkInternationalId2.StringValue = "0";
            this.NetworkInternationalId2.TabIndex = 46;
            this.NetworkInternationalId2.Text = "0";
            this.NetworkInternationalId2.Value = 0;
            // 
            // TerminalId2
            // 
            this.TerminalId2.AcceptNegative = false;
            this.TerminalId2.BytesValue = new byte[] {
        ((byte)(0))};
            this.TerminalId2.IsNull = false;
            this.TerminalId2.Length = 8;
            this.TerminalId2.Location = new System.Drawing.Point(170, 68);
            this.TerminalId2.MaxLength = 8;
            this.TerminalId2.Name = "TerminalId2";
            this.TerminalId2.Size = new System.Drawing.Size(69, 20);
            this.TerminalId2.StringValue = "0";
            this.TerminalId2.TabIndex = 48;
            this.TerminalId2.Text = "0";
            this.TerminalId2.Value = 0;
            // 
            // MessageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 609);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.pnlTrans);
            this.Controls.Add(this.pnlLogon);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageEditor";
            this.Load += new System.EventHandler(this.MessageEditor_Load);
            this.pnlTrans.ResumeLayout(false);
            this.pnlTrans.PerformLayout();
            this.pnlLogon.ResumeLayout(false);
            this.pnlLogon.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTrans;
        private System.Windows.Forms.Label lblPrimaryAccountNumber;
        private IntegerTextBox PrimaryAccountNumber;
        private System.Windows.Forms.Label lblProcessingCode;
        private DecimalTextBox TransactionAmount;
        private System.Windows.Forms.Label lblTransactionAmount;
        private System.Windows.Forms.Label lblSystemAuditTraceNumber;
        private IntegerTextBox SystemAuditTraceNumber;
        private DateTextBox ExpirationDate;
        private System.Windows.Forms.Label lblExpirationDate;
        private Iso8583Client.BytesComboBox PosEntryMode;
        private System.Windows.Forms.Label lblPosEntryMode;
        private System.Windows.Forms.Label lblNetworkInternationalId;
        private IntegerTextBox NetworkInternationalId;
        private Iso8583Client.BytesComboBox PosConditionCode;
        private System.Windows.Forms.Label lblPosConditionCode;
        private System.Windows.Forms.Label lblTrack2Data;
        private HexaDecimalTextBox Track2Data;
        private System.Windows.Forms.Label lblTerminalId;
        private IntegerTextBox TerminalId;
        private System.Windows.Forms.Label lblMerchantId;
        private IntegerTextBox MerchantId;
        private System.Windows.Forms.Label lblAdditionalData;
        private System.Windows.Forms.Label lblCardholderPinBlock;
        private HexaDecimalTextBox CardholderPinBlock;
        private IntegerTextBox InvoiceNumber;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.Label lblTransferData;
        private System.Windows.Forms.Label lblMessageAuthenticationCode;
        private HexaDecimalTextBox MessageAuthenticationCode;
        private NullCheckBox chkPrimaryAccountNumber;
        private NullCheckBox chkExpirationDate;
        private Bit63ContentBox TransferData;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private NullCheckBox chkTrack2Data;
        private NullCheckBox chkAdditionalData;
        private NullCheckBox chkTransferData;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Panel pnlLogon;
        private ProcessingCodeBox ProcessingCode2;
        private System.Windows.Forms.Label lblProcessingCode2;
        private System.Windows.Forms.Label lblSystemAuditTraceNumber2;
        private IntegerTextBox SystemAuditTraceNumber2;
        private System.Windows.Forms.Label lblNetworkInternationalId2;
        private IntegerTextBox NetworkInternationalId2;
        private IntegerTextBox TerminalId2;
        private System.Windows.Forms.Label lblTerminalId2;
        private ProcessingCodeBox ProcessingCode;
        private RequestDataEntry48Box AdditionalData;
        private NullCheckBox chkTransactionDescription;
        private StringTextBox TransactionDescription;
        private System.Windows.Forms.Label lblTransactionDescription;
        private NullCheckBox chkAdditionalField1;
        private StringTextBox AdditionalField1;
        private System.Windows.Forms.Label lblAdditionalField1;
        private NullCheckBox chkAdditionalField2;
        private IntegerTextBox AdditionalField2;
        private System.Windows.Forms.Label lblAdditionalField2;
    }
}