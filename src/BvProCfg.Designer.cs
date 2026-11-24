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
            ListViewItem listViewItem1 = new ListViewItem("aas");
            listView1 = new ListView();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Dock = DockStyle.Bottom;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem1 });
            listView1.Location = new Point(0, 91);
            listView1.Name = "listView1";
            listView1.Size = new Size(800, 359);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.Columns.Add("Index", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Target", -2, HorizontalAlignment.Left);
            listView1.View = View.Details;
            // 
            // BvProCfg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listView1);
            Name = "BvProCfg";
            Text = "BvPro Config";
            FormClosed += Form2_FormClosed;
            Load += Form2_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
    }
}