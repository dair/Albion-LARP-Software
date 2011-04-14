namespace Master
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
            this.personEditor = new Master.PersonEditor(getDatabase());
            this.tabControl = new System.Windows.Forms.TabControl();
            this.personPage = new System.Windows.Forms.TabPage();
            this.propPage = new System.Windows.Forms.TabPage();
            this.propertyEditor1 = new Master.PropertyEditor(getDatabase());
            this.tabControl.SuspendLayout();
            this.personPage.SuspendLayout();
            this.propPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // personEditor
            // 
            this.personEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.personEditor.Location = new System.Drawing.Point(0, 0);
            this.personEditor.Name = "personEditor";
            this.personEditor.Size = new System.Drawing.Size(512, 313);
            this.personEditor.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.personPage);
            this.tabControl.Controls.Add(this.propPage);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(520, 339);
            this.tabControl.TabIndex = 1;
            // 
            // personPage
            // 
            this.personPage.Controls.Add(this.personEditor);
            this.personPage.Location = new System.Drawing.Point(4, 22);
            this.personPage.Name = "personPage";
            this.personPage.Padding = new System.Windows.Forms.Padding(3);
            this.personPage.Size = new System.Drawing.Size(512, 313);
            this.personPage.TabIndex = 0;
            this.personPage.Text = "Персонажи";
            this.personPage.UseVisualStyleBackColor = true;
            // 
            // propPage
            // 
            this.propPage.Controls.Add(this.propertyEditor1);
            this.propPage.Location = new System.Drawing.Point(4, 22);
            this.propPage.Name = "propPage";
            this.propPage.Padding = new System.Windows.Forms.Padding(3);
            this.propPage.Size = new System.Drawing.Size(512, 313);
            this.propPage.TabIndex = 1;
            this.propPage.Text = "Свойства";
            this.propPage.UseVisualStyleBackColor = true;
            // 
            // propertyEditor1
            // 
            this.propertyEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyEditor1.Location = new System.Drawing.Point(6, 6);
            this.propertyEditor1.Name = "propertyEditor1";
            this.propertyEditor1.Size = new System.Drawing.Size(500, 301);
            this.propertyEditor1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 363);
            this.Controls.Add(this.tabControl);
            this.Name = "MainWindow";
            this.Text = "Bladerunner-1993: Мастерская";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabControl.ResumeLayout(false);
            this.personPage.ResumeLayout(false);
            this.propPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PersonEditor personEditor;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage personPage;
        private System.Windows.Forms.TabPage propPage;
        private PropertyEditor propertyEditor1;

    }
}