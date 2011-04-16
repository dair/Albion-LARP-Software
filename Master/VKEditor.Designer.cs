namespace Master
{
    partial class VKEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.vkQuestionList = new Master.VKQuestionList(getDatabase());
            this.refreshButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.genderBox = new System.Windows.Forms.ComboBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.idBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.editAnswerButton = new System.Windows.Forms.Button();
            this.deleteAnswerButton = new System.Windows.Forms.Button();
            this.addAnswerButton = new System.Windows.Forms.Button();
            this.vkAnswerList = new Master.VKAnswerList(getDatabase());
            this.saveButton = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vkQuestionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vkAnswerList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.vkQuestionList);
            this.splitContainer1.Panel1.Controls.Add(this.refreshButton);
            this.splitContainer1.Panel1.Controls.Add(this.deleteButton);
            this.splitContainer1.Panel1.Controls.Add(this.addButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.genderBox);
            this.splitContainer1.Panel2.Controls.Add(this.textBox);
            this.splitContainer1.Panel2.Controls.Add(this.idBox);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.editAnswerButton);
            this.splitContainer1.Panel2.Controls.Add(this.deleteAnswerButton);
            this.splitContainer1.Panel2.Controls.Add(this.addAnswerButton);
            this.splitContainer1.Panel2.Controls.Add(this.vkAnswerList);
            this.splitContainer1.Panel2.Controls.Add(this.saveButton);
            this.splitContainer1.Size = new System.Drawing.Size(727, 319);
            this.splitContainer1.SplitterDistance = 381;
            this.splitContainer1.TabIndex = 0;
            // 
            // vkQuestionList
            // 
            this.vkQuestionList.AllowUserToAddRows = false;
            this.vkQuestionList.AllowUserToDeleteRows = false;
            this.vkQuestionList.AllowUserToResizeRows = false;
            this.vkQuestionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vkQuestionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.vkQuestionList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.vkQuestionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vkQuestionList.Location = new System.Drawing.Point(0, 32);
            this.vkQuestionList.Name = "vkQuestionList";
            this.vkQuestionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.vkQuestionList.Size = new System.Drawing.Size(378, 287);
            this.vkQuestionList.TabIndex = 7;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(3, 3);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(303, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(222, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ответы:";
            // 
            // genderBox
            // 
            this.genderBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.genderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderBox.Items.AddRange(new object[] {
            "Любой",
            "Мальчик",
            "Девочка"});
            this.genderBox.Location = new System.Drawing.Point(49, 81);
            this.genderBox.Name = "genderBox";
            this.genderBox.Size = new System.Drawing.Size(290, 21);
            this.genderBox.TabIndex = 14;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(49, 55);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(290, 20);
            this.textBox.TabIndex = 13;
            // 
            // idBox
            // 
            this.idBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.idBox.Location = new System.Drawing.Point(49, 32);
            this.idBox.Name = "idBox";
            this.idBox.ReadOnly = true;
            this.idBox.Size = new System.Drawing.Size(290, 20);
            this.idBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Пол:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Текст:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // editAnswerButton
            // 
            this.editAnswerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editAnswerButton.Location = new System.Drawing.Point(154, 108);
            this.editAnswerButton.Name = "editAnswerButton";
            this.editAnswerButton.Size = new System.Drawing.Size(104, 23);
            this.editAnswerButton.TabIndex = 8;
            this.editAnswerButton.Text = "Редактировать";
            this.editAnswerButton.UseVisualStyleBackColor = true;
            this.editAnswerButton.Click += new System.EventHandler(this.editAnswerButton_Click);
            // 
            // deleteAnswerButton
            // 
            this.deleteAnswerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAnswerButton.Location = new System.Drawing.Point(264, 108);
            this.deleteAnswerButton.Name = "deleteAnswerButton";
            this.deleteAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.deleteAnswerButton.TabIndex = 7;
            this.deleteAnswerButton.Text = "Удалить";
            this.deleteAnswerButton.UseVisualStyleBackColor = true;
            this.deleteAnswerButton.Click += new System.EventHandler(this.deleteAnswerButton_Click);
            // 
            // addAnswerButton
            // 
            this.addAnswerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addAnswerButton.Location = new System.Drawing.Point(73, 108);
            this.addAnswerButton.Name = "addAnswerButton";
            this.addAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.addAnswerButton.TabIndex = 6;
            this.addAnswerButton.Text = "Добавить";
            this.addAnswerButton.UseVisualStyleBackColor = true;
            this.addAnswerButton.Click += new System.EventHandler(this.addAnswerButton_Click);
            // 
            // vkAnswerList
            // 
            this.vkAnswerList.AllowUserToAddRows = false;
            this.vkAnswerList.AllowUserToDeleteRows = false;
            this.vkAnswerList.AllowUserToResizeRows = false;
            this.vkAnswerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vkAnswerList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.vkAnswerList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.vkAnswerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vkAnswerList.Location = new System.Drawing.Point(3, 137);
            this.vkAnswerList.Name = "vkAnswerList";
            this.vkAnswerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.vkAnswerList.Size = new System.Drawing.Size(336, 179);
            this.vkAnswerList.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(264, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // VKEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "VKEditor";
            this.Size = new System.Drawing.Size(727, 322);
            this.Load += new System.EventHandler(this.VKEditor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vkQuestionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vkAnswerList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private VKQuestionList vkQuestionList;
        private System.Windows.Forms.Button saveButton;
        private VKAnswerList vkAnswerList;
        private System.Windows.Forms.Button deleteAnswerButton;
        private System.Windows.Forms.Button addAnswerButton;
        private System.Windows.Forms.Button editAnswerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ComboBox genderBox;
        private System.Windows.Forms.Label label4;
    }
}
