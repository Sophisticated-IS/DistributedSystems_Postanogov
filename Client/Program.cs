// See https://aka.ms/new-console-template for more information

using Client;
using Messages.Base;
using Messages.Base.Client;
using Messages.Base.Server;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var socketCM = new SocketCommunicationModule();
await socketCM.ConnectAsync(CancellationToken.None);
await socketCM.SendMessageAsync(new NormalizeTableMessage()
{
    TableName = "RentApartment"
}, CancellationToken.None);
var answer = await socketCM.GetMessageAsync(CancellationToken.None);

var msgDispatcher = new MessagesDispatcher(); 
msgDispatcher.DispatchMessage(answer);

//начнём передачу сообщений до конца потока записей
await socketCM.SendMessageAsync(new StartStreamMessage(),CancellationToken.None);
Message msg; 
do
{
    msg = await socketCM.GetMessageAsync(CancellationToken.None);
    msgDispatcher.DispatchMessage(msg);
    await socketCM.SendMessageAsync(new ContinueStreamMessage(), CancellationToken.None);
}
while (msg is not EndStreamMessage);


    
await socketCM.DisconnectAsync(CancellationToken.None);