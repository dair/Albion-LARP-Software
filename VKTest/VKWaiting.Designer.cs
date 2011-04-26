namespace VKTest
{
    partial class VKWaiting
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
            this.questionNumLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.questionText = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.answerText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // questionNumLabel
            // 
            this.questionNumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.questionNumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionNumLabel.ForeColor = System.Drawing.Color.White;
            this.questionNumLabel.Location = new System.Drawing.Point(255, 13);
            this.questionNumLabel.Name = "questionNumLabel";
            this.questionNumLabel.Size = new System.Drawing.Size(162, 25);
            this.questionNumLabel.TabIndex = 18;
            this.questionNumLabel.Text = "Вопрос №";
            this.questionNumLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(232, 13);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(565, 25);
            this.nameLabel.TabIndex = 17;
            this.nameLabel.Text = "Имя несчастного";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // questionText
            // 
            this.questionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionText.ForeColor = System.Drawing.Color.White;
            this.questionText.Location = new System.Drawing.Point(3, 58);
            this.questionText.Name = "questionText";
            this.questionText.Size = new System.Drawing.Size(793, 152);
            this.questionText.TabIndex = 19;
            this.questionText.Text = "Текст вопроса";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(-1, 369);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(793, 28);
            this.statusLabel.TabIndex = 20;
            this.statusLabel.Text = "Текст вопроса";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // answerText
            // 
            this.answerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.answerText.ForeColor = System.Drawing.Color.White;
            this.answerText.Location = new System.Drawing.Point(4, 210);
            this.answerText.Name = "answerText";
            this.answerText.Size = new System.Drawing.Size(793, 159);
            this.answerText.TabIndex = 21;
            this.answerText.Text = "Текст вопроса";
            // 
            // VKWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.answerText);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.questionText);
            this.Controls.Add(this.questionNumLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "VKWaiting";
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.nameLabel, 0);
            this.Controls.SetChildIndex(this.questionNumLabel, 0);
            this.Controls.SetChildIndex(this.questionText, 0);
            this.Controls.SetChildIndex(this.statusLabel, 0);
            this.Controls.SetChildIndex(this.answerText, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionNumLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label questionText;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label answerText;
    }
}
