using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBot {
	internal class TCPServer {

		private TcpListener? Server { get; set; } = null;
		private TcpClient? Client { get; set; } = null;
		private int ServerPort { get; set; }
		private int ClientPort { get; set; }
		private IPAddress? ServerIP { get; set; } = IPAddress.Any;
		private IPAddress? ClientIP { get; set; }
		internal byte[] ReadBuffer { get; set; } = new byte[1024];
		internal bool ReceiverRunning { get; set; }
		private bool ClientRunning { get; set; }
		internal bool ServerConnected { get; private set; }
		internal bool ClientConnected { get; private set; }


		internal event EventHandler<EventArgs>? OnMessageReceived;

		public TCPServer() {
			Client = new();
		}
		public TCPServer(int bufferSize = 1024) {
			ReadBuffer = new byte[bufferSize];
			Client = new();
		}

		internal bool StartServer(int port) {
			return StartServer("", port);
		}

		internal bool StartServer(string ip, int port) {
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

				Task.Run(() => Run());
			} catch (Exception e) {
				Console.WriteLine($"Error: {e}");
				StopServerConnection();
			}

			return ServerConnected;
		}

		internal bool RestartConnection() {
			Thread.Sleep(100);
			StopServerConnection();
			try {
				Server = new(ServerIP, ServerPort);
				Server.Start();
				ReceiverRunning = true;
				Console.WriteLine("Waiting for connection...");
				Client = Server.AcceptTcpClient();
				ServerConnected = true;
				Console.WriteLine("Connected");

				Task.Run(() => Run());
			} catch (Exception e) {
				Console.WriteLine($"Error: {e}");
				StopServerConnection();
			}

			return ServerConnected;
		}

		internal bool StartClient(string ip, int port) {
			if (ReceiverRunning)
				return false;

			ReceiverRunning = true;
			ClientIP = IPAddress.Parse(ip);
			ClientPort = port;
			Client = new();
			Client.ReceiveTimeout = 6000;

			try {
				Debug.WriteLine("Connecting...");
				Client.Connect(ClientIP, ClientPort);
				ClientConnected = true;
				Debug.WriteLine("Connected");

				Task.Run(() => Run());
			} catch (Exception e) {
				Debug.WriteLine($"Error: {e}");
			}

			return ClientConnected;
		}

		private async void Run() {
			while (ReceiverRunning) {
				try {
					var stream = Client.GetStream();
					int length;
					while ((length = stream.Read(ReadBuffer, 0, ReadBuffer.Length)) != 0 && ReceiverRunning) {
						var readData = new byte[length];
						Array.Copy(ReadBuffer, readData, length);
						Console.WriteLine($"Received {Encoding.UTF8.GetString(readData)}");
						OnMessageReceived?.Invoke(this, new MessageEventArgs(readData));
					}
				} catch (Exception e) {
					Debug.WriteLine($"Error: {e}");
				} finally {
					Debug.WriteLine("Disconnected");
					StopServerConnection();
					//RestartConnection();
				}
			}
		}

		internal void SendData(byte[] data) {
			var stream = Client.GetStream();
			stream.Write(data, 0, data.Length);
		}

		internal void StopServerConnection() {
			ServerConnected = false;
			ReceiverRunning = false;
			Client?.Close();
		}

		internal void Stop() {
			ServerConnected = false;
			ReceiverRunning = false;
			ClientConnected = false;
			ClientRunning = false;
			Client?.Close();
			Server?.Stop();
		}

		internal void StopClient() {
			ClientConnected = false;
			ClientRunning = false;
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
