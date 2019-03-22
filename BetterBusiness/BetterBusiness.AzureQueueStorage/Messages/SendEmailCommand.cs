namespace BetterBusiness.AzureQueueStorage.Messages
{
	public class SendEmailCommand : BaseQueueMessage
	{
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }

		public SendEmailCommand()
			: base(QueueRoutes.EmailBox)
		{
		}
	}
}
