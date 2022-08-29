using RabbitMQ.Client;
using System;

namespace CatalogShared.SubPub
{
    public interface IRabbitClientService : IDisposable
    {
        IConnection Connection { get; }
        IModel CreateChannel(string exchangeType, RabbitChannelInfo info);
    }
}
