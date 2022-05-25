using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.TCPCommunication {
	internal class TCPServer : ITCPServer {

		private TcpListener? Server { get; set; } = null;
		private TcpClient? Client { get; set; } = null;
		private int ServerPort { get; set; }
		private IPAddress ServerIP { get; set; } = IPAddress.Any;
		internal byte[] ReadBuffer { get; private set; } = new byte[1024];
		internal bool ReceiverRunning { get; private set; }
		internal bool ServerConnected { get; private set; }
		internal bool ClientConnected { get; private set; }

		internal event EventHandler<EventArgs>? OnMessageReceived;

		public TCPServer() { }
		public TCPServer(int bufferSize = 1024) {
			ReadBuffer = new byte[bufferSize];
		}

		internal bool StartServer(int port) {
			return StartServer("", port);
		}

		public bool StartServer(string ip, int port) {
			//if (ClientRunning)
			//return false;

			if (ip == "")
				ServerIP = IPAddress.Any;
			else
				ServerIP = IPAddress.Parse(ip);

			ServerPort = port;
			Server = new(ServerIP, ServerPort);


			try {
				Server.Start();
				ReceiverRunning = true;
				Console.WriteLine("Waiting for connection...");
				Client = Server.AcceptTcpClient();
				ServerConnected = true;
				Console.WriteLine("Connected");

				Task.Run(() => Receive());
			} catch (Exception e) {
				Debug.WriteLine($"Error: {e}");
				StopServerConnection();
			}

			return ServerConnected;
		}

		internal bool RestartConnection() {
			StopServerConnection();
			try {
				Server = new(ServerIP, ServerPort);
				Client = new TcpClient();
				Server.Start();
				ReceiverRunning = true;
				Console.WriteLine("Waiting for connection...");
				Client = Server.AcceptTcpClient();
				ServerConnected = true;
				Console.WriteLine("Connected");

				//Task.Run(() => Receive());
				Receive();
			} catch (Exception e) {
				Console.WriteLine($"Error: {e}");
				StopServerConnection();
			}

			return ServerConnected;
		}

		private async void Receive() {
			while (ReceiverRunning) {
				try {
					var stream = Client.GetStream();
					int length;
					while ((length = stream.Read(ReadBuffer, 0, ReadBuffer.Length)) != 0 && ReceiverRunning) {
						var readData = new byte[length];
						Array.Copy(ReadBuffer, readData, length);
						//Console.WriteLine($"Received {Encoding.UTF8.GetString(readData)}");
						OnMessageReceived?.Invoke(this, new MessageEventArgs(readData));
						break;
					}
				} catch (Exception e) {
					Debug.WriteLine($"Error: {e}");
				} finally {
					Console.WriteLine("Disconnected");
					//RestartConnection();
				}
			}
		}

		public void SendData(byte[] data) {
			var stream = Client.GetStream();
			stream.Write(data, 0, data.Length);
		}

		internal void StopServerConnection() {
			ServerConnected = false;
			ReceiverRunning = false;
			Client?.Close();
			Server?.Stop();
		}

		public void Stop() {
			ServerConnected = false;
			ReceiverRunning = false;
			ClientConnected = false;
			Client?.Close();
			Server?.Stop();
		}

		internal void StopClient() {
			ClientConnected = false;
			Client?.Close();
		}
	}

	internal class MessageEventArgs : EventArgs {
		internal MessageEventArgs(byte[] data) {
			Data = data;
		}

		internal byte[] Data { get; }
	}
}
