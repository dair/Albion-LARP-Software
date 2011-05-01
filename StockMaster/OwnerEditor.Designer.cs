namespace StockMaster
{
    partial class OwnerEditor
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
            this.personList = new UI.PersonList(getDatabase());
            this.sharesByPersonTableView = new StockMaster.SharesByPersonTableView(getDatabase());
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharesByPersonTableView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.personList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sharesByPersonTableView);
            this.splitContainer1.Panel2.Controls.Add(this.editButton);
            this.splitContainer1.Panel2.Controls.Add(this.addButton);
            this.splitContainer1.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer1.Panel2.Controls.Add(this.refreshButton);
            this.splitContainer1.Size = new System.Drawing.Size(707, 320);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 0;
            // 
            // personList
            // 
            this.personList.AllowUserToAddRows = false;
            this.personList.AllowUserToDeleteRows = false;
            this.personList.AllowUserToOrderColumns = true;
            this.personList.AllowUserToResizeRows = false;
            this.personList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.personList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.personList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.personList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.personList.Location = new System.Drawing.Point(0, 0);
            this.personList.MultiSelect = false;
            this.personList.Name = "personList";
            this.personList.ReadOnly = true;
            this.personList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.personList.Size = new System.Drawing.Size(165, 320);
            this.personList.TabIndex = 0;
            this.personList.SelectionChanged += new System.EventHandler(this.personList_SelectionChanged);
            // 
            // sharesByPersonTableView
            // 
            this.sharesByPersonTableView.AllowUserToAddRows = false;
            this.sharesByPersonTableView.AllowUserToDeleteRows = false;
            this.sharesByPersonTableView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sharesByPersonTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sharesByPersonTableView.Location = new System.Drawing.Point(2, 32);
            this.sharesByPersonTableView.MultiSelect = false;
            this.sharesByPersonTableView.Name = "sharesByPersonTableView";
            this.sharesByPersonTableView.ReadOnly = true;
            this.sharesByPersonTableView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sharesByPersonTableView.Size = new System.Drawing.Size(536, 288);
            this.sharesByPersonTableView.TabIndex = 5;
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Location = new System.Drawing.Point(344, 3);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(110, 23);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Редактировать";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(263, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(460, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(3, 3);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // OwnerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "OwnerEditor";
            this.Size = new System.Drawing.Size(707, 320);
            this.Load += new System.EventHandler(this.OwnerEditor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.personList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharesByPersonTableView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private UI.PersonList personList;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButton;
        private SharesByPersonTableView sharesByPersonTableView;

    }
}
