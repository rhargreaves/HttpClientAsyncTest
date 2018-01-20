using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HttpClientAsyncTest
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.Run(DelayAndWrite);
		}

		private static async Task DelayAndWrite(HttpContext context)
		{
			Console.WriteLine("Received Request And Delaying...");
			await Task.Delay(1000);
			await context.Response.WriteAsync("Hello world");
		}
	}
}