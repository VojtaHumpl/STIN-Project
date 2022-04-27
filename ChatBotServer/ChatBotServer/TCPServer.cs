using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer {
	internal class TCPServer {

		private TcpListener? Server { get; set; } = null;
		private TcpClient? Client { get; set; } = null;
		private int Port { get; set; }
		private IPAddress IP { get; set; }
		private byte[] ReadBuffer { get; set; } = new byte[256];
		private bool Running { get; set; }
		internal bool Connected { get; private set; }


		internal event EventHandler<EventArgs> OnMessageReceived;

		public TCPServer(string ip, int port) {
			Port = port;
			IP = IPAddress.Parse(ip);
			Server = new(IP, Port);
			Running = true;
		}

		internal bool Start() {
			Stop();
			try {
				Server!.Start();
				Running = true;
				Debug.WriteLine("Waiting for connection...");
				Client = Server.AcceptTcpClient();
				Connected = true;
				Debug.WriteLine("Connected");

				Task.Run(() => Run());
			} catch (Exception e) {
				Debug.WriteLine($"Error: {e}");
				Stop();
			}

			return Connected;
		}

		private async void Run() {
			while (Running) {
				try {
					var stream = Client!.GetStream();
					int length;
					while ((length = stream.Read(ReadBuffer, 0, ReadBuffer.Length)) != 0 && Running) {
						var readData = new byte[length];
						Array.Copy(ReadBuffer, readData, length);
						Debug.WriteLine($"Received {Encoding.UTF8.GetString(readData)}");
						OnMessageReceived?.Invoke(this, new MessageEventArgs(readData));
					}
				} catch (Exception e) {
					Debug.WriteLine($"Error: {e}");
				} finally {
					Stop();
					Debug.WriteLine("Disconnected");
				}
			}
		}

		internal void SendData(byte[] data) {
			var stream = Client.GetStream();
			stream.Write(data, 0, data.Length);
		}

		internal void Stop() {
			Connected = false;
			Running = false;
			Client?.Close();
			Server?.Stop();
		}
	}

	internal class MessageEventArgs : EventArgs {
		internal MessageEventArgs(byte[] data) {
			Data = data;
		}

		internal byte[] Data { get; }
	}
}
