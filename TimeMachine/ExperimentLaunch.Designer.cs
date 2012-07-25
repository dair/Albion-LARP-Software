namespace TimeMachine
{
    partial class ExperimentLaunch
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
            this.error = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.launchButton = new System.Windows.Forms.Button();
            this.energyLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mass = new System.Windows.Forms.TextBox();
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
            // error
            // 
            this.error.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.error.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(3, 24);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(656, 24);
            this.error.TabIndex = 15;
            this.error.Text = "Ошибка";
            this.error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.nameText);
            this.panel.Controls.Add(this.launchButton);
            this.panel.Controls.Add(this.energyLabel);
            this.panel.Controls.Add(this.cancelButton);
            this.panel.Controls.Add(this.saveButton);
            this.panel.Controls.Add(this.label5);
            this.panel.Controls.Add(this.mass);
            this.panel.Controls.Add(this.paramTime);
            this.panel.Controls.Add(this.paramSpace2);
            this.panel.Controls.Add(this.paramSpace1);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(7, 51);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(680, 377);
            this.panel.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(7, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 24);
            this.label6.TabIndex = 27;
            this.label6.Text = "Название:";
            // 
            // nameText
            // 
            this.nameText.Enabled = false;
            this.nameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameText.Location = new System.Drawing.Point(115, 38);
            this.nameText.MaxLength = 1000;
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(562, 29);
            this.nameText.TabIndex = 26;
            // 
            // launchButton
            // 
            this.launchButton.BackColor = System.Drawing.Color.Red;
            this.launchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.launchButton.ForeColor = System.Drawing.Color.White;
            this.launchButton.Location = new System.Drawing.Point(439, 275);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(238, 99);
            this.launchButton.TabIndex = 24;
            this.launchButton.Text = "ЗАПУСТИТЬ";
            this.launchButton.UseVisualStyleBackColor = false;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // energyLabel
            // 
            this.energyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.energyLabel.Location = new System.Drawing.Point(6, 230);
            this.energyLabel.Name = "energyLabel";
            this.energyLabel.Size = new System.Drawing.Size(671, 24);
            this.energyLabel.TabIndex = 25;
            this.energyLabel.Text = "Доступно энергии: $1 с $2 до $3";
            this.energyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.Location = new System.Drawing.Point(157, 275);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(135, 54);
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "Назад";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveButton.Location = new System.Drawing.Point(298, 275);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(135, 54);
            this.saveButton.TabIndex = 23;
            this.saveButton.Text = "Отменить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(20, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(361, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "Масса перемещаемого объекта, грамм";
            // 
            // mass
            // 
            this.mass.Enabled = false;
            this.mass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mass.Location = new System.Drawing.Point(387, 181);
            this.mass.Name = "mass";
            this.mass.Size = new System.Drawing.Size(187, 29);
            this.mass.TabIndex = 21;
            // 
            // paramTime
            // 
            this.paramTime.Enabled = false;
            this.paramTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramTime.Location = new System.Drawing.Point(194, 146);
            this.paramTime.Name = "paramTime";
            this.paramTime.Size = new System.Drawing.Size(187, 29);
            this.paramTime.TabIndex = 20;
            // 
            // paramSpace2
            // 
            this.paramSpace2.Enabled = false;
            this.paramSpace2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramSpace2.Location = new System.Drawing.Point(387, 111);
            this.paramSpace2.Name = "paramSpace2";
            this.paramSpace2.Size = new System.Drawing.Size(187, 29);
            this.paramSpace2.TabIndex = 19;
            // 
            // paramSpace1
            // 
            this.paramSpace1.Enabled = false;
            this.paramSpace1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramSpace1.Location = new System.Drawing.Point(194, 111);
            this.paramSpace1.Name = "paramSpace1";
            this.paramSpace1.Size = new System.Drawing.Size(187, 29);
            this.paramSpace1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(77, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 17;
            this.label4.Text = "временное";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(7, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 24);
            this.label3.TabIndex = 16;
            this.label3.Text = "пространственное";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(329, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "Смещение";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(264, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Старт машины";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title.Location = new System.Drawing.Point(3, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(343, 24);
            this.title.TabIndex = 0;
            this.title.Text = "Эксперимент $1 в рамках проекта $2";
            // 
            // ExperimentLaunch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.error);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.title);
            this.Name = "ExperimentLaunch";
            this.Size = new System.Drawing.Size(690, 431);
            this.Resize += new System.EventHandler(this.ExperimentEdit_Resize);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Label energyLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mass;
        private System.Windows.Forms.TextBox paramTime;
        private System.Windows.Forms.TextBox paramSpace2;
        private System.Windows.Forms.TextBox paramSpace1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label error;
    }
}
