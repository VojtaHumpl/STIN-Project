using ChatBotServer;
using ChatBotServer.Commands;
using ChatBotServer.Helpers;
using System.Net.Sockets;






var server = new TCPServer("127.0.0.1", 6969);
Task.Run(() => server.Start());

var cmd = new CommandTime();
var x = cmd.ToString();

while(true) {
	Thread.Sleep(500);
}





