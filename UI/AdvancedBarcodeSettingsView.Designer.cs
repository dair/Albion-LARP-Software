namespace UI
{
    partial class AdvancedBarcodeSettingsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonHid = new System.Windows.Forms.RadioButton();
            this.radioButtonCom = new System.Windows.Forms.RadioButton();
            this.bsSettingsView = new UI.BSSettingsView();
            this.SuspendLayout();
            // 
            // radioButtonHid
            // 
            this.radioButtonHid.AutoSize = true;
            this.radioButtonHid.Location = new System.Drawing.Point(3, 3);
            this.radioButtonHid.Name = "radioButtonHid";
            this.radioButtonHid.Size = new System.Drawing.Size(44, 17);
            this.radioButtonHid.TabIndex = 0;
            this.radioButtonHid.TabStop = true;
            this.radioButtonHid.Text = "HID";
            this.radioButtonHid.UseVisualStyleBackColor = true;
            this.radioButtonHid.CheckedChanged += new System.EventHandler(this.radioButtonSwitch);
            // 
            // radioButtonCom
            // 
            this.radioButtonCom.AutoSize = true;
            this.radioButtonCom.Location = new System.Drawing.Point(3, 26);
            this.radioButtonCom.Name = "radioButtonCom";
            this.radioButtonCom.Size = new System.Drawing.Size(74, 17);
            this.radioButtonCom.TabIndex = 1;
            this.radioButtonCom.TabStop = true;
            this.radioButtonCom.Text = "USB COM";
            this.radioButtonCom.UseVisualStyleBackColor = true;
            this.radioButtonCom.CheckedChanged += new System.EventHandler(this.radioButtonSwitch);
            // 
            // bsSettingsView
            // 
            this.bsSettingsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bsSettingsView.Location = new System.Drawing.Point(78, 3);
            this.bsSettingsView.MinimumSize = new System.Drawing.Size(150, 150);
            this.bsSettingsView.Name = "bsSettingsView";
            this.bsSettingsView.Size = new System.Drawing.Size(152, 150);
            this.bsSettingsView.TabIndex = 2;
            // 
            // AdvancedBarcodeSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bsSettingsView);
            this.Controls.Add(this.radioButtonCom);
            this.Controls.Add(this.radioButtonHid);
            this.Name = "AdvancedBarcodeSettingsView";
            this.Size = new System.Drawing.Size(236, 154);
            this.Load += new System.EventHandler(this.AdvancedBarcodeSettingsView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonHid;
        private System.Windows.Forms.RadioButton radioButtonCom;
        private BSSettingsView bsSettingsView;
    }
}
