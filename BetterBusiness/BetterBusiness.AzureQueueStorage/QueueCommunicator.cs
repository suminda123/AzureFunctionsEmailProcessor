using BetterBusiness.AzureQueueStorage.Messages;
using BetterBusiness.AzureQueueStorage.MessageSerializer;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;

namespace BetterBusiness.AzureQueueStorage
{
	public interface IQueueCommunicator
	{
		T Read<T>(string message);
		Task SendAsync<T>(T obj) where T : BaseQueueMessage;
	}

	public class QueueCommunicator : IQueueCommunicator
	{
		private readonly IMessageSerializer _messageSerializer;
		private readonly ICloudQueueClientFactory _cloudQueueClientFactory;

		public QueueCommunicator(
			IMessageSerializer messageSerializer,
			ICloudQueueClientFactory cloudQueueClientFactory)
		{
			_messageSerializer = messageSerializer;
			_cloudQueueClientFactory = cloudQueueClientFactory;
		}

		public T Read<T>(string message)
		{
			return _messageSerializer.Deserialize<T>(message);
		}

		public async Task SendAsync<T>(T obj) where T : BaseQueueMessage
		{
			var queueReference = _cloudQueueClientFactory.GetClient().GetQueueReference(obj.Route);
			await queueReference.CreateIfNotExistsAsync();

			var serialize = _messageSerializer.Serialize(obj);
			var queueMessage = new CloudQueueMessage(serialize);
			await queueReference.AddMessageAsync(queueMessage);
		}
	}
}
