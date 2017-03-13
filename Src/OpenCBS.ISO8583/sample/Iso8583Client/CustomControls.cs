/**
 * Custom Controls
 * Developed by AT Mulyana (atmulyana@yahoo.com)
 * CopyLeft (ᴐ) 2012
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Free.iso8583;
using Free.iso8583.example;
using Free.iso8583.example.model;

namespace Iso8583Client
{
    public interface IControlValue
    {
        [Browsable(false)]
        String StringValue { get; set; }

        [Browsable(false)]
        byte[] BytesValue { get; set; }

        [Browsable(false)]
        bool IsNull { get; set; }
    }
    public interface IControlValue<T> : IControlValue
    {
        [Browsable(false)]
        T Value { get; set; }
    }

    public abstract class AbstractNumericTextBox : TextBox
    {
        public int Length { get; set; }
        
        protected virtual bool IsValidChar(char ch)
        {
            return ('0' <= ch && ch <= '9');
        }

        protected bool IsValidKey(char ch)
        {
            return ch == (char)Keys.Back || ch == (char)Keys.Tab || ch == (char)Keys.Return;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!IsValidChar(e.KeyChar) && !IsValidKey(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyPress(e);
            }
        }

        protected String _textValue = "";
        protected virtual bool CheckTextChar(int pos)
        {
            return this.IsValidChar(_textValue[pos]);
        }

        public override String Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (value != null)
                {
                    if (value == this.Name) //To avoid error because of dropping this Control into the Form in design mode; To be reviewed
                    {
                        _textValue = "";
                    }
                    else
                    {
                        _textValue = value;
                    }
                    for (int i = 0; i < _textValue.Length; i++)
                    {
                        if (!CheckTextChar(i)) throw new ArgumentException("Invalid Text's value", "Text");
                    }
                }
                base.Text = value;
            }
        }

        [Browsable(false)]
        public String StringValue { get { return this.Text; } set { this.Text = value; } }
        [Browsable(false)]
        public bool IsNull { get; set; }
    }


    public class StringTextBox : TextBox, IControlValue<String>
    {
        [Browsable(false)]
        public string Value
        {
            get { return Text; }
            set { Text = value;  }
        }


        [Browsable(false)]
        public string StringValue
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get { return this.Text==null ? null : MessageUtility.StringToAsciiArray(this.Text); }
            set { this.Text = value==null ? null : MessageUtility.AsciiArrayToString(value); }
        }

        [Browsable(false)]
        public bool IsNull { get; set; }
    }

    /*** TextBox that only accepts hexadecimal value ***/
    public class HexaDecimalTextBox : AbstractNumericTextBox, IControlValue<byte[]>
    {
        public HexaDecimalTextBox() : base()
        {
        }
        
        public bool IsLowerCase { get; set; }
        public bool LeftAligned { get; set; }

        protected override bool IsValidChar(char ch)
        {
            return base.IsValidChar(ch) || ('a' <= ch && ch <= 'f') || ('A' <= ch && ch <= 'F');
        }

        protected override void OnLostFocus(EventArgs e)
        {
            String text = IsLowerCase ? this.Text.ToLower() : this.Text.ToUpper();
            if (this.Length > 0)
            {
                if (text.Length < this.Length) text = text.PadLeft(this.Length, '0');
                else if (text.Length > this.Length) text = text.Substring(text.Length - this.Length, this.Length);
            }
            if (text.Length % 2 == 1)
            {
                text = this.LeftAligned ? text + "0" : "0" + text;
            }
            this.Text = text;
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get { return Value; }
            set { Value = value; }
        }

        [Browsable(false)]
        public byte[] Value
        {
            get
            {
                String text = (Text == null ? "" : Text);
                return this.Length <= 0 ? MessageUtility.StringToHex(text, LeftAligned)
                    : MessageUtility.StringToHex(text, this.Length, LeftAligned);
            }
            set
            {
                Text = MessageUtility.HexToString(value);
            }
        }
    }


    /*** TextBox that only accepts integer value ***/
    public class IntegerTextBox : AbstractNumericTextBox, IControlValue<int>
    {
        public IntegerTextBox() : base()
        {
        }
        
        public bool AcceptNegative { get; set; }

        protected override bool IsValidChar(char ch)
        {
            if (ch == '-' && AcceptNegative && this.SelectionStart == 0) return true;
            return base.IsValidChar(ch);
        }

        protected override bool CheckTextChar(int pos)
        {
            char ch = _textValue[pos];
            if (ch == '-' && AcceptNegative && pos == 0) return true;
            return base.IsValidChar(ch);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (Length <= 0) return;
            String text = Text;
            bool isNegative = false;
            if (text.Length > 0 && text[0] == '-')
            {
                text = text.Substring(1);
                isNegative = true;
            }
            int length = Length;
            if (text.IndexOf('.') != -1) length += 1;

            if (length >= text.Length) text = text.PadLeft(length, '0');
            else text = text.Substring(text.Length - length, length);
            if (isNegative) text = '-' + text;
            this.Text = text;
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get
            {
                String text = String.IsNullOrEmpty(Text) ? "0" : Text;
                if (text.StartsWith("-")) return null;
                return MessageUtility.StringToHex(text);
            }
            set { Text = value == null ? "0" : MessageUtility.HexToString(value); }
        }

        [Browsable(false)]
        public int Value
        {
            get { return String.IsNullOrEmpty(Text) ? 0 : int.Parse(Text); }
            set { Text = value.ToString(); }
        }
    }


    /*** TextBox that only accepts decimal value ***/
    public class DecimalTextBox : IntegerTextBox, IControlValue<decimal>
    {
        public DecimalTextBox() : base()
        {
        }
        
        private int fracDigits = 2;
        public int FracDigits
        {
            get { return fracDigits; }
            set { if (value >= 0) fracDigits = value; }
        }

        protected override bool IsValidChar(char ch)
        {
            if (ch == '.' && this.Text.IndexOf('.') == -1) return true;
            return base.IsValidChar(ch);
        }

        protected override bool CheckTextChar(int pos)
        {
            if (_textValue[pos] == '.' && pos == _textValue.IndexOf('.')) return true;
            return base.CheckTextChar(pos);
        }

        [Browsable(false)]
        public new byte[] BytesValue
        {
            get {
                if (Value < 0) return null;
                return MessageUtility.IntToHex((ulong)Math.Round(Value * (decimal)Math.Pow(10,fracDigits)));
            }
            set { Value = value == null ? 0 : MessageUtility.HexToDecimal(value, fracDigits); }
        }

        [Browsable(false)]
        public new decimal Value
        {
            get { return String.IsNullOrEmpty(Text) ? 0 : decimal.Parse(Text); }
            set { Text = value.ToString(); }
        }
    }


    /*** TextBox to input date value in format of 'mm/dd' (Mode=0), 'yy/mm' (Mode=1), 'yy/mm/dd' (Mode=2) ***/
    public class DateTextBox : UserControl, IControlValue<DateTime>
    {
        public DateTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label1;
    
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(0, 0);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(42, 20);
            this.maskedTextBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            // 
            // DateTextBox1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskedTextBox1);
            this.Name = "DateTextBox1";
            this.Size = new System.Drawing.Size(100, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

            this.Mode = 0;
            this.Value = DateTime.Now;
        }

        #endregion

        private int mode = 0;
        public int Mode {
            get
            {
                return mode;
            }
            set
            {
                if (mode < 0 || mode > 2) return;
                mode = value;
                switch (value)
                {
                    case 0:
                        this.maskedTextBox1.Mask = "00/00";
                        this.label1.Text = "mm/dd";
                        break;
                    case 1:
                        this.maskedTextBox1.Mask = "00/00";
                        this.label1.Text = "yy/mm";
                        break;
                    case 2:
                        this.maskedTextBox1.Mask = "00/00/00";
                        this.label1.Text = "yy/mm/dd";
                        break;
                }
                this.Value = DateTime.Now;
            }
        }

        [Browsable(false)]
        public DateTime Value
        {
            get
            {
                DateTime dt = DateTime.Now;
                String[] dates  = this.maskedTextBox1.Text.Split(new char[] {'/'});
                if (mode == 1) //yy/mm
                {
                    dt = new DateTime(int.Parse(dates[0]), int.Parse(dates[1]), dt.Day);
                }
                else if (mode == 2) //yy/mm/dd
                {
                    dt = new DateTime(int.Parse(dates[0]), int.Parse(dates[1]), int.Parse(dates[2]));
                }
                else //mm/dd
                {
                    dt = new DateTime(dt.Year, int.Parse(dates[0]), int.Parse(dates[1]));
                }
                return dt;
            }
            set
            {
                String day = (value.Day + "").PadLeft(2, '0');
                String month = (value.Month + "").PadLeft(2, '0');
                String year = ((value.Year % 100) + "").PadLeft(2, '0');
                if (mode == 1)
                {
                    this.maskedTextBox1.Text = year + "/" + month;
                }
                else if (mode == 2)
                {
                    this.maskedTextBox1.Text = year + "/" + month + "/" + day;
                }
                else
                {
                    this.maskedTextBox1.Text = month + "/" + day;
                }
            }
        }

        [Browsable(false)]
        public string StringValue
        {
            get { return this.maskedTextBox1.Text==null ? null : this.maskedTextBox1.Text.Replace("/", ""); }
            set {
                if (value == null) this.maskedTextBox1.Text = null;
                else if (mode == 2) this.maskedTextBox1.Text = value.Insert(4, "/").Insert(2, "/");
                else this.maskedTextBox1.Text = value.Insert(2, "/");
            }
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get { return StringValue==null ? null : MessageUtility.StringToHex(StringValue); }
            set { StringValue = value == null ? null : MessageUtility.HexToString(value); }
        }

        [Browsable(false)]
        public bool IsNull { get; set; }
    }


    /*** TextBox to input time value in format of 'hh:mm:ss' ***/
    public class TimeTextBox : UserControl, IControlValue<DateTime>
    {
        public TimeTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label1;
        
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(0, 0);
            this.maskedTextBox1.Mask = "00:00:00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(52, 20);
            this.maskedTextBox1.TabIndex = 0;
            String hour = (DateTime.Now.Hour + "").PadLeft(2, '0');
            String minute = (DateTime.Now.Minute + "").PadLeft(2, '0');
            String second = (DateTime.Now.Second + "").PadLeft(2, '0');
            this.maskedTextBox1.Text = hour + ":" + minute + ":" + second;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "hh:mm:ss";
            // 
            // TimeTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskedTextBox1);
            this.Name = "TimeTextBox";
            this.Size = new System.Drawing.Size(111, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        [Browsable(false)]
        public DateTime Value
        {
            get
            {
                DateTime dt = DateTime.Now;
                String[] times = this.maskedTextBox1.Text.Split(new char[] { ':' });
                dt = new DateTime(dt.Year, dt.Month, dt.Day,
                    int.Parse(times[0]), int.Parse(times[1]), int.Parse(times[2]));
                return dt;
            }
            set
            {
                String hour = (value.Hour + "").PadLeft(2, '0');
                String minute = (value.Minute + "").PadLeft(2, '0');
                String second = (value.Second + "").PadLeft(2, '0');
                this.maskedTextBox1.Text = hour + ":" + minute + ":" + second;
            }
        }

        [Browsable(false)]
        public string StringValue
        {
            get { return this.maskedTextBox1.Text==null ? null : this.maskedTextBox1.Text.Replace(":", ""); }
            set { this.maskedTextBox1.Text = value==null ? null : value.Insert(4, ":").Insert(2, ":"); }
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get { return StringValue == null ? null : MessageUtility.StringToHex(StringValue); }
            set { StringValue = value==null ? null : MessageUtility.HexToString(value); }
        }

        [Browsable(false)]
        public bool IsNull { get; set; }
    }


    public class ComboItem<T> : IControlValue<T>
    {
        public ComboItem(T value, String text)
        {
            this.Value = value;
            this.Text = text;
        }
        
        public String Text { get; set; }
        public T Value { get; set; }
        public byte[] BytesValue
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual String StringValue
        {
            get
            {
                Object val = Value;
                if (val is byte[]) return MessageUtility.HexToString((byte[])val);
                return val.ToString();
            }
            set
            {
                if (value == null)
                {
                    Value = default(T);
                    return;
                }
                Type valueType = typeof(T);
                System.Reflection.MethodInfo parseMethod = valueType.GetMethod("Parse",
                       new Type[] {typeof(String)} );
                if (valueType.Equals(typeof(byte[])))
                {
                    Object val = MessageUtility.StringToHex(value);
                    Value = (T)val;
                }
                else if (parseMethod != null && parseMethod.IsStatic)
                {
                    Value = (T)parseMethod.Invoke(null, new object[] { value });
                }
                else
                {
                    throw new ArgumentException("Cannot convert the string \"" + value
                        + "\" to be type of " + valueType.Name);
                }
            }
        }

        public bool IsNull { get; set; }
        
        public override String ToString()
        {
            return this.Text;
        }
    }


    /*** Extended ComboBox Control ***/
    public abstract class ValueComboBox<T> : ComboBox, IControlValue<T>
    {
        public void AddItem(T value, String text)
        {
            ComboItem<T> item = new ComboItem<T>(value, text);
            this.Items.Add(item);
        }

        [Browsable(false)]
        public T Value
        {
            get
            {
                if (this.SelectedIndex < 0) return default(T);
                return ((ComboItem<T>)this.SelectedItem).Value;
            }
            set
            {
                int i = -1;
                Object val = value;
                if (val == null)
                {
                    for (int j = 0; j < this.Items.Count; j++)
                    {
                        ComboItem<T> item = (ComboItem<T>)this.Items[j];
                        if (item.Value == null)
                        {
                            i = j;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < this.Items.Count; j++)
                    {
                        ComboItem<T> item = (ComboItem<T>)this.Items[j];
                        Object val2 = item.Value;
                        if (val.Equals(val2) || (val is System.Collections.ICollection &&
                                ((System.Collections.ICollection)val).Equals2((System.Collections.ICollection)val2))
                        )
                        {
                            i = j;
                            break;
                        }
                    }
                }
                this.SelectedIndex = i;
            }
        }

        [Browsable(false)]
        public virtual String StringValue
        {
            get
            {
                if (this.SelectedIndex < 0) return null;
                return ((ComboItem<T>)this.SelectedItem).StringValue;
            }
            set
            {
                if (value == null)
                {
                    this.SelectedIndex = -1;
                    return;
                }
                Type valueType = typeof(T);
                System.Reflection.MethodInfo parseMethod = valueType.GetMethod("Parse",
                       new Type[] { typeof(String) });
                if (valueType.Equals(typeof(byte[])))
                {
                    Object val = MessageUtility.StringToHex(value);
                    Value = (T)val;
                }
                else if (parseMethod != null && parseMethod.IsStatic)
                {
                    Value = (T)parseMethod.Invoke(null, new object[] { value });
                }
                else
                {
                    throw new ArgumentException("Cannot convert the string \"" + value
                        + "\" to be type of " + valueType.Name);
                }
            }
        }

        [Browsable(false)]
        public abstract byte[] BytesValue { get; set; }

        [Browsable(false)]
        public bool IsNull { get; set; }
    }
    public class BytesComboBox : ValueComboBox<byte[]>
    {
        [Browsable(false)]
        public override byte[] BytesValue { get { return Value; } set { Value = value; } }
    }


    /*** The checkbox for NULL checking ***/
    public class NullCheckBox : CheckBox
    {
        public NullCheckBox() : base()
        {
            this.Text = "NULL";
        }

        public String NullControlName { get; set; }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = "NULL";
            }
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            if (String.IsNullOrEmpty(this.NullControlName)) return;
            Control ctl = this.Parent.Controls[this.NullControlName];
            if (ctl == null) return;
            if (this.Checked)
            {
                ctl.Text = null;
                ctl.Enabled = false;
                ((IControlValue)ctl).IsNull = true;
            }
            else
            {
                ctl.Text = "";
                ctl.Enabled = true;
                ((IControlValue)ctl).IsNull = false;
            }
        }
    }


    /*** The control for inputing the processing code of ISO 8583 message ***/
    public class ProcessingCodeBox : UserControl, IControlValue<ProcessingCode>
    {
        public ProcessingCodeBox()
        {
            InitializeComponent();
        }

        private System.ComponentModel.IContainer components = null;

        private IntegerTextBox TransactionType;
        private Iso8583Client.BytesComboBox FromAccount;
        private Iso8583Client.BytesComboBox ToAccount;
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.TransactionType = new Iso8583Client.IntegerTextBox();
            this.FromAccount = new Iso8583Client.BytesComboBox();
            this.ToAccount = new Iso8583Client.BytesComboBox();
            this.SuspendLayout();
            // 
            // TransactionType
            // 
            this.TransactionType.AcceptNegative = false;
            this.TransactionType.Enabled = false;
            this.TransactionType.Length = 0;
            this.TransactionType.Location = new System.Drawing.Point(0, 0);
            this.TransactionType.MaxLength = 2;
            this.TransactionType.Name = "TransactionType";
            this.TransactionType.Size = new System.Drawing.Size(28, 20);
            this.TransactionType.TabIndex = 4;
            this.TransactionType.ReadOnly = true;
            this.TransactionTypeCode = TransactionTypeCodeBytes.BALANCE_INQUIRY;

            byte[] accountCodes = (byte[])Enum.GetValues(typeof(AccountCodeBytes));

            // 
            // FromAccount
            // 
            this.FromAccount.FormattingEnabled = true;
            this.FromAccount.Location = new System.Drawing.Point(34, 0);
            this.FromAccount.Name = "FromAccount";
            this.FromAccount.Size = new System.Drawing.Size(300, 21);
            this.FromAccount.TabIndex = 5;
            foreach (byte accountCode in accountCodes)
            {
                this.FromAccount.AddItem(
                    new byte[] {accountCode},
                    MessageUtility.HexToString(accountCode) + " - "
                        + ProcessingCode.ACCOUNT[(AccountCodeBytes)accountCode]
                );
            }
            this.FromAccount.SelectedIndex = 0;

            // 
            // ToAccount
            // 
            this.ToAccount.FormattingEnabled = true;
            this.ToAccount.Location = new System.Drawing.Point(340, 0);
            this.ToAccount.Name = "ToAccount";
            this.ToAccount.Size = new System.Drawing.Size(300, 21);
            this.ToAccount.TabIndex = 6;
            foreach (byte accountCode in accountCodes)
            {
                this.ToAccount.AddItem(
                    new byte[] { accountCode },
                    MessageUtility.HexToString(accountCode) + " - "
                        + ProcessingCode.ACCOUNT[(AccountCodeBytes)accountCode]
                );
            }
            this.ToAccount.SelectedIndex = 0;

            // 
            // ProcessingCodeBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToAccount);
            this.Controls.Add(this.FromAccount);
            this.Controls.Add(this.TransactionType);
            this.Name = "ProcessingCodeBox";
            this.Size = new System.Drawing.Size(640, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TransactionTypeCodeBytes transactionTypeCode = TransactionTypeCodeBytes.BALANCE_INQUIRY;
        public TransactionTypeCodeBytes TransactionTypeCode
        {
            get
            {
                return transactionTypeCode;
            }
            set
            {
                transactionTypeCode = value;
                this.TransactionType.Text = MessageUtility.HexToString((byte)transactionTypeCode);
            }
        }

        private void SetValueNull()
        {
            transactionTypeCode = TransactionTypeCodeBytes.BALANCE_INQUIRY;
            this.FromAccount.SelectedIndex = -1;
            this.ToAccount.SelectedIndex = -1;
        }

        [Browsable(false)]
        public ProcessingCode Value
        {
            get
            {
                byte[] val = new byte[] { (byte)transactionTypeCode };
                if (this.FromAccount.Value == null || this.ToAccount.Value == null) return null;
                return val.Concat(this.FromAccount.Value).Concat(this.ToAccount.Value).ToArray();
            }
            set
            {
                if (value != null)
                {
                    transactionTypeCode = value.TransactionType;
                    this.FromAccount.Value = new byte[] { (byte)value.FromAccount };
                    this.ToAccount.Value = new byte[] { (byte)value.ToAccount };
                }
                else
                {
                    SetValueNull();
                }
            }
        }

        [Browsable(false)]
        public String StringValue
        {
            get { return Value==null ? null : MessageUtility.HexToString(Value); }
            set
            {
                if (value == null) this.Value = null;
                else this.Value = MessageUtility.StringToHex(value);
            }
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get { ProcessingCode value = Value;  return value == null ? null : value.BytesValue; }
            set { if (value != null) Value = value; else SetValueNull(); }
        }
        
        [Browsable(false)]
        public bool IsNull { get; set; }
    }


    /*** The control for inputing the AdditionalData field of ISO 8583 message ***/
    public class RequestDataEntry48Box : UserControl, IControlValue<Free.iso8583.example.model.RequestDataEntry48>
    {
        public RequestDataEntry48Box()
        {
            InitializeComponent();
        }

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label label1;
        private IntegerTextBox ProductCode;
        private System.Windows.Forms.Label label2;
        private StringTextBox Note;
        private System.Windows.Forms.Label label3;
        private IntegerTextBox Fee;
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ProductCode = new Iso8583Client.IntegerTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Note = new Iso8583Client.StringTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Fee = new Iso8583Client.IntegerTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ProductCode";
            // 
            // ProductCode
            // 
            this.ProductCode.AcceptNegative = false;
            this.ProductCode.Length = 0;
            this.ProductCode.Location = new System.Drawing.Point(75, 0);
            this.ProductCode.MaxLength = 5;
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Size = new System.Drawing.Size(50, 20);
            this.ProductCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Note";
            // 
            // Note
            // 
            this.Note.Location = new System.Drawing.Point(75, 22);
            this.Note.MaxLength = 50;
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(350, 20);
            this.Note.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fee";
            // 
            // Fee
            // 
            this.Fee.AcceptNegative = false;
            this.Fee.Length = 0;
            this.Fee.Location = new System.Drawing.Point(75, 44);
            this.Fee.MaxLength = 9;
            this.Fee.Name = "Fee";
            this.Fee.Size = new System.Drawing.Size(100, 20);
            this.Fee.TabIndex = 5;
            // 
            // RequestDataEntry48Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Fee);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProductCode);
            this.Controls.Add(this.label1);
            this.Name = "RequestDataEntry48Box";
            this.Size = new System.Drawing.Size(425, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Free.iso8583.example.model.RequestDataEntry48 value = null;
        [Browsable(false)]
        public Free.iso8583.example.model.RequestDataEntry48 Value
        {
            get
            {
                value = new Free.iso8583.example.model.RequestDataEntry48();
                value.ProductCode = this.ProductCode.Text;
                value.Note = this.Note.Text;
                value.Fee = this.Fee.Value;
                return value;
            }
            set
            {
                if (value != null)
                {
                    this.ProductCode.Text = value.ProductCode;
                    this.Note.Text = value.Note;
                    this.Fee.Value = (int)value.Fee;
                }
                else
                {
                    this.ProductCode.Text = "";
                    this.Note.Text = "";
                    this.Fee.Text = "";
                }
            }
        }

        [Browsable(false)]
        public string StringValue
        {
            get
            {
                if (value == null) return null;
                String s = "";
                s += (value.ProductCode == null ? "" : value.ProductCode).PadLeft(5, '0');
                s += (value.Note == null ? "" : value.Note).PadRight(50, ' ');
                String sFee = (int)value.Fee + "";
                s += "FD" + sFee.Length + sFee;
                return s;
            }
            set
            {
                if (value == null)
                {
                    this.value = null;
                    return;
                }
                Free.iso8583.example.model.RequestDataEntry48 val = new Free.iso8583.example.model.RequestDataEntry48();
                val.ProductCode = value.Substring(0, 5);
                val.Note = value.Substring(5, 50);
                String sFee = value.Substring(58);
                val.Fee = String.IsNullOrEmpty(sFee) ? 0 : int.Parse(sFee);
                Value = val;
            }
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get
            { 
                String value = StringValue;
                return value==null ? null :MessageUtility.StringToAsciiArray(StringValue);
            }
            set { StringValue = value==null ? null : MessageUtility.AsciiArrayToString(value); }
        }

        [Browsable(false)]
        public bool IsNull { get; set; }
    }


    /*** The control for inputing the TransferData field of ISO 8583 message ***/
    public class Bit63ContentBox : UserControl, IControlValue<Bit63Content>
    {
        public Bit63ContentBox()
        {
            InitializeComponent();
        }

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label label1;
        private IntegerTextBox TableId;
        private System.Windows.Forms.Label label2;
        private IntegerTextBox BeneficiaryInstitutionId;
        private System.Windows.Forms.Label label3;
        private IntegerTextBox BeneficiaryAccountNumber;
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TableId = new Iso8583Client.IntegerTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BeneficiaryInstitutionId = new Iso8583Client.IntegerTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BeneficiaryAccountNumber = new Iso8583Client.IntegerTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TableId";
            // 
            // TableId
            // 
            this.TableId.AcceptNegative = false;
            this.TableId.Length = 2;
            this.TableId.Location = new System.Drawing.Point(150, 0);
            this.TableId.MaxLength = 2;
            this.TableId.Name = "TableId";
            this.TableId.Size = new System.Drawing.Size(25, 20);
            this.TableId.TabIndex = 1;
            this.TableId.Text = "00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "BeneficiaryInstitutionId";
            // 
            // BeneficiaryInstitutionId
            // 
            this.BeneficiaryInstitutionId.AcceptNegative = false;
            this.BeneficiaryInstitutionId.Length = 11;
            this.BeneficiaryInstitutionId.Location = new System.Drawing.Point(150, 22);
            this.BeneficiaryInstitutionId.MaxLength = 11;
            this.BeneficiaryInstitutionId.Name = "BeneficiaryInstitutionId";
            this.BeneficiaryInstitutionId.Size = new System.Drawing.Size(125, 20);
            this.BeneficiaryInstitutionId.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "BeneficiaryAccountNumber";
            // 
            // BeneficiaryAccountNumber
            // 
            this.BeneficiaryAccountNumber.Location = new System.Drawing.Point(150, 44);
            this.BeneficiaryAccountNumber.MaxLength = 28;
            this.BeneficiaryAccountNumber.Name = "BeneficiaryAccountNumber";
            this.BeneficiaryAccountNumber.Size = new System.Drawing.Size(305, 20);
            this.BeneficiaryAccountNumber.TabIndex = 5;
            // 
            // Bit63ContentBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BeneficiaryAccountNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BeneficiaryInstitutionId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TableId);
            this.Controls.Add(this.label1);
            this.Name = "Bit63ContentBox";
            this.Size = new System.Drawing.Size(455, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        [Browsable(false)]
        public Bit63Content Value
        {
            get
            {
                Bit63Content value = new Bit63Content();
                value.TableId = this.TableId.Text;
                value.BeneficiaryInstitutionId = this.BeneficiaryInstitutionId.Text;
                value.BeneficiaryAccountNumber = this.BeneficiaryAccountNumber.Text;
                return value;
            }
            set
            {
                if (value != null)
                {
                    this.TableId.Text = value.TableId;
                    this.BeneficiaryInstitutionId.Text = value.BeneficiaryInstitutionId;
                    this.BeneficiaryAccountNumber.Text = value.BeneficiaryAccountNumber;
                }
                else
                {
                    this.TableId.Text = "";
                    this.BeneficiaryInstitutionId.Text = "";
                    this.BeneficiaryAccountNumber.Text = "";
                }
            }
        }

        [Browsable(false)]
        public string StringValue
        {
            get
            {
                return this.TableId.Text.PadLeft(2, '0') + this.BeneficiaryInstitutionId.Text.PadLeft(11, '0')
                    + this.BeneficiaryAccountNumber.Text.PadRight(28, ' ');
            }
            set
            {
                if (value != null)
                {
                    this.TableId.Text = value.Substring(0, 2);
                    this.BeneficiaryInstitutionId.Text = value.Substring(2, 11);
                    this.BeneficiaryAccountNumber.Text = value.Substring(13).TrimEnd();
                }
                else
                {
                    this.TableId.Text = "";
                    this.BeneficiaryInstitutionId.Text = "";
                    this.BeneficiaryAccountNumber.Text = "";
                }
            }
        }

        [Browsable(false)]
        public byte[] BytesValue
        {
            get { return MessageUtility.StringToAsciiArray(StringValue); }
            set { StringValue = MessageUtility.AsciiArrayToString(value); }
        }

        [Browsable(false)]
        public bool IsNull { get; set; }
    }


    /*** Makes the ICollection object has the true Equals method ***/
    public static class ICollectionEqualsExt
    {
        public static bool Equals2(this System.Collections.ICollection col, System.Collections.ICollection col2)
        {
            if (col == col2) return true; //Either the same instance or both of them are null
            if (col == null || col2 == null) return false; //Only one is null
            if (col.Count != col2.Count) return false;
            Object[] arCol = new Object[col.Count];
            int i = 0;
            foreach (Object item in col)
            {
                arCol[i] = item;
                i++;
            }

            bool isEqual = true;
            i = 0;
            foreach (Object item in col2)
            {
                if ((item is System.Collections.ICollection) && (arCol[i] is System.Collections.ICollection))
                {
                    isEqual = isEqual && ((System.Collections.ICollection)item)
                                   .Equals2((System.Collections.ICollection)arCol[i]);
                }
                else
                {
                    isEqual = isEqual && (
                               (item == null && arCol[i] == null)
                            || (item != null && item.Equals(arCol[i]))
                            || (arCol[i] != null && arCol[i].Equals(item))
                        );
                }
                i++;
            }
            return isEqual;
        }
    }

}
