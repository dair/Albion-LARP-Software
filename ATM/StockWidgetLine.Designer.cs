namespace ATM
{
    partial class StockWidgetLine
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
            this.ticker = new System.Windows.Forms.Label();
            this.quote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ticker
            // 
            this.ticker.BackColor = System.Drawing.Color.Transparent;
            this.ticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ticker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ticker.Location = new System.Drawing.Point(3, 0);
            this.ticker.Name = "ticker";
            this.ticker.Size = new System.Drawing.Size(71, 23);
            this.ticker.TabIndex = 0;
            this.ticker.Text = "GOOG";
            // 
            // quote
            // 
            this.quote.BackColor = System.Drawing.Color.Transparent;
            this.quote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.quote.Location = new System.Drawing.Point(80, 0);
            this.quote.Name = "quote";
            this.quote.Size = new System.Drawing.Size(119, 23);
            this.quote.TabIndex = 1;
            this.quote.Text = "GOOG";
            // 
            // StockWidgetLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.quote);
            this.Controls.Add(this.ticker);
            this.Name = "StockWidgetLine";
            this.Size = new System.Drawing.Size(212, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ticker;
        private System.Windows.Forms.Label quote;
    }
}
