namespace VKTest
{
    partial class VKObject
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
            this.label1 = new System.Windows.Forms.Label();
            this.instantValueBar = new VKTest.VerticalProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // escLabel
            // 
            this.escLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.escLabel.Location = new System.Drawing.Point(0, 372);
            this.escLabel.Size = new System.Drawing.Size(800, 28);
            this.escLabel.Visible = false;
            // 
            // pictureLogo
            // 
            this.pictureLogo.Location = new System.Drawing.Point(18, 334);
            this.pictureLogo.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 25);
            this.label1.TabIndex = 13;
            this.label1.Text = "Тест Войта-Кампфа";
            // 
            // instantValueBar
            // 
            this.instantValueBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.instantValueBar.ForeColor = System.Drawing.Color.Red;
            this.instantValueBar.Location = new System.Drawing.Point(768, 3);
            this.instantValueBar.Name = "instantValueBar";
            this.instantValueBar.Shaking = false;
            this.instantValueBar.Size = new System.Drawing.Size(32, 394);
            this.instantValueBar.TabIndex = 14;
            this.instantValueBar.Value = 50;
            // 
            // VKObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.instantValueBar);
            this.Controls.Add(this.label1);
            this.Name = "VKObject";
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.instantValueBar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        protected VerticalProgressBar instantValueBar;
    }
}
