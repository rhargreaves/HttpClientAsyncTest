using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace HttpClientAsyncTest
{
	public class Program
	{
		private static readonly HttpClient HttpClient = new HttpClient();

		public static void Main(string[] args)
		{
			StartWebServer();
			MakeRequests().Wait();

			Console.ReadKey();
		}

		private static async Task MakeRequests()
		{
			var tasks = Enumerable.Range(0, 1000).Select(MakeRequest).ToArray();
			await Task.WhenAll(tasks);
		}

		private static async Task MakeRequest(int requestNo)
		{
			using (var response = await HttpClient.SendAsync(
				new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/")))
			{
				var body = await response.Content.ReadAsStringAsync();

				Console.WriteLine("Response {2}: {0} {1}",
					(int) response.StatusCode,
					body, requestNo);
			}
		}

		private static void StartWebServer()
		{
			BuildWebHost().Start();
		}

		private static IWebHost BuildWebHost()
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseStartup<Startup>()
				.Build();
			return host;
		}

	}
}