namespace TimeMachine
{
    partial class SelectProject
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
            this.panel = new System.Windows.Forms.Panel();
            this.cancel = new System.Windows.Forms.Button();
            this.commit = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.leaderId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.projectKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.AutoSize = true;
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.Controls.Add(this.cancel);
            this.panel.Controls.Add(this.commit);
            this.panel.Controls.Add(this.password);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.leaderId);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.projectKey);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(22, 152);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(478, 142);
            this.panel.TabIndex = 6;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel.Location = new System.Drawing.Point(259, 105);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(108, 34);
            this.cancel.TabIndex = 13;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // commit
            // 
            this.commit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.commit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commit.Location = new System.Drawing.Point(145, 105);
            this.commit.Name = "commit";
            this.commit.Size = new System.Drawing.Size(108, 34);
            this.commit.TabIndex = 12;
            this.commit.Text = "Выбрать";
            this.commit.UseVisualStyleBackColor = true;
            this.commit.Click += new System.EventHandler(this.commit_Click);
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password.Location = new System.Drawing.Point(259, 70);
            this.password.MaxLength = 10;
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(216, 29);
            this.password.TabIndex = 11;
            this.password.Text = "123456";
            this.password.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(4, 70);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(249, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "Пароль лидера проекта:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // leaderId
            // 
            this.leaderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leaderId.Location = new System.Drawing.Point(259, 35);
            this.leaderId.MaxLength = 10;
            this.leaderId.Name = "leaderId";
            this.leaderId.Size = new System.Drawing.Size(216, 29);
            this.leaderId.TabIndex = 9;
            this.leaderId.Text = "123456";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(56, 35);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(197, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "ID лидера проекта:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // projectKey
            // 
            this.projectKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.projectKey.Location = new System.Drawing.Point(259, 0);
            this.projectKey.MaxLength = 8;
            this.projectKey.Name = "projectKey";
            this.projectKey.Size = new System.Drawing.Size(216, 29);
            this.projectKey.TabIndex = 7;
            this.projectKey.Text = "123456";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(93, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(160, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Код проекта:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // error
            // 
            this.error.BackColor = System.Drawing.Color.Transparent;
            this.error.Dock = System.Windows.Forms.DockStyle.Top;
            this.error.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(0, 0);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(546, 93);
            this.error.TabIndex = 8;
            this.error.Text = "label4";
            this.error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.error);
            this.Controls.Add(this.panel);
            this.Name = "SelectProject";
            this.Size = new System.Drawing.Size(546, 418);
            this.Resize += new System.EventHandler(this.SelectProject_Resize);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox leaderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox projectKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button commit;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label error;

    }
}
