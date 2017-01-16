namespace GJK_Demo
{
    partial class PolygonEditorPage
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
            this.pointsGrid = new System.Windows.Forms.DataGridView();
            this.colX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pointsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pointsGrid
            // 
            this.pointsGrid.AllowUserToResizeColumns = false;
            this.pointsGrid.AllowUserToResizeRows = false;
            this.pointsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pointsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX,
            this.colY});
            this.pointsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pointsGrid.Location = new System.Drawing.Point(0, 0);
            this.pointsGrid.Name = "pointsGrid";
            this.pointsGrid.Size = new System.Drawing.Size(232, 328);
            this.pointsGrid.TabIndex = 0;
            this.pointsGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.pointsGrid_CellValueChanged);
            this.pointsGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.pointsGrid_RowsAdded);
            this.pointsGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.pointsGrid_RowsRemoved);
            // 
            // colX
            // 
            this.colX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colX.HeaderText = "X";
            this.colX.Name = "colX";
            // 
            // colY
            // 
            this.colY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colY.HeaderText = "Y";
            this.colY.Name = "colY";
            // 
            // PolygonEditorPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pointsGrid);
            this.Name = "PolygonEditorPage";
            this.Size = new System.Drawing.Size(232, 328);
            ((System.ComponentModel.ISupportInitialize)(this.pointsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView pointsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colY;
    }
}
