using BetterBusiness.AzureQueueStorage.Infrastructure;
using Newtonsoft.Json;

namespace BetterBusiness.AzureQueueStorage.MessageSerializer
{
	public class JsonMessageSerializer : IMessageSerializer
	{
		public T Deserialize<T>(string message)
		{
			var obj = JsonConvert.DeserializeObject<T>(message.ToDecompressed());
			return obj;
		}

		public string Serialize(object obj)
		{
			var message = JsonConvert.SerializeObject(obj);
			return message.ToCompressed();
		}
	}
}
