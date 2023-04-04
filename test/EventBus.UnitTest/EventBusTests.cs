using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.UnitTest.Events.Events;
using EventBus.UnitTest.Events.EventsHandler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EventBus.UnitTest
{
	[TestClass]
	public class EventBusTests
	{
		private ServiceCollection services;

		public EventBusTests()
		{
			services = new ServiceCollection();
			services.AddLogging(configure => configure.AddConsole());
		}

		[TestMethod]
		public void subscribe_event_on_rabbitmq_test()
		{
			services.AddSingleton<IEventBus>(sp =>
			{
				return EventBusFactory.Create(GetRabbitMQconfig(), sp);
			});

			var sp = services.BuildServiceProvider();
			var eventBus = sp.GetRequiredService<IEventBus>();

			eventBus.Subscribe<LogIntegrationEvent, LogCreatedIntegrationEventHandler>();
			//eventBus.UnSubscribe<LogIntegrationEvent, LogCreatedIntegrationEventHandler>();
		}

		[TestMethod]
		public void send_message_to_rabbitmq()
		{
			services.AddSingleton<IEventBus>(sp =>
			{
				return EventBusFactory.Create(GetRabbitMQconfig(), sp);
			});

			var sp = services.BuildServiceProvider();
			var eventBus = sp.GetRequiredService<IEventBus>();

			eventBus.Publish(new LogIntegrationEvent("unit_test", "test_action", "selam dünyalý sesim geliyor mu?"));
		}

		private EventBusConfig GetRabbitMQconfig()
		{
			return new EventBusConfig()
			{
				ConnectionRetryCount = 5,
				SubscriberClientAppName = "EventBus.Test",
				DefaultTopicName = "BIK.EventTopicName",
				EventBusType = EventBusType.RabbitMQ,
				EventNameSuffix = "IntegrationEvent",
				Connection = new ConnectionFactory()
				{
					HostName = "",
					Port = 5672,
					UserName = "",
					Password = ""
				}
			};
		}
	}
}