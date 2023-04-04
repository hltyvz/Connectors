﻿using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;

namespace EventBus.Factory
{
	public class EventBusFactory
	{
		public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
		{
			switch (config.EventBusType)
			{
				case EventBusType.RabbitMQ: return new EventBusRabbitMQ(config, serviceProvider);
				default: return new EventBusRabbitMQ(config, serviceProvider);
			}
		}
	}
}
