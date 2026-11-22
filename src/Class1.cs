using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Net;
using System.Text;

namespace ezx6tools
{
	public class Svc1
	{
		public Svc1()
		{
		}
		public static async Task Run()
		{
            _ = new Mutex(
				initiallyOwned: true,
				name: "Ezx6t.Tools.Svc1.SingleInstance",
				createdNew: out bool createdNew
			);
            if (!createdNew) return;
			_ = Hear(); // Run the hear task in the background
            await BvPro.Run();
		}
        static async Task Hear()
        {
            while (true)
            {
                using var server = new NamedPipeServerStream(
                    "ezx6t_tools_bvpro_svc1hear",
                    PipeDirection.In
                );
                await server.WaitForConnectionAsync();
                using var reader = new StreamReader(server, Encoding.UTF8);
                string? cmd = await reader.ReadLineAsync();
                if (cmd == "reload") BvPro.Restart();
            }
        }
    }
	public class BvPro
	{
		private static readonly Lock _ctsLock = new();
        private static CancellationTokenSource cts = new();
		private static readonly HttpClient client = new();
		private static readonly string[] ProxiesSources = [
			"https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/http.txt",
			"https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks4.txt",
			"https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks5.txt",
			"https://raw.githubusercontent.com/jetkai/proxy-list/main/online-proxies/txt/proxies.txt",
			"https://raw.githubusercontent.com/monosans/proxy-list/main/proxies/all.txt", //Linus Torvalds
			"https://raw.githubusercontent.com/roosterkid/openproxylist/main/HTTPS_RAW.txt",
			"https://raw.githubusercontent.com/almroot/proxylist/master/list.txt",
			"https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/proxy.txt",
			"https://raw.githubusercontent.com/hookzof/socks5_list/master/proxy.txt",
			"https://raw.githubusercontent.com/clarketm/proxy-list/master/proxy-list-raw.txt",
			"https://raw.githubusercontent.com/proxifly/free-proxy-list/main/proxies/all/data.txt",
			"https://raw.githubusercontent.com/ALIILAPRO/Proxy/main/http.txt",
			"https://raw.githubusercontent.com/ALIILAPRO/Proxy/main/socks4.txt",
			"https://raw.githubusercontent.com/ALIILAPRO/Proxy/main/socks5.txt",
			"https://raw.githubusercontent.com/Zaeem20/FREE_PROXIES_LIST/master/http.txt",
			"https://raw.githubusercontent.com/Zaeem20/FREE_PROXIES_LIST/master/https.txt",
			"https://raw.githubusercontent.com/Zaeem20/FREE_PROXIES_LIST/master/socks4.txt",
			"https://raw.githubusercontent.com/Zaeem20/FREE_PROXIES_LIST/master/socks5.txt",
			"https://raw.githubusercontent.com/vakhov/fresh-proxy-list/master/proxylist.txt",
			"https://raw.githubusercontent.com/r00tee/Proxy-List/main/Https.txt",
			"https://raw.githubusercontent.com/r00tee/Proxy-List/main/Socks4.txt",
			"https://raw.githubusercontent.com/r00tee/Proxy-List/main/Socks5.txt",
			"https://github.com/databay-labs/free-proxy-list/raw/master/http.txt",
			"https://github.com/databay-labs/free-proxy-list/raw/master/socks4.txt",
			"https://github.com/databay-labs/free-proxy-list/raw/master/socks5.txt",
			"https://github.com/elliottophellia/proxylist/raw/master/results/mix_checked.txt",
			"https://github.com/rdavydov/proxy-list/raw/main/proxies/http.txt",
			"https://github.com/rdavydov/proxy-list/raw/main/proxies/socks4.txt",
			"https://github.com/rdavydov/proxy-list/raw/main/proxies/socks5.txt",
			"https://github.com/prxchk/proxy-list/raw/main/all.txt",
			"https://github.com/iplocate/free-proxy-list/raw/refs/heads/main/all-proxies.txt",
			"https://api.proxyscrape.com/v2/?request=displayproxies&protocol=all&timeout=10000&country=all&simplified=true",
		];
		private static readonly string[] NewLines = ["\r\n", "\n"];
		private static readonly int[] GoodCodes = [200, 429];
        private static readonly int[] NextCodes = [200, 404];
        private static readonly StringContent Payload = new("", Encoding.UTF8, "application/json");
		public BvPro()
		{
		}
		public static async Task Run()
		{
			try
			{
				client.Timeout = TimeSpan.FromSeconds(15);
			}
			catch { }
			while (true)
			{
                await Task.Delay(15000);
                try
				{
					CancellationToken ct;
					lock (_ctsLock)
					{
						ct = cts.Token;
					}
                    _ = Refresher(1800000, cts.Token);
					string[] targets = await ReadConfigs();
					if (targets.Length == 0) await Task.Delay(-1, ct);
                    ct.ThrowIfCancellationRequested();
					string[] proxies = await GetProxies(ct);
					HttpClient[] proxiedClients = await TestProxies(proxies, targets, 100, ct);
					await RunProxies(proxiedClients, targets, ct);
				}
				catch { }
				Restart();
			}
		}
		private static async Task<string[]> GetProxies(CancellationToken ct = default)
		{
			int sourcesCount = ProxiesSources.Length;
			Task<string[]>[] workers = new Task<string[]>[sourcesCount];
			for (int i = 0; i < sourcesCount; i++)
			{
				workers[i] = GetProxiesFromSource(ProxiesSources[i], ct);
			}
			HashSet<string> result = [];
			foreach (string[] proxies in await Task.WhenAll(workers))
			{
				result.UnionWith(proxies);
				ct.ThrowIfCancellationRequested();
			}
			if (result.Count == 0) throw new Exception("No proxies found!");
			return [..result];
		}
		private static async Task<string[]> GetProxiesFromSource(string ProxiesSource, CancellationToken ct = default)
		{
			try
			{
				string content = await client.GetStringAsync(ProxiesSource, ct);
				string[] proxies = content
					.Replace("http://", "")
					.Replace("socks4://", "")
					.Replace("socks5://", "")
					.Replace("https://", "")
					.Split(NewLines, StringSplitOptions.RemoveEmptyEntries);
				return proxies;
			}
			catch
			{
				return [];
			}
		}
		private static async Task<HttpClient[]> TestProxies(string[] proxies, string[] targets, int wc = 100, CancellationToken ct = default)
		{
			Lock _PCLock = new();
			List<HttpClient> ProxiedClients = [];
			var queue = new ConcurrentQueue<string>(proxies);
			var tasks = new Task[wc];
			for (int i = 0; i < wc; i++)
			{
				tasks[i] = TestProxyWorker(queue, ProxiedClients, targets[i % targets.Length], _PCLock, ct);
			}
			await Task.WhenAll(tasks);
			return [..ProxiedClients];
		}
		private static async Task TestProxyWorker(ConcurrentQueue<string> queue, List<HttpClient> proxiedClients, string target, Lock _PCLock, CancellationToken ct = default)
		{
			while (queue.TryDequeue(out string? proxy))
			{
				HttpClientHandler? handler = null;
				HttpClient? proxiedClient = null;
				try
				{
					handler = new HttpClientHandler
					{
						Proxy = new WebProxy(proxy),
						UseProxy = true
					};
					proxiedClient = new HttpClient(handler);
					proxiedClient!.Timeout = TimeSpan.FromSeconds(60);
					var res = await proxiedClient!.PostAsync(target, Payload, ct)!;
					int statusCode = (int)res.StatusCode;
					if (!GoodCodes.Contains(statusCode)) throw new Exception("SiMaNiMi");
					lock (_PCLock)
					{
						proxiedClients!.Add(proxiedClient);
					}
				}
				catch
				{
					try
					{
						handler?.Dispose();
						proxiedClient?.Dispose();
					}
					catch { }
				}
				ct.ThrowIfCancellationRequested();
			}
		}
		private static async Task RunProxies(HttpClient[] proxiedClients, string[] targets, CancellationToken ct = default)
		{
			int clientsCount = proxiedClients.Length;
			if (clientsCount == 0) throw new Exception("No live proxy found!");
			var tasks = new Task[clientsCount + 1];
			for (int i = 0; i < clientsCount; i++)
			{
				tasks[i + 1] = RunProxyWorker(i, proxiedClients[i], targets, ct);
			}
			tasks[0] = SendLocally(proxiedClients[0], targets, ct);
			await Task.WhenAll(tasks);
		}
        private static async Task RunProxyWorker(int id, HttpClient client, string[] targets, CancellationToken ct = default)
        {
            int i = id % targets.Length;
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                try
                {
                    var res = await client?.PostAsync(targets[i], Payload, ct)!;
                    int statusCode = (int)res.StatusCode;
                    if (NextCodes.Contains(statusCode)) i++;
                }
                catch { }
                i %= targets.Length;
                await Task.Delay(21000, ct);
            }
        }
        private static async Task SendLocally(HttpClient client, string[] targets, CancellationToken ct = default)
        {
            int i = 0;
            while (true)
            {
                try
                {
                    var res = await client.PostAsync(targets[i], Payload, ct)!;
                    int statusCode = (int)res.StatusCode;
                    if (NextCodes.Contains(statusCode)) i++;
                }
                catch { }
                i %= targets.Length;
                await Task.Delay(21000, ct);
            }
        }
		public static void Restart()
		{
			lock (_ctsLock)
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();
            }
        }
        private static async Task Refresher(int dur, CancellationToken ct = default)
        {
            try
            {
                await Task.Delay(dur, ct);
				Restart();
            }
            catch { }
        }
		private static async Task<string[]> ReadConfigs()
		{
			try
			{
				return [..File.ReadLines(Path.Join(Application.LocalUserAppDataPath, "ezx6tools", "bvpro_config.txt"))];
            }
			catch
			{
				return [];
			}
		}
    }
}
