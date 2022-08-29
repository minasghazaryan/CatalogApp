using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogShared.SubPub
{
    public interface ISubPubService
    {
        IRabbitClientService Connection { get; }
        void Publish(RabbitChannelType channel, object message, string appName, CancellationToken cancellationToken = default);
        Task SubscribeAsync<TMessage>(RabbitChannelType channal,  Func<TMessage, Task> action,string appName, CancellationToken cancellationToken = default);
        Task SubscribeAsync<TMessage>(RabbitChannelType channal,  Action<TMessage> action, string appName, CancellationToken cancellationToken = default);
    }
}
