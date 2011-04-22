namespace ATM
{
    partial class ATMVerify
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
            this.receiverLabel = new System.Windows.Forms.Label();
            this.senderLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.commitLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // receiverLabel
            // 
            this.receiverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.receiverLabel.ForeColor = System.Drawing.Color.White;
            this.receiverLabel.Location = new System.Drawing.Point(363, 109);
            this.receiverLabel.Name = "receiverLabel";
            this.receiverLabel.Size = new System.Drawing.Size(434, 86);
            this.receiverLabel.TabIndex = 14;
            this.receiverLabel.Text = "Получатель: NAMEHERE";
            this.receiverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // senderLabel
            // 
            this.senderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.senderLabel.ForeColor = System.Drawing.Color.White;
            this.senderLabel.Location = new System.Drawing.Point(363, 0);
            this.senderLabel.Name = "senderLabel";
            this.senderLabel.Size = new System.Drawing.Size(434, 109);
            this.senderLabel.TabIndex = 13;
            this.senderLabel.Text = "Плательщик: NAMEHERE";
            this.senderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // amountLabel
            // 
            this.amountLabel.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.amountLabel.ForeColor = System.Drawing.Color.White;
            this.amountLabel.Location = new System.Drawing.Point(544, 218);
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
            this.commitLabel.Location = new System.Drawing.Point(363, 284);
            this.commitLabel.Name = "commitLabel";
            this.commitLabel.Size = new System.Drawing.Size(434, 73);
            this.commitLabel.TabIndex = 16;
            this.commitLabel.Text = "Нажмите ENTER для подтверждения";
            this.commitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(363, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 46);
            this.label1.TabIndex = 17;
            this.label1.Text = "Сумма:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ATMVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commitLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.receiverLabel);
            this.Controls.Add(this.senderLabel);
            this.Name = "ATMVerify";
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.senderLabel, 0);
            this.Controls.SetChildIndex(this.receiverLabel, 0);
            this.Controls.SetChildIndex(this.amountLabel, 0);
            this.Controls.SetChildIndex(this.commitLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label receiverLabel;
        private System.Windows.Forms.Label senderLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.Label commitLabel;
        private System.Windows.Forms.Label label1;
    }
}
