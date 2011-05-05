namespace ATM
{
    partial class StockRequests
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.newRequestLabel = new System.Windows.Forms.Label();
            this.deleteRequestLabel = new System.Windows.Forms.Label();
            this.requestsView = new UI.BaseTableView();
            this.noRequestsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestsView)).BeginInit();
            this.SuspendLayout();
            // 
            // newRequestLabel
            // 
            this.newRequestLabel.BackColor = System.Drawing.Color.Transparent;
            this.newRequestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newRequestLabel.ForeColor = System.Drawing.Color.White;
            this.newRequestLabel.Location = new System.Drawing.Point(363, 293);
            this.newRequestLabel.Name = "newRequestLabel";
            this.newRequestLabel.Size = new System.Drawing.Size(434, 25);
            this.newRequestLabel.TabIndex = 16;
            this.newRequestLabel.Text = "1. Новая заявка";
            // 
            // deleteRequestLabel
            // 
            this.deleteRequestLabel.BackColor = System.Drawing.Color.Transparent;
            this.deleteRequestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteRequestLabel.ForeColor = System.Drawing.Color.White;
            this.deleteRequestLabel.Location = new System.Drawing.Point(363, 330);
            this.deleteRequestLabel.Name = "deleteRequestLabel";
            this.deleteRequestLabel.Size = new System.Drawing.Size(434, 25);
            this.deleteRequestLabel.TabIndex = 18;
            this.deleteRequestLabel.Text = "2. Снять заявку";
            // 
            // requestsView
            // 
            this.requestsView.AllowUserToAddRows = false;
            this.requestsView.AllowUserToDeleteRows = false;
            this.requestsView.AllowUserToResizeColumns = false;
            this.requestsView.AllowUserToResizeRows = false;
            this.requestsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.requestsView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.requestsView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.requestsView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.requestsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requestsView.ColumnHeadersVisible = false;
            this.requestsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.requestsView.Location = new System.Drawing.Point(363, 28);
            this.requestsView.MultiSelect = false;
            this.requestsView.Name = "requestsView";
            this.requestsView.ReadOnly = true;
            this.requestsView.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Olive;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.requestsView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.requestsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.requestsView.Size = new System.Drawing.Size(434, 262);
            this.requestsView.TabIndex = 19;
            this.requestsView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.requestsView_KeyDown);
            // 
            // noRequestsLabel
            // 
            this.noRequestsLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.noRequestsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.noRequestsLabel.ForeColor = System.Drawing.Color.White;
            this.noRequestsLabel.Location = new System.Drawing.Point(450, 126);
            this.noRequestsLabel.Name = "noRequestsLabel";
            this.noRequestsLabel.Size = new System.Drawing.Size(283, 25);
            this.noRequestsLabel.TabIndex = 20;
            this.noRequestsLabel.Text = "Заявок нет";
            this.noRequestsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(363, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "Ваши заявки:";
            // 
            // StockRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noRequestsLabel);
            this.Controls.Add(this.requestsView);
            this.Controls.Add(this.deleteRequestLabel);
            this.Controls.Add(this.newRequestLabel);
            this.Name = "StockRequests";
            this.Controls.SetChildIndex(this.pictureLogo, 0);
            this.Controls.SetChildIndex(this.escLabel, 0);
            this.Controls.SetChildIndex(this.newRequestLabel, 0);
            this.Controls.SetChildIndex(this.deleteRequestLabel, 0);
            this.Controls.SetChildIndex(this.requestsView, 0);
            this.Controls.SetChildIndex(this.noRequestsLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label newRequestLabel;
        private System.Windows.Forms.Label deleteRequestLabel;
        private UI.BaseTableView requestsView;
        private System.Windows.Forms.Label noRequestsLabel;
        private System.Windows.Forms.Label label1;
    }
}
