using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ezx6tools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static readonly VersionInfo verInfo = GetVersion();

        private static readonly HttpClient client = new();

        private async void Form1_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = $"v{verInfo.Version}";
            try
            {
                var latestVer = JsonSerializer.Deserialize<VersionInfo>(await client.GetStringAsync("https://github.com/anuser88/ezx6tools/releases/latest/download/version.json"));
                if (latestVer?.Version != verInfo.Version)
                {
                    UpdateBtn.Enabled = true;
                    UpdateStat.Text = $"Update available: v{latestVer?.Version}";
                }
                else
                {
                    UpdateStat.Text = "The program is up to date.";
                }
            }
            catch { }
        }

        private void BvProCfgBtn_Click(object sender, EventArgs e)
        {
            BvProCfg.OpenCfg();
        }
        public static VersionInfo GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("version.json")!;
            using StreamReader reader = new(stream!);
            string json = reader.ReadToEnd();
            var versionInfo = JsonSerializer.Deserialize<VersionInfo>(json);
            return versionInfo ?? new VersionInfo();
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

        }
    }
    public class VersionInfo
    {
        [JsonPropertyName("version")]
        public string? Version { get; set; }
    }
}
