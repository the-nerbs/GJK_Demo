namespace GJK_Demo
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
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
            this.shapesGrid = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.workspace = new GJK_Demo.WorkspacePanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblIntersecting = new System.Windows.Forms.ToolStripStatusLabel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shapesGrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.shapesGrid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.workspace);
            splitContainer1.Size = new System.Drawing.Size(822, 401);
            splitContainer1.SplitterDistance = 150;
            splitContainer1.TabIndex = 1;
            // 
            // shapesGrid
            // 
            this.shapesGrid.AllowUserToAddRows = false;
            this.shapesGrid.AllowUserToDeleteRows = false;
            this.shapesGrid.AllowUserToResizeColumns = false;
            this.shapesGrid.AllowUserToResizeRows = false;
            this.shapesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.shapesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colEdit});
            this.shapesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shapesGrid.Location = new System.Drawing.Point(0, 0);
            this.shapesGrid.Name = "shapesGrid";
            this.shapesGrid.RowHeadersVisible = false;
            this.shapesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.shapesGrid.Size = new System.Drawing.Size(150, 401);
            this.shapesGrid.TabIndex = 0;
            this.shapesGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.shapesGrid_CellContentClick);
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colEdit
            // 
            this.colEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colEdit.HeaderText = "Edit";
            this.colEdit.MinimumWidth = 50;
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Text = "...";
            this.colEdit.Width = 50;
            // 
            // workspace
            // 
            this.workspace.BackColor = System.Drawing.Color.White;
            this.workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspace.Location = new System.Drawing.Point(0, 0);
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(668, 401);
            this.workspace.TabIndex = 0;
            this.workspace.TabStop = true;
            this.workspace.ShapeMoved += new System.EventHandler<GJK_Demo.ShapeMovedEventArgs>(this.workspace_ShapeMoved);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripStatusLabel1,
            this.lblIntersecting});
            this.statusStrip1.Location = new System.Drawing.Point(0, 401);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(822, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(112, 17);
            toolStripStatusLabel1.Text = "Shapes intersecting:";
            // 
            // lblIntersecting
            // 
            this.lblIntersecting.Name = "lblIntersecting";
            this.lblIntersecting.Size = new System.Drawing.Size(39, 17);
            this.lblIntersecting.Text = "FALSE";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 423);
            this.Controls.Add(splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "GJK Demo";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.shapesGrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView shapesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private WorkspacePanel workspace;
        private System.Windows.Forms.ToolStripStatusLabel lblIntersecting;
    }
}