// See https://aka.ms/new-console-template for more information

using Client;
using Messages;
using Messages.Base;
using Messages.Base.Client;
using Messages.Base.Server;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

CommunicationModule communicationModule = new KafkaCommunicationModule();
await communicationModule.ConnectAsync(CancellationToken.None);
await communicationModule.SendMessageAsync(new NormalizeTableMessage()
{
    TableName = "RentApartment"
}, CancellationToken.None);
var answer = await communicationModule.GetMessageAsync(CancellationToken.None);

var msgDispatcher = new MessagesDispatcher(); 
msgDispatcher.DispatchMessage(answer);

//начнём передачу сообщений до конца потока записей
await communicationModule.SendMessageAsync(new StartStreamMessage(),CancellationToken.None);
Message msg = await communicationModule.GetMessageAsync(CancellationToken.None);
while (msg is not EndStreamMessage)
{
    msgDispatcher.DispatchMessage(msg);
    await communicationModule.SendMessageAsync(new ContinueStreamMessage(), CancellationToken.None);
    msg =  await communicationModule.GetMessageAsync(CancellationToken.None);
}



    
await communicationModule.DisconnectAsync(CancellationToken.None);