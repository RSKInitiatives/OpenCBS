using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Free.iso8583;
using Free.iso8583.example;
using Free.iso8583.example.model;

namespace Iso8583Client
{
    public partial class MessageEditor : Form
    {
        public MessageEditor()
        {
            InitializeComponent();

            byte[] mediaModeCodes = (byte[])Enum.GetValues(typeof(PosEntryMediaModeBytes));
            byte[] pinModeCodes = (byte[])Enum.GetValues(typeof(PosEntryPinModeBytes));
            this.PosEntryMode.Items.Clear();
            for (int i = 0; i < mediaModeCodes.Length; i++)
            {
                for (int j = 0; j < pinModeCodes.Length; j++)
                {
                    byte[] value = new byte[2];
                    value[0] = (byte)(mediaModeCodes[i] >> 4);
                    value[1] = (byte)((mediaModeCodes[i] << 4) | (pinModeCodes[j] & 0x0f));
                    this.PosEntryMode.AddItem(value,
                        MessageUtility.HexToString(value).Substring(1));
                }
            }

            byte[] conditionCodes = (byte[])Enum.GetValues(typeof(PosConditionCodeBytes));
            this.PosConditionCode.Items.Clear();
            for (int i = 0; i < conditionCodes.Length; i++)
            {
                byte[] value = new byte[] { conditionCodes[i] };
                this.PosConditionCode.AddItem(value, MessageUtility.HexToString(value));
            }
        }

        private EditedItem _editedItem;
        private Object _resultModel = null;

        public bool SetEditedItem(String key)
        {
            if (!EditedItems.Item.ContainsKey(key)) return false;
            _editedItem = EditedItems.Item[key];
            Panel panel = (Panel)this.GetType()
                .GetField(_editedItem.Panel, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(this);
            panel.BringToFront();
            Object o = _editedItem.DefaultModel;
            Type modelType = o.GetType();
            String suffix = _editedItem.SuffixName;
            lblModel.Text = modelType.FullName;

            PropertyInfo[] properties = modelType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                String name = property.Name + suffix;
                if (!panel.Controls.ContainsKey(name)) continue;
                Control control = panel.Controls[name];
                control.Visible = true;
                if (panel.Controls.ContainsKey("lbl" + name)) panel.Controls["lbl" + name].Visible = true;
                Type controlType = control.GetType();
                PropertyInfo propIsNull = controlType.GetProperty("IsNull");
                PropertyInfo propValue  = controlType.GetProperty("Value",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (propValue == null) propValue = controlType.GetProperty("Value");
                PropertyInfo propStringValue = controlType.GetProperty("StringValue");
                PropertyInfo propBytesValue = controlType.GetProperty("BytesValue",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (propBytesValue == null) propBytesValue = controlType.GetProperty("BytesValue");
                Object value = property.GetValue(o, null);
                
                byte[] bytesValue = null;
                try { bytesValue = (byte[])value; } catch {  }
                if (bytesValue == null)
                {
                    MethodInfo opConvert = Util.GetConvertOperator(property.PropertyType,
                        typeof(byte[]), false);
                    if (opConvert != null)
                    {
                        bytesValue = (byte[])opConvert.Invoke(null, new object[] { value });
                    }
                }

                if (value == null)
                {
                    propStringValue.SetValue(control, null, null);
                }
                else if (property.PropertyType == typeof(String))
                {
                    propStringValue.SetValue(control, value, null);
                }
                else if (!propValue.PropertyType.IsAssignableFrom(property.PropertyType)
                    && bytesValue != null)
                {
                    propBytesValue.SetValue(control, bytesValue, null);
                }
                else
                {
                    propValue.SetValue(control, value, null);
                }

                propIsNull.SetValue(control, value == null, null);
                String chkName = "chk" + name;
                if (panel.Controls.ContainsKey(chkName))
                {
                    ((CheckBox)panel.Controls[chkName]).Checked = value == null;
                    panel.Controls[chkName].Visible = true;
                }
            }
            
            return true;
        }

        public Object ResultModel { get { return _resultModel; } }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Object o = _editedItem.DefaultModel;
            if (o == null) return;
            Panel panel = (Panel)this.GetType()
                .GetField(_editedItem.Panel, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(this);
            Type modelType = o.GetType();
            String suffix = _editedItem.SuffixName;
            _resultModel = modelType.GetConstructor(new Type[] {}).Invoke(new object[] {});

            PropertyInfo[] properties = modelType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                String name = property.Name + suffix;
                if (!panel.Controls.ContainsKey(name)) continue;
                Object control = panel.Controls[name];
                Type controlType = control.GetType();
                PropertyInfo propIsNull = controlType.GetProperty("IsNull");
                bool isNull = (bool)propIsNull.GetValue(control, null);
                PropertyInfo propValue = controlType.GetProperty("Value",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (propValue == null) propValue = controlType.GetProperty("Value");
                PropertyInfo propStringValue = controlType.GetProperty("StringValue");
                PropertyInfo propBytesValue = controlType.GetProperty("BytesValue",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (propBytesValue == null) propBytesValue = controlType.GetProperty("BytesValue");
                
                Object castBytesValue = null;
                MethodInfo opConvert = Util.GetConvertOperator(property.PropertyType, typeof(byte[]), true);
                Object bytesValue = propBytesValue.GetValue(control, null);
                if (opConvert != null && bytesValue != null)
                {
                    castBytesValue = opConvert.Invoke(null,
                        new object[] { bytesValue });
                }
                
                if (isNull)
                {
                    property.SetValue(_resultModel, null, null);
                }
                else if (property.PropertyType == typeof(String))
                {
                    property.SetValue(_resultModel, propStringValue.GetValue(control, null), null);
                }
                else if (property.PropertyType == typeof(byte[]))
                {
                    property.SetValue(_resultModel, bytesValue, null);
                }
                else if (!property.PropertyType.IsAssignableFrom(propValue.PropertyType)
                    && castBytesValue != null)
                {
                    property.SetValue(_resultModel, castBytesValue, null);
                }
                else
                {
                    property.SetValue(_resultModel, propValue.GetValue(control, null), null);
                }
            }

            this.DialogResult = DialogResult.OK;
            CloseMe();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            CloseMe();
        }

        private void CloseMe()
        {
            //this.Close();
            _editedItem.DefaultModel = null;
        }

        private void MessageEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
