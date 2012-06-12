namespace CashDesk
{
    partial class CashDeskVerify
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
            this.senderLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.commitLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pinBox = new System.Windows.Forms.TextBox();
            this.remainLabel = new System.Windows.Forms.Label();
            this.wrongLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // senderLabel
            // 
            this.senderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.senderLabel.ForeColor = System.Drawing.Color.White;
            this.senderLabel.Location = new System.Drawing.Point(6, 0);
            this.senderLabel.Name = "senderLabel";
            this.senderLabel.Size = new System.Drawing.Size(794, 109);
            this.senderLabel.TabIndex = 13;
            this.senderLabel.Text = "Плательщик: NAMEHERE";
            this.senderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // amountLabel
            // 
            this.amountLabel.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.amountLabel.ForeColor = System.Drawing.Color.White;
            this.amountLabel.Location = new System.Drawing.Point(363, 109);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(253, 46);
            this.amountLabel.TabIndex = 15;
            this.amountLabel.Text = "$AMOUNTHERE";
            this.amountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // commitLabel
            // 
            this.commitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commitLabel.ForeColor = System.Drawing.Color.White;
            this.commitLabel.Location = new System.Drawing.Point(194, 243);
            this.commitLabel.Name = "commitLabel";
            this.commitLabel.Size = new System.Drawing.Size(422, 73);
            this.commitLabel.TabIndex = 16;
            this.commitLabel.Text = "Введите PIN и нажмите ENTER для подтверждения";
            this.commitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(182, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 46);
            this.label1.TabIndex = 17;
            this.label1.Text = "Сумма:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pinBox
            // 
            this.pinBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pinBox.BackColor = System.Drawing.Color.Black;
            this.pinBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pinBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pinBox.ForeColor = System.Drawing.Color.White;
            this.pinBox.Location = new System.Drawing.Point(315, 158);
            this.pinBox.MaxLength = 5;
            this.pinBox.Name = "pinBox";
            this.pinBox.Size = new System.Drawing.Size(173, 62);
            this.pinBox.TabIndex = 18;
            this.pinBox.Text = "88888";
            this.pinBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pinBox.UseSystemPasswordChar = true;
            this.pinBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pinBox_KeyDown);
            // 
            // remainLabel
            // 
            this.remainLabel.BackColor = System.Drawing.Color.Transparent;
            this.remainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.remainLabel.ForeColor = System.Drawing.Color.Red;
            this.remainLabel.Location = new System.Drawing.Point(189, 233);
            this.remainLabel.Name = "remainLabel";
            this.remainLabel.Size = new System.Drawing.Size(422, 63);
            this.remainLabel.TabIndex = 20;
            this.remainLabel.Text = "Осталось 3 попытки";
            this.remainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wrongLabel
            // 
            this.wrongLabel.BackColor = System.Drawing.Color.Transparent;
            this.wrongLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wrongLabel.ForeColor = System.Drawing.Color.Red;
            this.wrongLabel.Location = new System.Drawing.Point(189, 105);
            this.wrongLabel.Name = "wrongLabel";
            this.wrongLabel.Size = new System.Drawing.Size(422, 116);
            this.wrongLabel.TabIndex = 19;
            this.wrongLabel.Text = "PIN-код введён неверно!";
            this.wrongLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CashDeskVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.pinBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commitLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.senderLabel);
            this.Controls.Add(this.remainLabel);
            this.Controls.Add(this.wrongLabel);
            this.Name = "CashDeskVerify";
            this.Controls.SetChildIndex(this.wrongLabel, 0);
            this.Controls.SetChildIndex(this.remainLabel, 0);
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.senderLabel, 0);
            this.Controls.SetChildIndex(this.amountLabel, 0);
            this.Controls.SetChildIndex(this.commitLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pinBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label senderLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.Label commitLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pinBox;
        private System.Windows.Forms.Label remainLabel;
        private System.Windows.Forms.Label wrongLabel;
    }
}
