namespace UI
{
    partial class PersonInfo
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
            this.label2 = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deletePropButton = new System.Windows.Forms.Button();
            this.editPropButton = new System.Windows.Forms.Button();
            this.addPropButton = new System.Windows.Forms.Button();
            this.propertiesGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.genderBox = new System.Windows.Forms.ComboBox();
            this.genomeBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // idBox
            // 
            this.idBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.idBox.Location = new System.Drawing.Point(54, 3);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(250, 20);
            this.idBox.TabIndex = 2;
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Location = new System.Drawing.Point(54, 29);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(250, 20);
            this.nameBox.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.deletePropButton);
            this.groupBox1.Controls.Add(this.editPropButton);
            this.groupBox1.Controls.Add(this.addPropButton);
            this.groupBox1.Controls.Add(this.propertiesGridView);
            this.groupBox1.Location = new System.Drawing.Point(3, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 207);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // deletePropButton
            // 
            this.deletePropButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePropButton.Location = new System.Drawing.Point(226, 19);
            this.deletePropButton.Name = "deletePropButton";
            this.deletePropButton.Size = new System.Drawing.Size(75, 23);
            this.deletePropButton.TabIndex = 3;
            this.deletePropButton.Text = "Удалить";
            this.deletePropButton.UseVisualStyleBackColor = true;
            this.deletePropButton.Click += new System.EventHandler(this.deletePropButton_Click);
            // 
            // editPropButton
            // 
            this.editPropButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editPropButton.Location = new System.Drawing.Point(124, 19);
            this.editPropButton.Name = "editPropButton";
            this.editPropButton.Size = new System.Drawing.Size(96, 23);
            this.editPropButton.TabIndex = 2;
            this.editPropButton.Text = "Редактировать";
            this.editPropButton.UseVisualStyleBackColor = true;
            this.editPropButton.Click += new System.EventHandler(this.editPropButton_Click);
            // 
            // addPropButton
            // 
            this.addPropButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addPropButton.Location = new System.Drawing.Point(43, 19);
            this.addPropButton.Name = "addPropButton";
            this.addPropButton.Size = new System.Drawing.Size(75, 23);
            this.addPropButton.TabIndex = 1;
            this.addPropButton.Text = "Добавить";
            this.addPropButton.UseVisualStyleBackColor = true;
            this.addPropButton.Click += new System.EventHandler(this.addPropButton_Click);
            // 
            // propertiesGridView
            // 
            this.propertiesGridView.AllowUserToAddRows = false;
            this.propertiesGridView.AllowUserToDeleteRows = false;
            this.propertiesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesGridView.Location = new System.Drawing.Point(6, 48);
            this.propertiesGridView.Name = "propertiesGridView";
            this.propertiesGridView.ReadOnly = true;
            this.propertiesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.propertiesGridView.Size = new System.Drawing.Size(295, 153);
            this.propertiesGridView.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пол:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // genderBox
            // 
            this.genderBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.genderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderBox.FormattingEnabled = true;
            this.genderBox.Items.AddRange(new object[] {
            "Неизвестно",
            "Мальчик",
            "Девочка"});
            this.genderBox.Location = new System.Drawing.Point(54, 56);
            this.genderBox.Name = "genderBox";
            this.genderBox.Size = new System.Drawing.Size(250, 21);
            this.genderBox.TabIndex = 6;
            // 
            // genomeBox
            // 
            this.genomeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.genomeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genomeBox.Items.AddRange(new object[] {
            "Человек",
            "Андроид"});
            this.genomeBox.Location = new System.Drawing.Point(54, 83);
            this.genomeBox.Name = "genomeBox";
            this.genomeBox.Size = new System.Drawing.Size(250, 21);
            this.genomeBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Геном:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.genomeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.genderBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PersonInfo";
            this.Size = new System.Drawing.Size(313, 320);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView propertiesGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox genderBox;
        private System.Windows.Forms.ComboBox genomeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deletePropButton;
        private System.Windows.Forms.Button editPropButton;
        private System.Windows.Forms.Button addPropButton;
    }
}
