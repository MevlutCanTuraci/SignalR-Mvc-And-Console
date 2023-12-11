// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalR.ConsoleClient;

bool writedServerMessage = true;
Console.WriteLine("Connecting to server...");


var connection = new HubConnectionBuilder()
.WithUrl("https://localhost:7194/ClientHub")
.AddMessagePackProtocol()
.Build();


await connection.StartAsync().ContinueWith(op =>
{
    if (!op.IsFaulted)
    {
        Console.Clear();
        Console.WriteLine($"Connected to server. Connection State -> {connection.State}");
    }

    else
    {
        Console.WriteLine($"Connection error! : {op.IsFaulted}\r\n{op.Exception}");
    }
});


connection.On<string>("ReceiveMessage", async (serverMessage) =>
{
    Console.WriteLine("\r\nServer say: " + serverMessage);
    writedServerMessage = true;
});


connection.On<ResponseMessageModel>("ReceiveMessageObject", async (serverMessage) =>
{
    Console.WriteLine($"\r\nServer say: Message: {serverMessage.Message} - Date: {serverMessage.Date} ");
    writedServerMessage = true;
});


connection.Closed += async (Exception ex) =>
{
    Console.WriteLine($"Sunucu bağlantıyı kapattı. Hata: {ex.Message}");
};



while (true)
{
    string? clientMessage = string.Empty;

    if (writedServerMessage)
    {
        Console.WriteLine();

        Console.WriteLine("Client Message: ");
        clientMessage = Console.ReadLine();
    }    

    if (!string.IsNullOrEmpty(clientMessage) || !string.IsNullOrWhiteSpace(clientMessage))
    {
        await connection.SendAsync("Send-Message-Response-Object-Request-Object", new RequestMessageModel
        {
            Message = clientMessage
            //Message = "Client object sended message"
        });

        //await connection.SendAsync("Send-Message-Response-Object", clientMessage);
        //await connection.SendAsync("Send-Message", clientMessage);
        writedServerMessage = false;
    }
    else
    {
        continue;
    }

}



