using Microsoft.AspNetCore.SignalR;
using SignalR.WebMvc.Models;

namespace SignalR.WebMvc.Hubs
{
    public class ClientHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected ClientHub: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Disconnected ClientHub: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }


        [HubMethodName("Send-Message")]
        public void SendMessage(string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Hello SignalR!");
        }


        [HubMethodName("Send-Message-Response-Object")]
        public void SendMessageResponseObject(string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessageObject", new ResponseMessageModel
            {
                Message = "Hello SignalR!",
                Date    = DateTime.Now
            });
        }


        [HubMethodName("Send-Message-Response-Object-Request-Object")]
        public void SendMessageResponseObjectRequestObject(RequestMessageModel requestMessage)
        {
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessageObject", new ResponseMessageModel
            {
                Message = $"Hello SignalR! - \r\n\t'Client Message: {requestMessage.Message}'\r\n\t",
                Date = DateTime.Now
            });
        }

    }
}