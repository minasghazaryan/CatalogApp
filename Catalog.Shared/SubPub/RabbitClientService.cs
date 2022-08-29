using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace CatalogShared.SubPub
{

    public class RabbitClientService : IRabbitClientService
        {
            private const int MAX_RETRY_COUNT = 10;
            private IConnection _connection;
            private bool disposed = false;

            public IConnection Connection => _connection;

            public RabbitClientService()
            {
                Connect();
            }

            private void Connect()
            {
                int retryCount = 0;
                while (true)
                {
                    try
                    {
                        ConnectionFactory factory = new ConnectionFactory
                        {
                            RequestedHeartbeat = TimeSpan.FromSeconds(30),
                            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                        };
                        _connection = factory.CreateConnection();
                        _connection.ConnectionShutdown += ConnectionShutdownCallBack;
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (retryCount < MAX_RETRY_COUNT)
                        {
                            ++retryCount;
                            Task.Delay(1000).Wait();
                            continue;
                        }
                    }
                }
            }

            private void ConnectionShutdownCallBack(object sender, ShutdownEventArgs e)
            {
                _connection = null;
                Connect();
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    _connection?.Close();
                    _connection?.Dispose();
                }

                disposed = true;
            }

            public IModel CreateChannel(string exchangeType, RabbitChannelInfo info)
            {
                IModel channel = _connection.CreateModel();
                channel.QueueDeclare(info.Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                channel.QueueBind(info.Queue, info.Exchange, info.RoutingKey);
                channel.BasicQos(0, 10, true);
                return channel;
            }
        }
}
