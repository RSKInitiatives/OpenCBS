namespace OpenCBS.GUI.Export
{
    partial class DateFieldPropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateFieldPropertiesForm));
            this.labelDateFormat = new System.Windows.Forms.Label();
            this.textBoxDateFormat = new System.Windows.Forms.TextBox();
            this.linkLabelDocumentation = new System.Windows.Forms.LinkLabel();
            this.labelSampleLabel = new System.Windows.Forms.Label();
            this.labelSampleValue = new System.Windows.Forms.Label();
            this.checkBoxAlignRight = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDateFormat
            // 
            resources.ApplyResources(this.labelDateFormat, "labelDateFormat");
            this.labelDateFormat.Name = "labelDateFormat";
            // 
            // textBoxDateFormat
            // 
            resources.ApplyResources(this.textBoxDateFormat, "textBoxDateFormat");
            this.textBoxDateFormat.Name = "textBoxDateFormat";
            this.textBoxDateFormat.TextChanged += new System.EventHandler(this.textBoxDateFormat_TextChanged);
            // 
            // linkLabelDocumentation
            // 
            resources.ApplyResources(this.linkLabelDocumentation, "linkLabelDocumentation");
            this.linkLabelDocumentation.Name = "linkLabelDocumentation";
            this.linkLabelDocumentation.TabStop = true;
            this.linkLabelDocumentation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDocumentation_LinkClicked);
            // 
            // labelSampleLabel
            // 
            resources.ApplyResources(this.labelSampleLabel, "labelSampleLabel");
            this.labelSampleLabel.Name = "labelSampleLabel";
            // 
            // labelSampleValue
            // 
            resources.ApplyResources(this.labelSampleValue, "labelSampleValue");
            this.labelSampleValue.Name = "labelSampleValue";
            // 
            // checkBoxAlignRight
            // 
            resources.ApplyResources(this.checkBoxAlignRight, "checkBoxAlignRight");
            this.checkBoxAlignRight.Name = "checkBoxAlignRight";
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // DateFieldPropertiesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxAlignRight);
            this.Controls.Add(this.linkLabelDocumentation);
            this.Controls.Add(this.textBoxDateFormat);
            this.Controls.Add(this.labelSampleValue);
            this.Controls.Add(this.labelSampleLabel);
            this.Controls.Add(this.labelDateFormat);
            this.Name = "DateFieldPropertiesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDateFormat;
        private System.Windows.Forms.TextBox textBoxDateFormat;
        private System.Windows.Forms.LinkLabel linkLabelDocumentation;
        private System.Windows.Forms.Label labelSampleLabel;
        private System.Windows.Forms.Label labelSampleValue;
        private System.Windows.Forms.CheckBox checkBoxAlignRight;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}