namespace OpenCBS.GUI.Export
{
    partial class StringFieldPropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringFieldPropertiesForm));
            this.labelStartPosition = new System.Windows.Forms.Label();
            this.checkBoxAlignRight = new System.Windows.Forms.CheckBox();
            this.dgvReplacementList = new System.Windows.Forms.DataGridView();
            this.ColumnOriginalValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplacementValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tnStartPosition = new OpenCBS.GUI.UserControl.TextNumericUserControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEndPosition = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReplacementList)).BeginInit();
            this.SuspendLayout();
            // 
            // labelStartPosition
            // 
            resources.ApplyResources(this.labelStartPosition, "labelStartPosition");
            this.labelStartPosition.Name = "labelStartPosition";
            // 
            // checkBoxAlignRight
            // 
            resources.ApplyResources(this.checkBoxAlignRight, "checkBoxAlignRight");
            this.checkBoxAlignRight.Name = "checkBoxAlignRight";
            // 
            // dgvReplacementList
            // 
            resources.ApplyResources(this.dgvReplacementList, "dgvReplacementList");
            this.dgvReplacementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReplacementList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnOriginalValue,
            this.ColumnReplacementValue});
            this.dgvReplacementList.Name = "dgvReplacementList";
            // 
            // ColumnOriginalValue
            // 
            resources.ApplyResources(this.ColumnOriginalValue, "ColumnOriginalValue");
            this.ColumnOriginalValue.Name = "ColumnOriginalValue";
            // 
            // ColumnReplacementValue
            // 
            resources.ApplyResources(this.ColumnReplacementValue, "ColumnReplacementValue");
            this.ColumnReplacementValue.Name = "ColumnReplacementValue";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // tnStartPosition
            // 
            resources.ApplyResources(this.tnStartPosition, "tnStartPosition");
            this.tnStartPosition.Name = "tnStartPosition";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxEndPosition
            // 
            resources.ApplyResources(this.textBoxEndPosition, "textBoxEndPosition");
            this.textBoxEndPosition.Name = "textBoxEndPosition";
            // 
            // StringFieldPropertiesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxEndPosition);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dgvReplacementList);
            this.Controls.Add(this.checkBoxAlignRight);
            this.Controls.Add(this.tnStartPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelStartPosition);
            this.Name = "StringFieldPropertiesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReplacementList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStartPosition;
        private OpenCBS.GUI.UserControl.TextNumericUserControl tnStartPosition;
        private System.Windows.Forms.CheckBox checkBoxAlignRight;
        private System.Windows.Forms.DataGridView dgvReplacementList;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxEndPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOriginalValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplacementValue;
    }
}