namespace TimeMachine
{
    partial class RecoveryConsole
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
            this.shellControl1 = new UILibrary.ShellControl();
            this.SuspendLayout();
            // 
            // shellControl1
            // 
            this.shellControl1.BackColor = System.Drawing.Color.Black;
            this.shellControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellControl1.Location = new System.Drawing.Point(0, 0);
            this.shellControl1.Name = "shellControl1";
            this.shellControl1.Prompt = "# ";
            this.shellControl1.ShellTextBackColor = System.Drawing.Color.Black;
            this.shellControl1.ShellTextFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shellControl1.ShellTextForeColor = System.Drawing.Color.LawnGreen;
            this.shellControl1.Size = new System.Drawing.Size(539, 424);
            this.shellControl1.TabIndex = 0;
            this.shellControl1.CommandEntered += new UILibrary.EventCommandEntered(this.shellControl1_CommandEntered);
            // 
            // RecoveryConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.shellControl1);
            this.Name = "RecoveryConsole";
            this.Size = new System.Drawing.Size(539, 424);
            this.ResumeLayout(false);

        }

        #endregion

        private UILibrary.ShellControl shellControl1;
    }
}
