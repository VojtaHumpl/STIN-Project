using ChatBotServer;
using ChatBotServer.Commands;
using ChatBotServer.Helpers;
using System.Net.Sockets;



var server = new TCPServer("127.0.0.1", 6969);
Task.Run(() => server.Start());


var cmd = CommandFactory.Create(typeof(CommandName));


var x = new CommandEurToday();
Console.WriteLine(x.ToString());

while(true) {
	Thread.Sleep(500);
}





