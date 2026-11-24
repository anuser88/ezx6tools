using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace ezx6tools
{
    public partial class BvProCfg : Form
    {
        private static bool opened = false;
        private static readonly HttpClient client = new();
        private static BvProConfigFormat config = BvPro.ReadConfigs();
        private static int select = -1;
        public BvProCfg()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RenderTargets();
            EnableOrDisable.Text = config.Activated ? "Disable BvPro" : "Enable BvPro";
        }
        public static void OpenCfg()
        {
            if (opened) return;
            BvProCfg bvProCfg = new();
            bvProCfg.Show();
            opened = true;
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            opened = false;
        }

        private void WriteConfigs()
        {
            try
            {
                File.WriteAllText(Path.Join(Environment.SpecialFolder.LocalApplicationData.ToString(), "ezx6tools", "bvprocfg.json"), JsonSerializer.Serialize(config));
                Save.Enabled = false;
                Discard.Enabled = false;
                TheLabel.Text = "Config saved successfully.";
                try
                {
                    using var client = new NamedPipeClientStream(
                        ".",
                        "ezx6t_tools_svc1_bvprohear",
                        PipeDirection.Out
                    );
                    client.Connect(3000);
                    using var writer = new StreamWriter(client, Encoding.UTF8)
                    {
                        AutoFlush = true
                    };
                    writer.WriteLine("reload");
                }
                catch { }
            }
            catch (Exception e)
            {
                TheLabel.Text = $"Error: {e.Message}";
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            WriteConfigs();
        }

        private void Discard_Click(object sender, EventArgs e)
        {
            config = BvPro.ReadConfigs();
            Save.Enabled = false;
            Discard.Enabled = false;
            TheLabel.Text = "Changes discarded.";
        }
        private void RenderTargets()
        {
            TargetList.Items.Clear();
            if (config.Targets == null) return;
            for (int i = 0; i < config.Targets.Length; i++)
            {
                ListViewItem item = new((i + 1).ToString());
                item.SubItems.Add(config.Targets[i]);
                TargetList.Items.Add(item);

            }
        }

        private void TargetList_Click(object sender, EventArgs e)
        {
            if (TargetList.SelectedItems.Count == 0) return;
            ListViewItem sel = TargetList.SelectedItems[0];
            select = int.Parse(sel.Text);
            LblSlct.Text = $"Selected target: {select}";
            RemoveTarget.Enabled = true;
        }

        private void EnableOrDisable_Click(object sender, EventArgs e)
        {
            config.Activated = !config.Activated;
            EnableOrDisable.Text = config.Activated ? "Disable BvPro" : "Enable BvPro";
            Save.Enabled = true;
            Discard.Enabled = true;
            TheLabel.Text = config.Activated ? "BvPro enabled." : "BvPro disabled.";
        }

        private void RemoveTarget_Click(object sender, EventArgs e)
        {
            config.Targets = config?.Targets?.Where((_, i) => i != select).ToArray();
            RenderTargets();
            Save.Enabled = true;
            Discard.Enabled = true;
            TheLabel.Text = "Target removed.";
            select = -1;
            LblSlct.Text = "No target selected.";
        }

        private async void AddTarget_Click(object sender, EventArgs e)
        {
            AddTarget.Enabled = false;
            try
            {
                if (ProjectId.Text != "")
                {
                    JsonDocument projectData = JsonDocument.Parse(await client.GetStringAsync($"https://api.scratch.mit.edu/projects/{ProjectId.Text}"));
                    JsonElement author = projectData.RootElement.GetProperty("author");
                    string projectAuthorUsername = author.GetProperty("username").GetString()!;
                    config.Targets = config?.Targets?.Concat([$"https://api.scratch.mit.edu/users/{projectAuthorUsername}/projects/{ProjectId.Text}/views"]).ToArray();
                    RenderTargets();
                    Save.Enabled = true;
                    Discard.Enabled = true;
                    TheLabel.Text = "Target added.";
                    ProjectId.Clear();
                }
            }
            catch
            {
                TheLabel.Text = "Error: Invalid project ID.";
            }
            AddTarget.Enabled = true;
        }
    }
}
