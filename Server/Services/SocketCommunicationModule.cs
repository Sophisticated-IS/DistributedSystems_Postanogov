using System.Net;
using System.Net.Sockets;
using Messages;
using Messages.Base;

namespace Server;

public sealed class SocketCommunicationModule : CommunicationModule
{
    private Socket? _listenSocket;
    private Socket _clientConnectionSocket;

    public override async Task ConnectAsync(CancellationToken cancellationToken)
    {
        var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 33333);
        _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _listenSocket.Bind(ipPoint);
        _listenSocket.Listen(1);
        _clientConnectionSocket =  _listenSocket.Accept();
    }

    public override async Task<Message> GetMessageAsync(CancellationToken cancellationToken)
    {
        if (_listenSocket is null) throw new NotSupportedException("Connect to Socket first!");
        if(_clientConnectionSocket.Connected) throw new NotSupportedException("Connect to Socket first!");
        //todo socket was closed by client 
        
        var bytesMessage = new List<byte>(1024);
        var buffer = new byte[256];

        do
        {
            var amountBytes = _clientConnectionSocket.Receive(buffer);
            var availableBytes = buffer.Take(amountBytes);
            bytesMessage.AddRange(availableBytes);
        }
        while (_clientConnectionSocket.Available > 0);

        return Serializer.DeSerialize(bytesMessage.ToArray());
    }

    public override async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        if (_clientConnectionSocket is null) throw new NotSupportedException("GetMessage from Client first!");

        var bytesMsg = Serializer.Serialize(message);

        await _clientConnectionSocket.SendAsync(bytesMsg, SocketFlags.None, cancellationToken);
    }

    public override async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        if (_listenSocket is null) return;

        await _listenSocket.DisconnectAsync(false, cancellationToken);
    }
}