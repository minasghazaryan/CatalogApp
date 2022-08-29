using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogShared.SubPub
{
    public class SubPubService : ISubPubService
    {
        private readonly IRabbitClientService _connection;

        public SubPubService
            (
                IRabbitClientService connection
            )
        {
            _connection = connection;
        }

        public IRabbitClientService Connection => _connection;

        public void Publish(RabbitChannelType channel, object message,string appName, CancellationToken cancellationToken = default)
        {
            string jsonData = JsonConvert.SerializeObject(message);
            var channleInfo = new RabbitChannelInfo(channel,appName);
            //_connection.CreateChannel(ExchangeType.Topic, channleInfo);
            try
            {
                var body = Encoding.UTF8.GetBytes(jsonData);
                using (var connectionChannal = _connection.Connection.CreateModel())
                {
                    try
                    {
                        connectionChannal.BasicPublish(exchange: channleInfo.Exchange, routingKey: channleInfo.RoutingKey, basicProperties: null, body: body);
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = $"Message: {jsonData} Channle Info:: {JsonConvert.SerializeObject(channleInfo)}.";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        public Task SubscribeAsync<TMessage>(RabbitChannelType channal,  Func<TMessage, Task> action,string appName, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                var channelInfo = new RabbitChannelInfo(channal, appName);
                var subChannal = _connection.CreateChannel(ExchangeType.Topic, channelInfo);
                var subscriber = new EventingBasicConsumer(subChannal);
                subscriber.Received += async (sender, ev) =>
                {
                    var messageAsString = Encoding.UTF8.GetString(ev.Body.ToArray());
                    var value = JsonConvert.DeserializeObject<TMessage>(messageAsString);
                    await action(value);
                };
                subChannal.BasicConsume(channelInfo.Queue, true, subscriber);
            });
        }

        public Task SubscribeAsync<TMessage>(RabbitChannelType channal, Action<TMessage> action, string appName, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                var channelInfo = new RabbitChannelInfo(channal,appName);
                var subChannal = _connection.CreateChannel(ExchangeType.Topic, channelInfo);
                var subscriber = new EventingBasicConsumer(subChannal);
                subscriber.Received += (senser, ev) =>
                {
                    var messageAsString = Encoding.UTF8.GetString(ev.Body.ToArray());
                    var value = JsonConvert.DeserializeObject<TMessage>(messageAsString);
                    action(value);
                };
                subChannal.BasicConsume(channelInfo.Queue, true, subscriber);
            });
        }
    }
}
