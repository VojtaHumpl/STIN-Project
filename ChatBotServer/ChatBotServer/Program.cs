using ChatBotServer;
using ChatBotServer.Commands;
using ChatBotServer.Helpers;
using ChatBotServer.TCPCommunication;
using System.Net.Sockets;






var server = new TCPServer();
var msgHandler = new MessageHandler();
server.OnMessageReceived += Server_OnMessageReceived;


Task.Run(() => server.StartServer("127.0.0.1", 6969));


while (true) {
	Thread.Sleep(500);
}


void Server_OnMessageReceived(object? sender, EventArgs e) {
	var data = (MessageEventArgs)e;
	var message = ProtocolParser.ParsePacket(data.Data);
	var cmd = msgHandler.AnalyzeMessage(message);
	var res = msgHandler.GetResponsePacketToMessage(cmd);
	server.SendData(res);
	Thread.Sleep(100);
	server.RestartConnection();
}


