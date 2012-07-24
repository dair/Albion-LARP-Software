namespace TimeMachine
{
    partial class EnergyRequest
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
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.amountText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.commit = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.errorAmount = new System.Windows.Forms.Panel();
            this.periodError = new System.Windows.Forms.Panel();
            this.priceLabel = new System.Windows.Forms.Label();
            this.to = new TimeMachine.TimeForm();
            this.from = new TimeMachine.TimeForm();
            this.payButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(266, 162);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 30);
            this.label17.TabIndex = 17;
            this.label17.Text = "по";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(3, 161);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 30);
            this.label16.TabIndex = 16;
            this.label16.Text = "С";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // amountText
            // 
            this.amountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.amountText.Location = new System.Drawing.Point(226, 66);
            this.amountText.Name = "amountText";
            this.amountText.Size = new System.Drawing.Size(310, 29);
            this.amountText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Период:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество энергии:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(543, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Запрос на предоставление электроэнергии в рамках научного проекта $1\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // commit
            // 
            this.commit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commit.Location = new System.Drawing.Point(276, 255);
            this.commit.Name = "commit";
            this.commit.Size = new System.Drawing.Size(127, 49);
            this.commit.TabIndex = 22;
            this.commit.Text = "Запросить";
            this.commit.UseVisualStyleBackColor = true;
            this.commit.Click += new System.EventHandler(this.commit_Click);
            // 
            // cancel
            // 
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel.Location = new System.Drawing.Point(143, 255);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(127, 49);
            this.cancel.TabIndex = 21;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // errorAmount
            // 
            this.errorAmount.BackColor = System.Drawing.Color.Red;
            this.errorAmount.Location = new System.Drawing.Point(226, 101);
            this.errorAmount.Name = "errorAmount";
            this.errorAmount.Size = new System.Drawing.Size(310, 5);
            this.errorAmount.TabIndex = 23;
            // 
            // periodError
            // 
            this.periodError.BackColor = System.Drawing.Color.Red;
            this.periodError.Location = new System.Drawing.Point(38, 211);
            this.periodError.Name = "periodError";
            this.periodError.Size = new System.Drawing.Size(489, 5);
            this.periodError.TabIndex = 24;
            // 
            // priceLabel
            // 
            this.priceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.priceLabel.ForeColor = System.Drawing.Color.Red;
            this.priceLabel.Location = new System.Drawing.Point(3, 219);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(524, 30);
            this.priceLabel.TabIndex = 25;
            this.priceLabel.Text = "Стоимость";
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // to
            // 
            this.to.AutoSize = true;
            this.to.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.to.Location = new System.Drawing.Point(295, 134);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(232, 94);
            this.to.TabIndex = 19;
            // 
            // from
            // 
            this.from.AutoSize = true;
            this.from.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.from.Location = new System.Drawing.Point(38, 134);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(232, 94);
            this.from.TabIndex = 18;
            // 
            // payButton
            // 
            this.payButton.Enabled = false;
            this.payButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.payButton.Location = new System.Drawing.Point(409, 255);
            this.payButton.Name = "payButton";
            this.payButton.Size = new System.Drawing.Size(127, 49);
            this.payButton.TabIndex = 26;
            this.payButton.Text = "Оплатить";
            this.payButton.UseVisualStyleBackColor = true;
            this.payButton.Click += new System.EventHandler(this.payButton_Click);
            // 
            // EnergyRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.payButton);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.periodError);
            this.Controls.Add(this.errorAmount);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.commit);
            this.Controls.Add(this.to);
            this.Controls.Add(this.from);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.amountText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EnergyRequest";
            this.Size = new System.Drawing.Size(543, 309);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox amountText;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private TimeForm from;
        private TimeForm to;
        private System.Windows.Forms.Button commit;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Panel errorAmount;
        private System.Windows.Forms.Panel periodError;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Button payButton;
    }
}
