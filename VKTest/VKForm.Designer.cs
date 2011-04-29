namespace VKTest
{
    partial class VKForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.verticalProgressBar = new VKTest.VerticalProgressBar();
            this.score = new System.Windows.Forms.Label();
            this.middle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // verticalProgressBar
            // 
            this.verticalProgressBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.verticalProgressBar.ForeColor = System.Drawing.Color.DarkRed;
            this.verticalProgressBar.Location = new System.Drawing.Point(755, 0);
            this.verticalProgressBar.Name = "verticalProgressBar";
            this.verticalProgressBar.Shaking = false;
            this.verticalProgressBar.Size = new System.Drawing.Size(45, 600);
            this.verticalProgressBar.TabIndex = 0;
            this.verticalProgressBar.Value = 50;
            // 
            // score
            // 
            this.score.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.score.AutoSize = true;
            this.score.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.score.Location = new System.Drawing.Point(714, 9);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(35, 13);
            this.score.TabIndex = 1;
            this.score.Text = "label1";
            // 
            // middle
            // 
            this.middle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.middle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.middle.Location = new System.Drawing.Point(580, 9);
            this.middle.Name = "middle";
            this.middle.Size = new System.Drawing.Size(81, 13);
            this.middle.TabIndex = 2;
            this.middle.Text = "label1";
            // 
            // VKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = true;
            this.Controls.Add(this.middle);
            this.Controls.Add(this.score);
            this.Controls.Add(this.verticalProgressBar);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "VKForm";
            this.Text = "VKForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VerticalProgressBar verticalProgressBar;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Label middle;
    }
}