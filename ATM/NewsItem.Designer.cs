namespace ATM
{
    partial class NewsItem
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
            this.headerLabel = new GrowLabel();
            this.textLabel = new GrowLabel();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(447, 20);
            this.headerLabel.TabIndex = 2;
            this.headerLabel.Text = "growLabel1";
            // 
            // textLabel
            // 
            this.textLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textLabel.ForeColor = System.Drawing.Color.White;
            this.textLabel.Location = new System.Drawing.Point(0, 23);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(447, 16);
            this.textLabel.TabIndex = 3;
            this.textLabel.Text = "growLabel1";
            // 
            // NewsItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.headerLabel);
            this.Name = "NewsItem";
            this.Size = new System.Drawing.Size(447, 85);
            this.ResumeLayout(false);

        }

        #endregion

        private GrowLabel headerLabel;
        private GrowLabel textLabel;

    }
}
