namespace ATM
{
    partial class StockDirectQty
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
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.infoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // companyLabel
            // 
            this.companyLabel.BackColor = System.Drawing.Color.Transparent;
            this.companyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.companyLabel.ForeColor = System.Drawing.Color.White;
            this.companyLabel.Location = new System.Drawing.Point(363, 55);
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
            this.label1.Text = "Введите количество акций:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // priceBox
            // 
            this.qtyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.qtyBox.BackColor = System.Drawing.Color.Black;
            this.qtyBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.qtyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.qtyBox.ForeColor = System.Drawing.Color.White;
            this.qtyBox.Location = new System.Drawing.Point(423, 180);
            this.qtyBox.MaxLength = 7;
            this.qtyBox.Name = "priceBox";
            this.qtyBox.Size = new System.Drawing.Size(306, 62);
            this.qtyBox.TabIndex = 16;
            this.qtyBox.Text = "150000";
            this.qtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.qtyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.qtyBox_KeyDown);
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
            // StockDirectQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.qtyBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.companyLabel);
            this.Name = "StockDirectQty";
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.companyLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.qtyBox, 0);
            this.Controls.SetChildIndex(this.infoLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label infoLabel;
    }
}
