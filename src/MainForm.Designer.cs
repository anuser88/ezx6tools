namespace ezx6tools
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            BvProCfgBtn = new Button();
            VersionLabel = new Label();
            UpdateBtn = new Button();
            UpdateStat = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(335, 22);
            label1.Name = "label1";
            label1.Size = new Size(107, 30);
            label1.TabIndex = 0;
            label1.Text = "ezx6tools";
            // 
            // BvProCfgBtn
            // 
            BvProCfgBtn.Location = new Point(335, 136);
            BvProCfgBtn.Name = "BvProCfgBtn";
            BvProCfgBtn.Size = new Size(104, 23);
            BvProCfgBtn.TabIndex = 1;
            BvProCfgBtn.Text = "BvPro Config";
            BvProCfgBtn.UseVisualStyleBackColor = true;
            BvProCfgBtn.Click += BvProCfgBtn_Click;
            // 
            // VersionLabel
            // 
            VersionLabel.Location = new Point(401, 543);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.RightToLeft = RightToLeft.No;
            VersionLabel.Size = new Size(380, 15);
            VersionLabel.TabIndex = 2;
            VersionLabel.TextAlign = ContentAlignment.MiddleRight;
            VersionLabel.Click += VersionLabel_Click;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Enabled = false;
            UpdateBtn.Location = new Point(12, 526);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(75, 23);
            UpdateBtn.TabIndex = 3;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            UpdateBtn.Click += UpdateBtn_Click;
            // 
            // UpdateStat
            // 
            UpdateStat.AutoSize = true;
            UpdateStat.Location = new Point(93, 530);
            UpdateStat.Name = "UpdateStat";
            UpdateStat.Size = new Size(0, 15);
            UpdateStat.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(784, 561);
            Controls.Add(UpdateStat);
            Controls.Add(UpdateBtn);
            Controls.Add(VersionLabel);
            Controls.Add(BvProCfgBtn);
            Controls.Add(label1);
            MaximumSize = new Size(800, 600);
            MinimumSize = new Size(800, 600);
            Name = "MainForm";
            Text = "ezx6tools";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button BvProCfgBtn;
        private Label VersionLabel;
        private Button UpdateBtn;
        private Label UpdateStat;
    }
}
