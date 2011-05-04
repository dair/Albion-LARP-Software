namespace StockMaster
{
    partial class RequestEditor
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
            this.requestList = new StockMaster.RequestList(getDatabase());
            this.processButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.requestList)).BeginInit();
            this.SuspendLayout();
            // 
            // requestList
            // 
            this.requestList.AllowUserToAddRows = false;
            this.requestList.AllowUserToDeleteRows = false;
            this.requestList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.requestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requestList.Location = new System.Drawing.Point(3, 35);
            this.requestList.MultiSelect = false;
            this.requestList.Name = "requestList";
            this.requestList.ReadOnly = true;
            this.requestList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.requestList.Size = new System.Drawing.Size(467, 245);
            this.requestList.TabIndex = 0;
            // 
            // processButton
            // 
            this.processButton.BackColor = System.Drawing.Color.Red;
            this.processButton.Location = new System.Drawing.Point(3, 6);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(183, 23);
            this.processButton.TabIndex = 1;
            this.processButton.Text = "Осуществить проводку";
            this.processButton.UseVisualStyleBackColor = false;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // RequestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.requestList);
            this.Name = "RequestEditor";
            this.Size = new System.Drawing.Size(473, 283);
            ((System.ComponentModel.ISupportInitialize)(this.requestList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RequestList requestList;
        private System.Windows.Forms.Button processButton;
    }
}
