using MessagePack;

namespace SignalR.WebMvc.Infrastructer
{
    public static class ServiceExtensions
    {
        public static void ConfigureSignalR(this IServiceCollection service)
        {
            service.AddSignalR().AddMessagePackProtocol();
        }
    }
}