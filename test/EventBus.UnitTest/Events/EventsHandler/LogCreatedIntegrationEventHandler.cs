using EventBus.Base.Abstraction;
using EventBus.UnitTest.Events.Events;

namespace EventBus.UnitTest.Events.EventsHandler
{
	public class LogCreatedIntegrationEventHandler : IIntegrationEventHandler<LogIntegrationEvent>
	{
		public Task Handle(LogIntegrationEvent @event)
		{
			return Task.CompletedTask;
		}
	}
}
