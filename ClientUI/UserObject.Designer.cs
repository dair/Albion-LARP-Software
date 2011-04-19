namespace ClientUI
{
    partial class UserObject
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
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.escLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureLogo
            // 
            /*
            this.pictureLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureLogo.Image = global::B5App.Properties.Resources.bab_logo;
            this.pictureLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(302, 299);
            this.pictureLogo.TabIndex = 9;
            this.pictureLogo.TabStop = false;
            this.pictureLogo.WaitOnLoad = true;
             */
            // 
            // escLabel
            // 
            this.escLabel.BackColor = System.Drawing.Color.Black;
            this.escLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.escLabel.ForeColor = System.Drawing.Color.White;
            this.escLabel.Location = new System.Drawing.Point(0, 377);
            this.escLabel.Name = "escLabel";
            this.escLabel.Size = new System.Drawing.Size(800, 23);
            this.escLabel.TabIndex = 10;
            this.escLabel.Text = "Вы можете прервать выполнение текущей операции, нажав клавишу ESC";
            this.escLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // UserObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.escLabel);
            this.Controls.Add(this.pictureLogo);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.Name = "UserObject";
            this.Size = new System.Drawing.Size(800, 400);
            this.Load += new System.EventHandler(this.UserObject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label escLabel;
        protected System.Windows.Forms.PictureBox pictureLogo;
    }
}
