namespace Flying47
{
    partial class PositionsListForm
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
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_SaveTable = new System.Windows.Forms.Button();
			this.B_RemoveEntry = new System.Windows.Forms.Button();
			this.B_Add = new System.Windows.Forms.Button();
			this.B_LoadTable = new System.Windows.Forms.Button();
			this.B_TeleportTo = new System.Windows.Forms.Button();
			this.positionGrid = new System.Windows.Forms.DataGridView();
			this.PosName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ContextMenu_DataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.positionGrid)).BeginInit();
			this.ContextMenu_DataGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.positionGrid, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(800, 450);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.B_SaveTable, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.B_RemoveEntry, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_LoadTable, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.B_TeleportTo, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(683, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(114, 444);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// B_SaveTable
			// 
			this.B_SaveTable.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_SaveTable.Location = new System.Drawing.Point(10, 416);
			this.B_SaveTable.Name = "B_SaveTable";
			this.B_SaveTable.Size = new System.Drawing.Size(94, 23);
			this.B_SaveTable.TabIndex = 3;
			this.B_SaveTable.Text = "Save Table";
			this.B_SaveTable.UseVisualStyleBackColor = true;
			this.B_SaveTable.Click += new System.EventHandler(this.B_SaveTable_Click);
			// 
			// B_RemoveEntry
			// 
			this.B_RemoveEntry.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_RemoveEntry.Location = new System.Drawing.Point(10, 36);
			this.B_RemoveEntry.Name = "B_RemoveEntry";
			this.B_RemoveEntry.Size = new System.Drawing.Size(94, 23);
			this.B_RemoveEntry.TabIndex = 1;
			this.B_RemoveEntry.Text = "Remove entry";
			this.B_RemoveEntry.UseVisualStyleBackColor = true;
			this.B_RemoveEntry.Click += new System.EventHandler(this.B_RemoveEntry_Click);
			// 
			// B_Add
			// 
			this.B_Add.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Add.Location = new System.Drawing.Point(10, 4);
			this.B_Add.Name = "B_Add";
			this.B_Add.Size = new System.Drawing.Size(94, 23);
			this.B_Add.TabIndex = 0;
			this.B_Add.Text = "Add new";
			this.B_Add.UseVisualStyleBackColor = true;
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// B_LoadTable
			// 
			this.B_LoadTable.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_LoadTable.Location = new System.Drawing.Point(10, 384);
			this.B_LoadTable.Name = "B_LoadTable";
			this.B_LoadTable.Size = new System.Drawing.Size(94, 23);
			this.B_LoadTable.TabIndex = 2;
			this.B_LoadTable.Text = "Load Table";
			this.B_LoadTable.UseVisualStyleBackColor = true;
			this.B_LoadTable.Click += new System.EventHandler(this.B_LoadTable_Click);
			// 
			// B_TeleportTo
			// 
			this.B_TeleportTo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_TeleportTo.Location = new System.Drawing.Point(9, 69);
			this.B_TeleportTo.Name = "B_TeleportTo";
			this.B_TeleportTo.Size = new System.Drawing.Size(95, 38);
			this.B_TeleportTo.TabIndex = 4;
			this.B_TeleportTo.Text = "Teleport to selected";
			this.B_TeleportTo.UseVisualStyleBackColor = true;
			this.B_TeleportTo.Click += new System.EventHandler(this.B_TeleportTo_Click);
			// 
			// positionGrid
			// 
			this.positionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.positionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PosName,
            this.X,
            this.Y,
            this.Z});
			this.positionGrid.ContextMenuStrip = this.ContextMenu_DataGrid;
			this.positionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.positionGrid.Location = new System.Drawing.Point(3, 3);
			this.positionGrid.MultiSelect = false;
			this.positionGrid.Name = "positionGrid";
			this.positionGrid.Size = new System.Drawing.Size(674, 444);
			this.positionGrid.TabIndex = 1;
			this.positionGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.positionGrid_CellBeginEdit);
			this.positionGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.positionGrid_CellEndEdit);
			this.positionGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.PositionGrid_CellLeave);
			this.positionGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PositionGrid_CellValueChanged);
			// 
			// PosName
			// 
			this.PosName.HeaderText = "Name";
			this.PosName.Name = "PosName";
			// 
			// X
			// 
			this.X.HeaderText = "X";
			this.X.Name = "X";
			// 
			// Y
			// 
			this.Y.HeaderText = "Y";
			this.Y.Name = "Y";
			// 
			// Z
			// 
			this.Z.HeaderText = "Z";
			this.Z.Name = "Z";
			// 
			// ContextMenu_DataGrid
			// 
			this.ContextMenu_DataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.cancelToolStripMenuItem});
			this.ContextMenu_DataGrid.Name = "ContextMenu_DataGrid";
			this.ContextMenu_DataGrid.Size = new System.Drawing.Size(118, 70);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.addToolStripMenuItem.Text = "Add";
			this.addToolStripMenuItem.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.B_RemoveEntry_Click);
			// 
			// cancelToolStripMenuItem
			// 
			this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
			this.cancelToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.cancelToolStripMenuItem.Text = "Cancel";
			// 
			// PositionsListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "PositionsListForm";
			this.Text = "List of Positions";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PositionsListForm_FormClosing);
			this.Load += new System.EventHandler(this.PositionsListForm_Load);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.positionGrid)).EndInit();
			this.ContextMenu_DataGrid.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button B_Add;
        private System.Windows.Forms.Button B_SaveTable;
        private System.Windows.Forms.Button B_RemoveEntry;
        private System.Windows.Forms.Button B_LoadTable;
        private System.Windows.Forms.DataGridView positionGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosName;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.Button B_TeleportTo;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_DataGrid;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
    }
}