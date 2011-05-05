namespace ATM
{
    partial class StockAddQty
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
            this.qtyBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // qtyBox
            // 
            this.qtyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.qtyBox.BackColor = System.Drawing.Color.Black;
            this.qtyBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.qtyBox.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.qtyBox.ForeColor = System.Drawing.Color.White;
            this.qtyBox.Location = new System.Drawing.Point(363, 145);
            this.qtyBox.MaxLength = 10;
            this.qtyBox.Name = "qtyBox";
            this.qtyBox.Size = new System.Drawing.Size(423, 49);
            this.qtyBox.TabIndex = 12;
            this.qtyBox.Text = "WWWWW";
            this.qtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.qtyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tickerBox_KeyDown);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(388, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(367, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "Количество акций:";
            // 
            // infoLabel
            // 
            this.infoLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLabel.ForeColor = System.Drawing.Color.White;
            this.infoLabel.Location = new System.Drawing.Point(363, 226);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(392, 131);
            this.infoLabel.TabIndex = 16;
            this.infoLabel.Text = "Введите тикер компании:";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StockAddQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.qtyBox);
            this.Name = "StockAddQty";
            this.Controls.SetChildIndex(this.qtyBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.infoLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox qtyBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label infoLabel;
    }
}
