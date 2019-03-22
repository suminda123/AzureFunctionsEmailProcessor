using Newtonsoft.Json;

namespace BetterBusiness.AzureQueueStorage.Messages
{
	public abstract class BaseQueueMessage
	{
		[JsonIgnore]
		public string Route { get; set; }

		public BaseQueueMessage(string route)
		{
			Route = route;
		}
	}
}
