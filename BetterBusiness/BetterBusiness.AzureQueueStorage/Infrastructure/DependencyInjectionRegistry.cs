using BetterBusiness.AzureQueueStorage.MessageSerializer;
using Microsoft.Extensions.DependencyInjection;

namespace BetterBusiness.AzureQueueStorage.Infrastructure
{
	public static class DependencyInjectionRegistry
	{
		public static IServiceCollection AddAzureQueueStorage(this IServiceCollection services, QueueConfig queueConfig)
		{
			services.AddSingleton(queueConfig);
			services.AddSingleton<IMessageSerializer, JsonMessageSerializer>();
			services.AddSingleton<ICloudQueueClientFactory, CloudQueueClientFactory>();
			services.AddTransient<IQueueCommunicator, QueueCommunicator>();
			return services;
		}
	}
}
