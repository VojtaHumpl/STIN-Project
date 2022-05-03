using ChatBotServer;
using ChatBotServer.Commands;
using ChatBotServer.Helpers;
using System.Net.Sockets;



var server = new TCPServer("127.0.0.1", 6969);
Task.Run(() => server.Start());


var cmd = CommandFactory.Create(typeof(CommandName));


var x = Reflection.ReflectiveEnumerator.GetEnumerableOfType<Command>();

var xx = new CommandEurToday();
var asads = xx.ToString();



while(true) {
	//Console.WriteLine("");
	var y = new CommandTime();
	var tmp = y.ToString();
	Thread.Sleep(500);
}





