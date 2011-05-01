namespace StockMaster
{
    partial class MainWindow
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cycleEditor1 = new StockMaster.CycleEditor(getDatabase());
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.newsEditor = new StockMaster.NewsEditor(getDatabase());
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.companiesEditor = new StockMaster.CompaniesEditor(getDatabase());
            this.tabPageOwners = new System.Windows.Forms.TabPage();
            this.ownerEditor1 = new StockMaster.OwnerEditor(getDatabase());
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPageOwners.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPageOwners);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(592, 373);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cycleEditor1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(584, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Оперативный простор";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cycleEditor1
            // 
            this.cycleEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cycleEditor1.Location = new System.Drawing.Point(3, 3);
            this.cycleEditor1.Name = "cycleEditor1";
            this.cycleEditor1.Size = new System.Drawing.Size(578, 341);
            this.cycleEditor1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.newsEditor);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(584, 347);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Новости";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // newsEditor
            // 
            this.newsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsEditor.Location = new System.Drawing.Point(3, 3);
            this.newsEditor.Name = "newsEditor";
            this.newsEditor.Size = new System.Drawing.Size(578, 341);
            this.newsEditor.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.companiesEditor);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(584, 347);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Компании";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // companiesEditor
            // 
            this.companiesEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.companiesEditor.Location = new System.Drawing.Point(3, 3);
            this.companiesEditor.Name = "companiesEditor";
            this.companiesEditor.Size = new System.Drawing.Size(578, 341);
            this.companiesEditor.TabIndex = 0;
            // 
            // tabPageOwners
            // 
            this.tabPageOwners.Controls.Add(this.ownerEditor1);
            this.tabPageOwners.Location = new System.Drawing.Point(4, 22);
            this.tabPageOwners.Name = "tabPageOwners";
            this.tabPageOwners.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOwners.Size = new System.Drawing.Size(584, 347);
            this.tabPageOwners.TabIndex = 3;
            this.tabPageOwners.Text = "Капиталисты";
            this.tabPageOwners.UseVisualStyleBackColor = true;
            // 
            // ownerEditor1
            // 
            this.ownerEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ownerEditor1.Location = new System.Drawing.Point(3, 3);
            this.ownerEditor1.Name = "ownerEditor1";
            this.ownerEditor1.Size = new System.Drawing.Size(578, 341);
            this.ownerEditor1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 373);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainWindow";
            this.Text = "Bladerunner-1993: Биржа";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPageOwners.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private NewsEditor newsEditor;
        private System.Windows.Forms.TabPage tabPage3;
        private CompaniesEditor companiesEditor;
        private System.Windows.Forms.TabPage tabPageOwners;
        private OwnerEditor ownerEditor1;
        private CycleEditor cycleEditor1;
    }
}

