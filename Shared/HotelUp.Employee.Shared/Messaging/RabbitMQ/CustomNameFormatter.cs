using MassTransit;

namespace HotelUp.Employee.Shared.Messaging.RabbitMQ;

public class CustomNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return $"HotelUp.Employee:{typeof(T).Name}";
    }
}