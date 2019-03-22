using BetterBusiness.AzureQueueStorage;
using BetterBusiness.AzureQueueStorage.Messages;
using BetterBusiness.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BetterBusiness.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IQueueCommunicator _queueCommunicator;

		public HomeController(IQueueCommunicator queueCommunicator)
		{
			_queueCommunicator = queueCommunicator;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("Send")]
		public async Task<IActionResult> Send()
		{
			await _queueCommunicator.SendAsync
			(
				new SendEmailCommand()
				{
					To = "test@test.com",
					Subject = "Testing Email",
					Body = "Best Body Around"
				}
			);
			return Ok("Sent");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
