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
            // RequestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.requestList);
            this.Name = "RequestEditor";
            this.Size = new System.Drawing.Size(473, 283);
            ((System.ComponentModel.ISupportInitialize)(this.requestList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RequestList requestList;
    }
}
