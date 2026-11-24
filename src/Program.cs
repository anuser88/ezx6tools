using System.Diagnostics;

namespace ezx6tools
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                _ = Checks(); // Run the checks in the background
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new MainForm());
            }
            else
            {
                if (args[0] == "svc1")
                {
                    await Svc1.Run();
                }
            }
        }
        static async Task Checks()
        {
            bool svc1TaskNotExists;
            var psi = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = "/query /tn ezx6tools-svc1",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process? process = Process.Start(psi))
            {
                await process?.WaitForExitAsync()!;
                svc1TaskNotExists = process?.ExitCode == 1;
            }
            if (svc1TaskNotExists)
            {
                psi = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = $"/create /tn ezx6tools-svc1 /tr \"{Environment.ProcessPath} svc1\" /sc onlogon /f",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas"
                };
                Process.Start(psi);
            }
            Directory.CreateDirectory(Path.Join(Environment.SpecialFolder.LocalApplicationData.ToString(), "ezx6tools"));
            psi = new ProcessStartInfo
            {
                FileName = Environment.ProcessPath,
                Arguments = "svc1",
                UseShellExecute = true
            };
            //Process.Start(psi);
        }
    }
}