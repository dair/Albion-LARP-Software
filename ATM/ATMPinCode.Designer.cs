namespace ATM
{
    partial class ATMPinCode
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
            this.greetLabel = new System.Windows.Forms.Label();
            this.pinBox = new System.Windows.Forms.TextBox();
            this.wrongLabel = new System.Windows.Forms.Label();
            this.remainLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // greetLabel
            // 
            this.greetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.greetLabel.ForeColor = System.Drawing.Color.White;
            this.greetLabel.Location = new System.Drawing.Point(363, 9);
            this.greetLabel.Name = "greetLabel";
            this.greetLabel.Size = new System.Drawing.Size(434, 143);
            this.greetLabel.TabIndex = 10;
            this.greetLabel.Text = "Здравствуйте, NAMEHERE!\r\n\r\nВведите PIN-код и нажмите Enter:";
            this.greetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pinBox
            // 
            this.pinBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pinBox.BackColor = System.Drawing.Color.Black;
            this.pinBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pinBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pinBox.ForeColor = System.Drawing.Color.White;
            this.pinBox.Location = new System.Drawing.Point(494, 194);
            this.pinBox.MaxLength = 5;
            this.pinBox.Name = "pinBox";
            this.pinBox.Size = new System.Drawing.Size(173, 62);
            this.pinBox.TabIndex = 11;
            this.pinBox.Text = "88888";
            this.pinBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pinBox.UseSystemPasswordChar = true;
            // 
            // wrongLabel
            // 
            this.wrongLabel.BackColor = System.Drawing.Color.Transparent;
            this.wrongLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wrongLabel.ForeColor = System.Drawing.Color.Red;
            this.wrongLabel.Location = new System.Drawing.Point(363, 166);
            this.wrongLabel.Name = "wrongLabel";
            this.wrongLabel.Size = new System.Drawing.Size(422, 116);
            this.wrongLabel.TabIndex = 12;
            this.wrongLabel.Text = "PIN-код введён неверно!";
            this.wrongLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remainLabel
            // 
            this.remainLabel.BackColor = System.Drawing.Color.Transparent;
            this.remainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.remainLabel.ForeColor = System.Drawing.Color.Red;
            this.remainLabel.Location = new System.Drawing.Point(363, 294);
            this.remainLabel.Name = "remainLabel";
            this.remainLabel.Size = new System.Drawing.Size(422, 63);
            this.remainLabel.TabIndex = 13;
            this.remainLabel.Text = "Осталось 3 попытки";
            this.remainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ATMPinCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.remainLabel);
            this.Controls.Add(this.greetLabel);
            this.Controls.Add(this.pinBox);
            this.Controls.Add(this.wrongLabel);
            this.Name = "ATMPinCode";
            this.Controls.SetChildIndex(this.wrongLabel, 0);
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.pinBox, 0);
            this.Controls.SetChildIndex(this.greetLabel, 0);
            this.Controls.SetChildIndex(this.remainLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label greetLabel;
        private System.Windows.Forms.TextBox pinBox;
        private System.Windows.Forms.Label wrongLabel;
        private System.Windows.Forms.Label remainLabel;
    }
}
