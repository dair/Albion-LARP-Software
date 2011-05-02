namespace ATM
{
    partial class StockDirectPrice
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
            this.companyLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // companyLabel
            // 
            this.companyLabel.BackColor = System.Drawing.Color.Transparent;
            this.companyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.companyLabel.ForeColor = System.Drawing.Color.White;
            this.companyLabel.Location = new System.Drawing.Point(363, 14);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(418, 67);
            this.companyLabel.TabIndex = 14;
            this.companyLabel.Text = "Название компании";
            this.companyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(363, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 29);
            this.label1.TabIndex = 15;
            this.label1.Text = "Введите стоимость акций:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // priceBox
            // 
            this.priceBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.priceBox.BackColor = System.Drawing.Color.Black;
            this.priceBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.priceBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.priceBox.ForeColor = System.Drawing.Color.White;
            this.priceBox.Location = new System.Drawing.Point(423, 180);
            this.priceBox.MaxLength = 0;
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(306, 62);
            this.priceBox.TabIndex = 16;
            this.priceBox.Text = "150000";
            this.priceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priceBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.priceBox_KeyDown);
            // 
            // infoLabel
            // 
            this.infoLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLabel.ForeColor = System.Drawing.Color.White;
            this.infoLabel.Location = new System.Drawing.Point(363, 245);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(418, 67);
            this.infoLabel.TabIndex = 17;
            this.infoLabel.Text = "Строка состояния";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // qtyLabel
            // 
            this.qtyLabel.BackColor = System.Drawing.Color.Transparent;
            this.qtyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.qtyLabel.ForeColor = System.Drawing.Color.White;
            this.qtyLabel.Location = new System.Drawing.Point(363, 81);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(418, 67);
            this.qtyLabel.TabIndex = 18;
            this.qtyLabel.Text = "Кол-во акций";
            this.qtyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(395, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 62);
            this.label2.TabIndex = 19;
            this.label2.Text = "$";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StockDirectPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.companyLabel);
            this.Name = "StockDirectPrice";
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.companyLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.priceBox, 0);
            this.Controls.SetChildIndex(this.infoLabel, 0);
            this.Controls.SetChildIndex(this.qtyLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Label label2;
    }
}
