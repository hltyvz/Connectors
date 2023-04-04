using Newtonsoft.Json;

namespace EventBus.Base.Events
{
	public class IntegrationEvent
	{
		public IntegrationEvent()
		{
			Id = Guid.NewGuid();
			CreatedTime = DateTime.Now;
		}
		[JsonConstructor]
		public IntegrationEvent(Guid id, DateTime createdTime)
		{
			Id = id;
			CreatedTime = createdTime;
		}

		[JsonProperty]
		public Guid Id { get; private set; }
		public DateTime CreatedTime { get; private set; }
	}
}
