using ChatBotServer;
using System.Net.Sockets;




var server = new TCPServer("127.0.0.1", 6969);
Task.Run(() => server.Start());










