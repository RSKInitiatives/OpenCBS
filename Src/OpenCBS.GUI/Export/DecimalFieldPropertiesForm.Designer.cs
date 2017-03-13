namespace OpenCBS.GUI.Export
{
    partial class DecimalFieldPropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecimalFieldPropertiesForm));
            this.labelDecimalNumber = new System.Windows.Forms.Label();
            this.tnDecimalNumber = new OpenCBS.GUI.UserControl.TextNumericUserControl();
            this.labelDecimalSeparator = new System.Windows.Forms.Label();
            this.textBoxDecimalSeparator = new System.Windows.Forms.TextBox();
            this.labelGroupSeparator = new System.Windows.Forms.Label();
            this.textBoxGroupSeparator = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxAlignRight = new System.Windows.Forms.CheckBox();
            this.labelSampleValue = new System.Windows.Forms.Label();
            this.labelSampleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDecimalNumber
            // 
            resources.ApplyResources(this.labelDecimalNumber, "labelDecimalNumber");
            this.labelDecimalNumber.Name = "labelDecimalNumber";
            // 
            // tnDecimalNumber
            // 
            resources.ApplyResources(this.tnDecimalNumber, "tnDecimalNumber");
            this.tnDecimalNumber.Name = "tnDecimalNumber";
            this.tnDecimalNumber.NumberChanged += new System.EventHandler(this.tnDecimalNumber_NumberChanged);
            // 
            // labelDecimalSeparator
            // 
            resources.ApplyResources(this.labelDecimalSeparator, "labelDecimalSeparator");
            this.labelDecimalSeparator.Name = "labelDecimalSeparator";
            // 
            // textBoxDecimalSeparator
            // 
            resources.ApplyResources(this.textBoxDecimalSeparator, "textBoxDecimalSeparator");
            this.textBoxDecimalSeparator.Name = "textBoxDecimalSeparator";
            this.textBoxDecimalSeparator.TextChanged += new System.EventHandler(this.textBoxDecimalSeparator_TextChanged);
            // 
            // labelGroupSeparator
            // 
            resources.ApplyResources(this.labelGroupSeparator, "labelGroupSeparator");
            this.labelGroupSeparator.Name = "labelGroupSeparator";
            // 
            // textBoxGroupSeparator
            // 
            resources.ApplyResources(this.textBoxGroupSeparator, "textBoxGroupSeparator");
            this.textBoxGroupSeparator.Name = "textBoxGroupSeparator";
            this.textBoxGroupSeparator.TextChanged += new System.EventHandler(this.textBoxGroupSeparator_TextChanged);
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
            // checkBoxAlignRight
            // 
            resources.ApplyResources(this.checkBoxAlignRight, "checkBoxAlignRight");
            this.checkBoxAlignRight.Name = "checkBoxAlignRight";
            // 
            // labelSampleValue
            // 
            resources.ApplyResources(this.labelSampleValue, "labelSampleValue");
            this.labelSampleValue.Name = "labelSampleValue";
            // 
            // labelSampleLabel
            // 
            resources.ApplyResources(this.labelSampleLabel, "labelSampleLabel");
            this.labelSampleLabel.Name = "labelSampleLabel";
            // 
            // DecimalFieldPropertiesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSampleValue);
            this.Controls.Add(this.labelSampleLabel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxAlignRight);
            this.Controls.Add(this.textBoxGroupSeparator);
            this.Controls.Add(this.textBoxDecimalSeparator);
            this.Controls.Add(this.labelGroupSeparator);
            this.Controls.Add(this.labelDecimalSeparator);
            this.Controls.Add(this.tnDecimalNumber);
            this.Controls.Add(this.labelDecimalNumber);
            this.Name = "DecimalFieldPropertiesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDecimalNumber;
        private OpenCBS.GUI.UserControl.TextNumericUserControl tnDecimalNumber;
        private System.Windows.Forms.Label labelDecimalSeparator;
        private System.Windows.Forms.TextBox textBoxDecimalSeparator;
        private System.Windows.Forms.Label labelGroupSeparator;
        private System.Windows.Forms.TextBox textBoxGroupSeparator;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxAlignRight;
        private System.Windows.Forms.Label labelSampleValue;
        private System.Windows.Forms.Label labelSampleLabel;
    }
}