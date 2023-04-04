using EventBus.Base.Events;

namespace EventBus.UnitTest.Events.Events
{
	public class LogIntegrationEvent : IntegrationEvent
	{
		public LogIntegrationEvent(string projectName, string actionName, string message)
		{
			ProjectName = projectName;
			ActionName = actionName;
			Message = message;
		}

		public string ProjectName { get; set; }
		public string ActionName { get; set; }
		public string Message { get; set; }
	}
}
