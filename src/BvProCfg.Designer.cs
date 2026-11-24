namespace ezx6tools
{
    partial class BvProCfg
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
            TargetList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            Save = new Button();
            Discard = new Button();
            TheLabel = new Label();
            ProjectId = new TextBox();
            LblSi = new Label();
            AddTarget = new Button();
            RemoveTarget = new Button();
            LblSlct = new Label();
            EnableOrDisable = new Button();
            SuspendLayout();
            // 
            // TargetList
            // 
            TargetList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            TargetList.Dock = DockStyle.Bottom;
            TargetList.Location = new Point(0, 158);
            TargetList.MaximumSize = new Size(800, 292);
            TargetList.MinimumSize = new Size(800, 292);
            TargetList.Name = "TargetList";
            TargetList.Size = new Size(800, 292);
            TargetList.TabIndex = 0;
            TargetList.UseCompatibleStateImageBehavior = false;
            TargetList.View = View.Details;
            TargetList.Click += TargetList_Click;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Index";
            columnHeader1.Width = 41;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Target";
            columnHeader2.Width = 755;
            // 
            // Save
            // 
            Save.Enabled = false;
            Save.Location = new Point(12, 12);
            Save.Name = "Save";
            Save.Size = new Size(101, 23);
            Save.TabIndex = 1;
            Save.Text = "Save changes";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // Discard
            // 
            Discard.Enabled = false;
            Discard.Location = new Point(119, 12);
            Discard.Name = "Discard";
            Discard.Size = new Size(101, 23);
            Discard.TabIndex = 2;
            Discard.Text = "Discard changes";
            Discard.UseVisualStyleBackColor = true;
            Discard.Click += Discard_Click;
            // 
            // TheLabel
            // 
            TheLabel.AutoSize = true;
            TheLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TheLabel.Location = new Point(226, 12);
            TheLabel.Name = "TheLabel";
            TheLabel.Size = new Size(192, 21);
            TheLabel.TabIndex = 3;
            TheLabel.Text = "Welcome to BvPro Config!";
            // 
            // ProjectId
            // 
            ProjectId.BackColor = SystemColors.Control;
            ProjectId.Location = new Point(300, 49);
            ProjectId.Name = "ProjectId";
            ProjectId.PlaceholderText = "your project id here";
            ProjectId.Size = new Size(136, 23);
            ProjectId.TabIndex = 4;
            ProjectId.WordWrap = false;
            // 
            // LblSi
            // 
            LblSi.AutoSize = true;
            LblSi.Location = new Point(119, 53);
            LblSi.Name = "LblSi";
            LblSi.Size = new Size(181, 15);
            LblSi.TabIndex = 5;
            LblSi.Text = "https://scratch.mit.edu/projects/";
            // 
            // AddTarget
            // 
            AddTarget.Location = new Point(12, 49);
            AddTarget.Name = "AddTarget";
            AddTarget.Size = new Size(101, 23);
            AddTarget.TabIndex = 6;
            AddTarget.Text = "Add target";
            AddTarget.UseVisualStyleBackColor = true;
            AddTarget.Click += AddTarget_Click;
            // 
            // RemoveTarget
            // 
            RemoveTarget.Enabled = false;
            RemoveTarget.Location = new Point(12, 87);
            RemoveTarget.Name = "RemoveTarget";
            RemoveTarget.Size = new Size(101, 23);
            RemoveTarget.TabIndex = 7;
            RemoveTarget.Text = "Remove target";
            RemoveTarget.UseVisualStyleBackColor = true;
            RemoveTarget.Click += RemoveTarget_Click;
            // 
            // LblSlct
            // 
            LblSlct.AutoSize = true;
            LblSlct.Location = new Point(119, 91);
            LblSlct.Name = "LblSlct";
            LblSlct.Size = new Size(325, 15);
            LblSlct.TabIndex = 8;
            LblSlct.Text = "No target selected. Click the index number to select a target.";
            // 
            // EnableOrDisable
            // 
            EnableOrDisable.Location = new Point(12, 125);
            EnableOrDisable.Name = "EnableOrDisable";
            EnableOrDisable.Size = new Size(101, 23);
            EnableOrDisable.TabIndex = 9;
            EnableOrDisable.Text = "Enable BvPro";
            EnableOrDisable.UseVisualStyleBackColor = true;
            EnableOrDisable.Click += EnableOrDisable_Click;
            // 
            // BvProCfg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(EnableOrDisable);
            Controls.Add(LblSlct);
            Controls.Add(RemoveTarget);
            Controls.Add(AddTarget);
            Controls.Add(LblSi);
            Controls.Add(ProjectId);
            Controls.Add(TheLabel);
            Controls.Add(Discard);
            Controls.Add(Save);
            Controls.Add(TargetList);
            Name = "BvProCfg";
            Text = "BvPro Config";
            FormClosed += Form2_FormClosed;
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView TargetList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button Save;
        private Button Discard;
        private Label TheLabel;
        private TextBox ProjectId;
        private Label LblSi;
        private Button AddTarget;
        private Button RemoveTarget;
        private Label LblSlct;
        private Button EnableOrDisable;
    }
}