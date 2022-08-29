
namespace CatalogShared.SubPub
{
    public class RabbitChannelInfo
    {
        public RabbitChannelInfo( RabbitChannelType channel,string appName)
        {
            Exchange = $"{channel}-Exchange";
            Queue = $"{appName}-{channel}-Queue";
            RoutingKey = $"{channel}";
        }

        public string Exchange { get; }
        public string Queue { get; }
        public string RoutingKey { get; }
    }
}
