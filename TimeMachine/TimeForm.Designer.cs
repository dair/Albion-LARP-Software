namespace TimeMachine
{
    partial class TimeForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.minute = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.hour = new System.Windows.Forms.TextBox();
            this.day = new System.Windows.Forms.TextBox();
            this.month = new System.Windows.Forms.TextBox();
            this.year = new System.Windows.Forms.TextBox();
            this.yearError = new System.Windows.Forms.Panel();
            this.monthError = new System.Windows.Forms.Panel();
            this.dayError = new System.Windows.Forms.Panel();
            this.hourError = new System.Windows.Forms.Panel();
            this.minuteError = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Location = new System.Drawing.Point(91, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 0);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Минуты";
            // 
            // minute
            // 
            this.minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minute.Location = new System.Drawing.Point(191, 25);
            this.minute.MaxLength = 2;
            this.minute.Name = "minute";
            this.minute.Size = new System.Drawing.Size(30, 29);
            this.minute.TabIndex = 19;
            this.minute.Text = "29";
            this.minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Часы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "День";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(62, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Месяц";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Год";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(177, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 30);
            this.label12.TabIndex = 20;
            this.label12.Text = ":";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hour
            // 
            this.hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour.Location = new System.Drawing.Point(148, 25);
            this.hour.MaxLength = 2;
            this.hour.Name = "hour";
            this.hour.Size = new System.Drawing.Size(30, 29);
            this.hour.TabIndex = 18;
            this.hour.Text = "29";
            this.hour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // day
            // 
            this.day.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.day.Location = new System.Drawing.Point(101, 25);
            this.day.MaxLength = 2;
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(30, 29);
            this.day.TabIndex = 17;
            this.day.Text = "29";
            this.day.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // month
            // 
            this.month.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.month.Location = new System.Drawing.Point(65, 25);
            this.month.MaxLength = 2;
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(30, 29);
            this.month.TabIndex = 16;
            this.month.Text = "12";
            this.month.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // year
            // 
            this.year.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.year.Location = new System.Drawing.Point(7, 25);
            this.year.MaxLength = 4;
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(52, 29);
            this.year.TabIndex = 15;
            this.year.Text = "2567";
            this.year.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yearError
            // 
            this.yearError.BackColor = System.Drawing.Color.Red;
            this.yearError.Location = new System.Drawing.Point(7, 60);
            this.yearError.Name = "yearError";
            this.yearError.Size = new System.Drawing.Size(52, 5);
            this.yearError.TabIndex = 26;
            this.yearError.Visible = false;
            // 
            // monthError
            // 
            this.monthError.BackColor = System.Drawing.Color.Red;
            this.monthError.Location = new System.Drawing.Point(65, 60);
            this.monthError.Name = "monthError";
            this.monthError.Size = new System.Drawing.Size(30, 5);
            this.monthError.TabIndex = 27;
            this.monthError.Visible = false;
            // 
            // dayError
            // 
            this.dayError.BackColor = System.Drawing.Color.Red;
            this.dayError.Location = new System.Drawing.Point(101, 60);
            this.dayError.Name = "dayError";
            this.dayError.Size = new System.Drawing.Size(30, 5);
            this.dayError.TabIndex = 28;
            this.dayError.Visible = false;
            // 
            // hourError
            // 
            this.hourError.BackColor = System.Drawing.Color.Red;
            this.hourError.Location = new System.Drawing.Point(148, 60);
            this.hourError.Name = "hourError";
            this.hourError.Size = new System.Drawing.Size(30, 5);
            this.hourError.TabIndex = 29;
            this.hourError.Visible = false;
            // 
            // minuteError
            // 
            this.minuteError.BackColor = System.Drawing.Color.Red;
            this.minuteError.Location = new System.Drawing.Point(192, 60);
            this.minuteError.Name = "minuteError";
            this.minuteError.Size = new System.Drawing.Size(30, 5);
            this.minuteError.TabIndex = 30;
            this.minuteError.Visible = false;
            // 
            // TimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.minuteError);
            this.Controls.Add(this.hourError);
            this.Controls.Add(this.dayError);
            this.Controls.Add(this.monthError);
            this.Controls.Add(this.yearError);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.hour);
            this.Controls.Add(this.day);
            this.Controls.Add(this.month);
            this.Controls.Add(this.year);
            this.Controls.Add(this.panel2);
            this.Name = "TimeForm";
            this.Size = new System.Drawing.Size(232, 94);
            this.Load += new System.EventHandler(this.TimeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox minute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox hour;
        private System.Windows.Forms.TextBox day;
        private System.Windows.Forms.TextBox month;
        private System.Windows.Forms.TextBox year;
        private System.Windows.Forms.Panel yearError;
        private System.Windows.Forms.Panel monthError;
        private System.Windows.Forms.Panel dayError;
        private System.Windows.Forms.Panel hourError;
        private System.Windows.Forms.Panel minuteError;
    }
}
