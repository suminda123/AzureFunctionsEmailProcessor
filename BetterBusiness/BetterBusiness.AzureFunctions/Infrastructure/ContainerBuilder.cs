using BetterBusiness.AzureFunctions.Email;
using BetterBusiness.AzureQueueStorage;
using BetterBusiness.AzureQueueStorage.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BetterBusiness.AzureFunctions.Infrastructure
{
	public class ContainerBuilder
	{
		private readonly IServiceCollection _services;
		private readonly IConfigurationRoot _configuration;

		public ContainerBuilder()
		{
			_services = new ServiceCollection();
			_configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();
		}

		public IServiceProvider Build()
		{
			_services.AddSingleton
			(
				new EmailConfig
				(
					_configuration.GetValue<string>("EmailHost"),
					_configuration.GetValue<int>("EmailPort"),
					_configuration.GetValue<string>("EmailSender"),
					_configuration.GetValue<string>("EmailPassword")
				)
			);

			_services.AddTransient<ISendEmailCommandHandler, SendEmailCommandHandler>();
			_services.AddAzureQueueStorage(new QueueConfig(_configuration["AzureWebJobsStorage"]));
			return _services.BuildServiceProvider();
		}
	}
}
