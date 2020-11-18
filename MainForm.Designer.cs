namespace Flying47
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.TTimer = new System.Windows.Forms.Timer(this.components);
			this.LB_Running = new System.Windows.Forms.Label();
			this.InputPanel = new System.Windows.Forms.Panel();
			this.KeyPanel = new System.Windows.Forms.Panel();
			this.B_SaveProgramConfig = new System.Windows.Forms.Button();
			this.TB_MoveZAxis = new System.Windows.Forms.TextBox();
			this.TB_MoveXYAxis = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.B_KeyForward = new System.Windows.Forms.CheckBox();
			this.B_KeyDown = new System.Windows.Forms.CheckBox();
			this.B_KeyUp = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.B_LoadPosition = new System.Windows.Forms.CheckBox();
			this.B_StorePosition = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.vectorDisplay = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.L_Z = new System.Windows.Forms.Label();
			this.L_Y = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.L_X = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadOtherConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reloadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.githubRepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.teleportListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InputPanel.SuspendLayout();
			this.KeyPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.vectorDisplay)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TTimer
			// 
			this.TTimer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// LB_Running
			// 
			this.LB_Running.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LB_Running.Dock = System.Windows.Forms.DockStyle.Top;
			this.LB_Running.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LB_Running.ForeColor = System.Drawing.Color.Red;
			this.LB_Running.Location = new System.Drawing.Point(0, 24);
			this.LB_Running.Name = "LB_Running";
			this.LB_Running.Size = new System.Drawing.Size(391, 24);
			this.LB_Running.TabIndex = 1;
			this.LB_Running.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// InputPanel
			// 
			this.InputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.InputPanel.Controls.Add(this.KeyPanel);
			this.InputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.InputPanel.Location = new System.Drawing.Point(0, 143);
			this.InputPanel.Name = "InputPanel";
			this.InputPanel.Size = new System.Drawing.Size(391, 113);
			this.InputPanel.TabIndex = 41;
			// 
			// KeyPanel
			// 
			this.KeyPanel.Controls.Add(this.B_SaveProgramConfig);
			this.KeyPanel.Controls.Add(this.TB_MoveZAxis);
			this.KeyPanel.Controls.Add(this.TB_MoveXYAxis);
			this.KeyPanel.Controls.Add(this.label11);
			this.KeyPanel.Controls.Add(this.label10);
			this.KeyPanel.Controls.Add(this.B_KeyForward);
			this.KeyPanel.Controls.Add(this.B_KeyDown);
			this.KeyPanel.Controls.Add(this.B_KeyUp);
			this.KeyPanel.Controls.Add(this.label9);
			this.KeyPanel.Controls.Add(this.label8);
			this.KeyPanel.Controls.Add(this.B_LoadPosition);
			this.KeyPanel.Controls.Add(this.B_StorePosition);
			this.KeyPanel.Controls.Add(this.label6);
			this.KeyPanel.Controls.Add(this.label1);
			this.KeyPanel.Location = new System.Drawing.Point(5, 8);
			this.KeyPanel.Name = "KeyPanel";
			this.KeyPanel.Size = new System.Drawing.Size(380, 97);
			this.KeyPanel.TabIndex = 41;
			// 
			// B_SaveProgramConfig
			// 
			this.B_SaveProgramConfig.Location = new System.Drawing.Point(251, 66);
			this.B_SaveProgramConfig.Name = "B_SaveProgramConfig";
			this.B_SaveProgramConfig.Size = new System.Drawing.Size(129, 23);
			this.B_SaveProgramConfig.TabIndex = 60;
			this.B_SaveProgramConfig.Text = "Save program config";
			this.B_SaveProgramConfig.UseVisualStyleBackColor = true;
			this.B_SaveProgramConfig.Click += new System.EventHandler(this.B_SaveProgramConfig_Click);
			// 
			// TB_MoveZAxis
			// 
			this.TB_MoveZAxis.Location = new System.Drawing.Point(186, 68);
			this.TB_MoveZAxis.Name = "TB_MoveZAxis";
			this.TB_MoveZAxis.Size = new System.Drawing.Size(58, 20);
			this.TB_MoveZAxis.TabIndex = 55;
			this.TB_MoveZAxis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_MoveZAxis_KeyDown);
			// 
			// TB_MoveXYAxis
			// 
			this.TB_MoveXYAxis.Location = new System.Drawing.Point(69, 68);
			this.TB_MoveXYAxis.Name = "TB_MoveXYAxis";
			this.TB_MoveXYAxis.Size = new System.Drawing.Size(58, 20);
			this.TB_MoveXYAxis.TabIndex = 54;
			this.TB_MoveXYAxis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_MoveXYAxis_KeyDown);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(133, 71);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(47, 13);
			this.label11.TabIndex = 53;
			this.label11.Text = "Move Z:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 71);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(59, 13);
			this.label10.TabIndex = 52;
			this.label10.Text = "Move X/Y:";
			// 
			// B_KeyForward
			// 
			this.B_KeyForward.Appearance = System.Windows.Forms.Appearance.Button;
			this.B_KeyForward.Location = new System.Drawing.Point(251, 34);
			this.B_KeyForward.Name = "B_KeyForward";
			this.B_KeyForward.Size = new System.Drawing.Size(129, 26);
			this.B_KeyForward.TabIndex = 51;
			this.B_KeyForward.Text = "key_forward";
			this.B_KeyForward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.B_KeyForward.UseVisualStyleBackColor = true;
			this.B_KeyForward.CheckedChanged += new System.EventHandler(this.B_ChangeButton);
			this.B_KeyForward.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			// 
			// B_KeyDown
			// 
			this.B_KeyDown.Appearance = System.Windows.Forms.Appearance.Button;
			this.B_KeyDown.Location = new System.Drawing.Point(319, 5);
			this.B_KeyDown.Name = "B_KeyDown";
			this.B_KeyDown.Size = new System.Drawing.Size(61, 26);
			this.B_KeyDown.TabIndex = 50;
			this.B_KeyDown.Text = "key_dn";
			this.B_KeyDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.B_KeyDown.UseVisualStyleBackColor = true;
			this.B_KeyDown.CheckedChanged += new System.EventHandler(this.B_ChangeButton);
			this.B_KeyDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			// 
			// B_KeyUp
			// 
			this.B_KeyUp.Appearance = System.Windows.Forms.Appearance.Button;
			this.B_KeyUp.Location = new System.Drawing.Point(251, 5);
			this.B_KeyUp.Name = "B_KeyUp";
			this.B_KeyUp.Size = new System.Drawing.Size(61, 26);
			this.B_KeyUp.TabIndex = 49;
			this.B_KeyUp.Text = "key_up";
			this.B_KeyUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.B_KeyUp.UseVisualStyleBackColor = true;
			this.B_KeyUp.CheckedChanged += new System.EventHandler(this.B_ChangeButton);
			this.B_KeyUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(162, 41);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(90, 13);
			this.label9.TabIndex = 47;
			this.label9.Text = "Teleport Forward:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(163, 12);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(87, 13);
			this.label8.TabIndex = 46;
			this.label8.Text = "Move Up/Down:";
			// 
			// B_LoadPosition
			// 
			this.B_LoadPosition.Appearance = System.Windows.Forms.Appearance.Button;
			this.B_LoadPosition.Location = new System.Drawing.Point(84, 34);
			this.B_LoadPosition.Name = "B_LoadPosition";
			this.B_LoadPosition.Size = new System.Drawing.Size(73, 26);
			this.B_LoadPosition.TabIndex = 45;
			this.B_LoadPosition.Text = "key_load";
			this.B_LoadPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.B_LoadPosition.UseVisualStyleBackColor = true;
			this.B_LoadPosition.CheckedChanged += new System.EventHandler(this.B_ChangeButton);
			this.B_LoadPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			// 
			// B_StorePosition
			// 
			this.B_StorePosition.Appearance = System.Windows.Forms.Appearance.Button;
			this.B_StorePosition.Location = new System.Drawing.Point(84, 5);
			this.B_StorePosition.Name = "B_StorePosition";
			this.B_StorePosition.Size = new System.Drawing.Size(73, 26);
			this.B_StorePosition.TabIndex = 44;
			this.B_StorePosition.Text = "key_store";
			this.B_StorePosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.B_StorePosition.UseVisualStyleBackColor = true;
			this.B_StorePosition.CheckedChanged += new System.EventHandler(this.B_ChangeButton);
			this.B_StorePosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 41);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(74, 13);
			this.label6.TabIndex = 43;
			this.label6.Text = "Load Position:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 42;
			this.label1.Text = "Save Position:";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.vectorDisplay);
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(391, 95);
			this.panel1.TabIndex = 42;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(324, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(52, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Direction:";
			// 
			// vectorDisplay
			// 
			this.vectorDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.vectorDisplay.Location = new System.Drawing.Point(322, 24);
			this.vectorDisplay.Name = "vectorDisplay";
			this.vectorDisplay.Size = new System.Drawing.Size(63, 63);
			this.vectorDisplay.TabIndex = 3;
			this.vectorDisplay.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
			this.tableLayoutPanel1.Controls.Add(this.L_Z, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.L_Y, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_X, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(338, 63);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// L_Z
			// 
			this.L_Z.AutoSize = true;
			this.L_Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Z.Location = new System.Drawing.Point(206, 35);
			this.L_Z.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.L_Z.Name = "L_Z";
			this.L_Z.Size = new System.Drawing.Size(47, 13);
			this.L_Z.TabIndex = 6;
			this.L_Z.Text = "#####";
			// 
			// L_Y
			// 
			this.L_Y.AutoSize = true;
			this.L_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_Y.Location = new System.Drawing.Point(105, 35);
			this.L_Y.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.L_Y.Name = "L_Y";
			this.L_Y.Size = new System.Drawing.Size(47, 13);
			this.L_Y.TabIndex = 5;
			this.L_Y.Text = "#####";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(206, 1);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.label5.Size = new System.Drawing.Size(52, 18);
			this.label5.TabIndex = 4;
			this.label5.Text = "Z (height)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(105, 1);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.label4.Size = new System.Drawing.Size(14, 18);
			this.label4.TabIndex = 3;
			this.label4.Text = "Y";
			// 
			// L_X
			// 
			this.L_X.AutoSize = true;
			this.L_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.L_X.Location = new System.Drawing.Point(4, 35);
			this.L_X.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.L_X.Name = "L_X";
			this.L_X.Size = new System.Drawing.Size(47, 13);
			this.L_X.TabIndex = 1;
			this.L_X.Text = "#####";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 1);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.label3.Size = new System.Drawing.Size(14, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "X";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Current Coordinates:";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.teleportListToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(391, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mainToolStripMenuItem
			// 
			this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadOtherConfigToolStripMenuItem,
            this.reloadConfigToolStripMenuItem,
            this.toolStripSeparator1,
            this.alwaysOnTopToolStripMenuItem,
            this.githubRepositoryToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
			this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.mainToolStripMenuItem.Text = "Main";
			// 
			// loadOtherConfigToolStripMenuItem
			// 
			this.loadOtherConfigToolStripMenuItem.Name = "loadOtherConfigToolStripMenuItem";
			this.loadOtherConfigToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.loadOtherConfigToolStripMenuItem.Text = "Load game config";
			this.loadOtherConfigToolStripMenuItem.Click += new System.EventHandler(this.LoadOtherConfigToolStripMenuItem_Click);
			// 
			// reloadConfigToolStripMenuItem
			// 
			this.reloadConfigToolStripMenuItem.Name = "reloadConfigToolStripMenuItem";
			this.reloadConfigToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.reloadConfigToolStripMenuItem.Text = "Reload game config";
			this.reloadConfigToolStripMenuItem.Click += new System.EventHandler(this.ReloadConfigToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// alwaysOnTopToolStripMenuItem
			// 
			this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
			this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
			this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
			this.alwaysOnTopToolStripMenuItem.CheckedChanged += new System.EventHandler(this.AlwaysOnTopToolStripMenuItem_CheckedChanged);
			// 
			// githubRepositoryToolStripMenuItem
			// 
			this.githubRepositoryToolStripMenuItem.Name = "githubRepositoryToolStripMenuItem";
			this.githubRepositoryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.githubRepositoryToolStripMenuItem.Text = "Github repository";
			this.githubRepositoryToolStripMenuItem.Click += new System.EventHandler(this.GithubRepositoryToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// teleportListToolStripMenuItem
			// 
			this.teleportListToolStripMenuItem.Name = "teleportListToolStripMenuItem";
			this.teleportListToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
			this.teleportListToolStripMenuItem.Text = "Teleport list";
			this.teleportListToolStripMenuItem.Click += new System.EventHandler(this.TeleportListToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(391, 256);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.InputPanel);
			this.Controls.Add(this.LB_Running);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Universal Trainer for Speedrunners";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.InputPanel.ResumeLayout(false);
			this.KeyPanel.ResumeLayout(false);
			this.KeyPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.vectorDisplay)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer TTimer;
        private System.Windows.Forms.Label LB_Running;
        private System.Windows.Forms.Panel InputPanel;
        private System.Windows.Forms.Panel KeyPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label L_X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label L_Z;
        private System.Windows.Forms.Label L_Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox B_StorePosition;
        private System.Windows.Forms.CheckBox B_LoadPosition;
        private System.Windows.Forms.PictureBox vectorDisplay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox B_KeyForward;
        private System.Windows.Forms.CheckBox B_KeyDown;
        private System.Windows.Forms.CheckBox B_KeyUp;
        private System.Windows.Forms.TextBox TB_MoveZAxis;
        private System.Windows.Forms.TextBox TB_MoveXYAxis;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button B_SaveProgramConfig;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadOtherConfigToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reloadConfigToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem githubRepositoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem teleportListToolStripMenuItem;
	}
}

