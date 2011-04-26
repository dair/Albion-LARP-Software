namespace StockMaster
{
    partial class EditCycleForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.border1Time = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.border2Time = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.syncButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(426, 330);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(345, 330);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // startTime
            // 
            this.startTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTime.Location = new System.Drawing.Point(157, 12);
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(210, 20);
            this.startTime.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Начало цикла";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Окончание снятия заявок";
            // 
            // border1Time
            // 
            this.border1Time.CustomFormat = "dd.MM.yyyy HH:mm";
            this.border1Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.border1Time.Location = new System.Drawing.Point(157, 38);
            this.border1Time.Name = "border1Time";
            this.border1Time.Size = new System.Drawing.Size(344, 20);
            this.border1Time.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Окончание приёма заявок";
            // 
            // border2Time
            // 
            this.border2Time.CustomFormat = "dd.MM.yyyy HH:mm";
            this.border2Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.border2Time.Location = new System.Drawing.Point(157, 64);
            this.border2Time.Name = "border2Time";
            this.border2Time.Size = new System.Drawing.Size(344, 20);
            this.border2Time.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Конец цикла";
            // 
            // endTime
            // 
            this.endTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.endTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTime.Location = new System.Drawing.Point(157, 90);
            this.endTime.Name = "endTime";
            this.endTime.Size = new System.Drawing.Size(344, 20);
            this.endTime.TabIndex = 8;
            // 
            // syncButton
            // 
            this.syncButton.Location = new System.Drawing.Point(373, 9);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(128, 23);
            this.syncButton.TabIndex = 10;
            this.syncButton.Text = "Проставить время";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // EditCycleForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(513, 365);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.border2Time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.border1Time);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startTime);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "EditCycleForm";
            this.Text = "EditCycleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker border1Time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker border2Time;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker endTime;
        private System.Windows.Forms.Button syncButton;
    }
}