namespace ATM
{
    partial class CycleInfoBar
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
            this.serverTimeLabel = new System.Windows.Forms.Label();
            this.untilBorder1Label = new System.Windows.Forms.Label();
            this.untilBorder2Label = new System.Windows.Forms.Label();
            this.untilFinishLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serverTimeLabel
            // 
            this.serverTimeLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serverTimeLabel.ForeColor = System.Drawing.Color.White;
            this.serverTimeLabel.Location = new System.Drawing.Point(3, 0);
            this.serverTimeLabel.Name = "serverTimeLabel";
            this.serverTimeLabel.Size = new System.Drawing.Size(145, 23);
            this.serverTimeLabel.TabIndex = 0;
            this.serverTimeLabel.Text = "label1";
            this.serverTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // untilBorder1Label
            // 
            this.untilBorder1Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.untilBorder1Label.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.untilBorder1Label.ForeColor = System.Drawing.Color.White;
            this.untilBorder1Label.Location = new System.Drawing.Point(154, 0);
            this.untilBorder1Label.Name = "untilBorder1Label";
            this.untilBorder1Label.Size = new System.Drawing.Size(145, 23);
            this.untilBorder1Label.TabIndex = 1;
            this.untilBorder1Label.Text = "label1";
            this.untilBorder1Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // untilBorder2Label
            // 
            this.untilBorder2Label.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.untilBorder2Label.ForeColor = System.Drawing.Color.White;
            this.untilBorder2Label.Location = new System.Drawing.Point(342, 0);
            this.untilBorder2Label.Name = "untilBorder2Label";
            this.untilBorder2Label.Size = new System.Drawing.Size(145, 23);
            this.untilBorder2Label.TabIndex = 2;
            this.untilBorder2Label.Text = "label1";
            this.untilBorder2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // untilFinishLabel
            // 
            this.untilFinishLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.untilFinishLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.untilFinishLabel.ForeColor = System.Drawing.Color.White;
            this.untilFinishLabel.Location = new System.Drawing.Point(540, 0);
            this.untilFinishLabel.Name = "untilFinishLabel";
            this.untilFinishLabel.Size = new System.Drawing.Size(145, 23);
            this.untilFinishLabel.TabIndex = 3;
            this.untilFinishLabel.Text = "label1";
            this.untilFinishLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CycleInfoBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.untilFinishLabel);
            this.Controls.Add(this.untilBorder2Label);
            this.Controls.Add(this.untilBorder1Label);
            this.Controls.Add(this.serverTimeLabel);
            this.Name = "CycleInfoBar";
            this.Size = new System.Drawing.Size(688, 28);
            this.Load += new System.EventHandler(this.CycleInfoBar_Load);
            this.SizeChanged += new System.EventHandler(this.CycleInfoBar_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label serverTimeLabel;
        private System.Windows.Forms.Label untilBorder1Label;
        private System.Windows.Forms.Label untilBorder2Label;
        private System.Windows.Forms.Label untilFinishLabel;
    }
}
