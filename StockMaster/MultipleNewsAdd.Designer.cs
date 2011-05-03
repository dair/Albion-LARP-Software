namespace StockMaster
{
    partial class MultipleNewsAdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.okBox = new System.Windows.Forms.Button();
            this.cancelBox = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Новости будут добавлены по одной строке на новость, текущей датой.";
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(15, 55);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(470, 177);
            this.textBox.TabIndex = 1;
            this.textBox.WordWrap = false;
            // 
            // okBox
            // 
            this.okBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBox.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBox.Location = new System.Drawing.Point(329, 238);
            this.okBox.Name = "okBox";
            this.okBox.Size = new System.Drawing.Size(75, 23);
            this.okBox.TabIndex = 2;
            this.okBox.Text = "OK";
            this.okBox.UseVisualStyleBackColor = true;
            this.okBox.Click += new System.EventHandler(this.okBox_Click);
            // 
            // cancelBox
            // 
            this.cancelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBox.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBox.Location = new System.Drawing.Point(410, 238);
            this.cancelBox.Name = "cancelBox";
            this.cancelBox.Size = new System.Drawing.Size(75, 23);
            this.cancelBox.TabIndex = 3;
            this.cancelBox.Text = "Отмена";
            this.cancelBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Перевод строки - Ctrl-Enter";
            // 
            // MultipleNewsAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 273);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelBox);
            this.Controls.Add(this.okBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label1);
            this.Name = "MultipleNewsAdd";
            this.Text = "Добавление кучи новостей";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button okBox;
        private System.Windows.Forms.Button cancelBox;
        private System.Windows.Forms.Label label2;
    }
}