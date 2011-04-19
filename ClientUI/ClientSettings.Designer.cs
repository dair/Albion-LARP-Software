namespace ClientUI
{
    partial class ClientSettings
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dbSettingsView = new UI.DBSettingsView(getDatabase());
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.bsSettingsView = new UI.BSSettingsView();
            this.testBarCodeButton = new System.Windows.Forms.Button();
            this.testTextBox = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(712, 224);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dbSettingsView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(704, 198);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Database";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dbSettingsView
            // 
            this.dbSettingsView.Location = new System.Drawing.Point(9, 7);
            this.dbSettingsView.Name = "dbSettingsView";
            this.dbSettingsView.Size = new System.Drawing.Size(687, 182);
            this.dbSettingsView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.testTextBox);
            this.tabPage2.Controls.Add(this.testBarCodeButton);
            this.tabPage2.Controls.Add(this.bsSettingsView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(704, 198);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "BarCode";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(625, 235);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(544, 235);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // bsSettingsView
            // 
            this.bsSettingsView.Location = new System.Drawing.Point(9, 7);
            this.bsSettingsView.MaximumSize = new System.Drawing.Size(0, 150);
            this.bsSettingsView.MinimumSize = new System.Drawing.Size(150, 150);
            this.bsSettingsView.Name = "bsSettingsView";
            this.bsSettingsView.Size = new System.Drawing.Size(692, 150);
            this.bsSettingsView.TabIndex = 0;
            // 
            // testBarCodeButton
            // 
            this.testBarCodeButton.Location = new System.Drawing.Point(9, 161);
            this.testBarCodeButton.Name = "testBarCodeButton";
            this.testBarCodeButton.Size = new System.Drawing.Size(75, 23);
            this.testBarCodeButton.TabIndex = 1;
            this.testBarCodeButton.Text = "Проверить";
            this.testBarCodeButton.UseVisualStyleBackColor = true;
            // 
            // testTextBox
            // 
            this.testTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.testTextBox.Location = new System.Drawing.Point(90, 163);
            this.testTextBox.Name = "testTextBox";
            this.testTextBox.ReadOnly = true;
            this.testTextBox.Size = new System.Drawing.Size(605, 20);
            this.testTextBox.TabIndex = 2;
            // 
            // ClientSettings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(712, 270);
            this.ControlBox = false;
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl);
            this.Name = "ClientSettings";
            this.Text = "Настройки";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UI.DBSettingsView dbSettingsView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private UI.BSSettingsView bsSettingsView;
        private System.Windows.Forms.TextBox testTextBox;
        private System.Windows.Forms.Button testBarCodeButton;

    }
}