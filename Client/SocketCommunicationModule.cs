using System.Net;
using System.Net.Sockets;
using Messages;
using Messages.Base;

namespace Client;

public sealed class SocketCommunicationModule : CommunicationModule
{
    private NetworkStream? _clientConnection;

    public override async Task ConnectAsync(CancellationToken cancellationToken)
    {
        var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 33333);
        var tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(ipPoint, cancellationToken);
        _clientConnection = tcpClient.GetStream();
    }

    public override async Task<Message> GetMessageAsync(CancellationToken cancellationToken)
    {
        if (_clientConnection is null) throw new NotSupportedException("Connect to Socket first!");
        
        while (!_clientConnection.DataAvailable)
        {
            await Task.Delay(1, cancellationToken);
        }
        
        var bytesMessage = new List<byte>(1024);
        var buffer = new byte[256];

        do
        {
            var amountBytes = await _clientConnection.ReadAsync(buffer, cancellationToken);
            var availableBytes = buffer.Take(amountBytes);
            bytesMessage.AddRange(availableBytes);
        }
        while (_clientConnection.Socket.Available > 0);


        return Serializer.DeSerialize(bytesMessage.ToArray());
    }

    public override async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        if (_clientConnection is null) throw new NotSupportedException("GetMessage from Server first!");

        var bytesMsg = Serializer.Serialize(message);

        await _clientConnection.WriteAsync(bytesMsg, cancellationToken);
    }

    public override async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        if (_clientConnection is null) return;

        _clientConnection.Close();
        _clientConnection.Socket.Close();
    }
}