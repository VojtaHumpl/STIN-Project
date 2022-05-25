using ChatBotServer;
using ChatBotServer.Commands;
using ChatBotServer.Helpers;
using ChatBotServer.TCPCommunication;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;



namespace ChatBotServer {
	[ExcludeFromCodeCoverage]
	internal class Program {
		static void Main(string[] args) {
			// init history
			CommandHelpers.UpdateHistory();

			//var cmd = new CommandEurHistory().ToString();

			var server = new TCPServer();
			var msgHandler = new MessageHandler();
			server.OnMessageReceived += Server_OnMessageReceived;	

			Task.Run(() => server.StartServer(6969));


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
		}
	}
}


