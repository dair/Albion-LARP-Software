namespace TimeMachine
{
    partial class LaunchFinish
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
            this.label6 = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.mass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mass2 = new System.Windows.Forms.TextBox();
            this.paramTime = new System.Windows.Forms.TextBox();
            this.paramSpace2 = new System.Windows.Forms.TextBox();
            this.paramSpace1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(7, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 24);
            this.label6.TabIndex = 27;
            this.label6.Text = "Название:";
            // 
            // nameText
            // 
            this.nameText.Enabled = false;
            this.nameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameText.Location = new System.Drawing.Point(115, 29);
            this.nameText.MaxLength = 1000;
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(562, 29);
            this.nameText.TabIndex = 26;
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.Controls.Add(this.timeLabel);
            this.panel.Controls.Add(this.mass);
            this.panel.Controls.Add(this.label7);
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.nameText);
            this.panel.Controls.Add(this.cancelButton);
            this.panel.Controls.Add(this.label5);
            this.panel.Controls.Add(this.mass2);
            this.panel.Controls.Add(this.paramTime);
            this.panel.Controls.Add(this.paramSpace2);
            this.panel.Controls.Add(this.paramSpace1);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(4, 59);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(680, 332);
            this.panel.TabIndex = 17;
            // 
            // timeLabel
            // 
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeLabel.Location = new System.Drawing.Point(7, 69);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(670, 24);
            this.timeLabel.TabIndex = 30;
            this.timeLabel.Text = "Период запуска";
            // 
            // mass
            // 
            this.mass.Enabled = false;
            this.mass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mass.Location = new System.Drawing.Point(387, 194);
            this.mass.Name = "mass";
            this.mass.Size = new System.Drawing.Size(187, 29);
            this.mass.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(20, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(361, 24);
            this.label7.TabIndex = 28;
            this.label7.Text = "Масса перемещаемого объекта, грамм";
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.Location = new System.Drawing.Point(278, 275);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(135, 54);
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "В меню";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(20, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(360, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "Масса перемещённого объекта, грамм";
            // 
            // mass2
            // 
            this.mass2.Enabled = false;
            this.mass2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mass2.Location = new System.Drawing.Point(387, 226);
            this.mass2.Name = "mass2";
            this.mass2.Size = new System.Drawing.Size(187, 29);
            this.mass2.TabIndex = 21;
            // 
            // paramTime
            // 
            this.paramTime.Enabled = false;
            this.paramTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramTime.Location = new System.Drawing.Point(194, 159);
            this.paramTime.Name = "paramTime";
            this.paramTime.Size = new System.Drawing.Size(187, 29);
            this.paramTime.TabIndex = 20;
            // 
            // paramSpace2
            // 
            this.paramSpace2.Enabled = false;
            this.paramSpace2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramSpace2.Location = new System.Drawing.Point(387, 124);
            this.paramSpace2.Name = "paramSpace2";
            this.paramSpace2.Size = new System.Drawing.Size(187, 29);
            this.paramSpace2.TabIndex = 19;
            // 
            // paramSpace1
            // 
            this.paramSpace1.Enabled = false;
            this.paramSpace1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramSpace1.Location = new System.Drawing.Point(194, 124);
            this.paramSpace1.Name = "paramSpace1";
            this.paramSpace1.Size = new System.Drawing.Size(187, 29);
            this.paramSpace1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(77, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 17;
            this.label4.Text = "временное";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(7, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 24);
            this.label3.TabIndex = 16;
            this.label3.Text = "пространственное";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(329, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "Смещение";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(239, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Результаты эксперимента";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title.Location = new System.Drawing.Point(4, 4);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(343, 24);
            this.title.TabIndex = 16;
            this.title.Text = "Эксперимент $1 в рамках проекта $2";
            // 
            // LaunchFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.title);
            this.Name = "LaunchFinish";
            this.Size = new System.Drawing.Size(687, 394);
            this.Resize += new System.EventHandler(this.LaunchFinish_Resize);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox mass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mass2;
        private System.Windows.Forms.TextBox paramTime;
        private System.Windows.Forms.TextBox paramSpace2;
        private System.Windows.Forms.TextBox paramSpace1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label timeLabel;

    }
}
