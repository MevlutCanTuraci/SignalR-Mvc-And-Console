using Microsoft.AspNetCore.SignalR;

namespace SignalR.WebMvc.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected ChatHub: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Disconnected ChatHub: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }



        public void JoinRoom(string userName, string roomName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Clients.Group(roomName).SendAsync("UserJoined", userName);

        }


        public bool LeaveRoom(string userName, string roomName)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            Clients.Group(roomName).SendAsync("UserLeave", userName);

            return true;
        }


        public void SendMessage(string userName, string roomName, string message)
        {
            Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message);
        }


    }
}