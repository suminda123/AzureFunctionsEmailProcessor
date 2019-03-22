using System;

namespace BetterBusiness.AzureQueueStorage
{
	public class QueueConfig
	{
		public string QueueConnectionString { get; set; }

		public QueueConfig(string queueConnectionString)
		{
			if (string.IsNullOrEmpty(queueConnectionString))
				throw new ArgumentException("QueueConfig.QueueConnectionString cannot be null or empty");
			QueueConnectionString = queueConnectionString;
		}
	}
}
