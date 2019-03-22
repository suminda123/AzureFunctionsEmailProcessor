using BetterBusiness.AzureFunctions.Infrastructure;
using BetterBusiness.AzureQueueStorage;
using BetterBusiness.AzureQueueStorage.Messages;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BetterBusiness.AzureFunctions.Email
{
	public static class EmailQueueTrigger
	{
		public static readonly IServiceProvider Container = new ContainerBuilder().Build();

		[FunctionName("EmailQueueTrigger")]
		public static async Task Run(
			[QueueTrigger(QueueRoutes.EmailBox, Connection = "AzureWebJobsStorage")]
			string message,
			ILogger log)
		{
			try
			{
				var queueCommunicator = Container.GetService<IQueueCommunicator>();
				var command = queueCommunicator.Read<SendEmailCommand>(message);

				var sendEmailCommandHandler = Container.GetService<ISendEmailCommandHandler>();
				await sendEmailCommandHandler.Handle(command);
			}
			catch (Exception ex)
			{
				log.LogError(ex, $"Something went wrong with EmailQueueTrigger {ex.Message}");
				throw;
			}
		}
	}
}
