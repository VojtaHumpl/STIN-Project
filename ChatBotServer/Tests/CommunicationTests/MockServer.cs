using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.CommunicationTests {
	internal class MockServer : ITCPServer {

		private TcpListener? Server { get; set; } = null;
		private TcpClient? Client { get; set; } = null;
		private int ServerPort { get; set; }
		private IPAddress ServerIP { get; set; } = IPAddress.Any;
		internal byte[] ReadBuffer { get; private set; } = new byte[1024];
		internal bool ReceiverRunning { get; private set; }
		internal bool ServerConnected { get; private set; }
		internal bool ClientConnected { get; private set; }

		public void SendData(byte[] message) {
			Thread.Sleep(100);
			return;
		}

		public bool StartServer(string ip, int port) {
			Thread.Sleep(100);
			ReceiverRunning = true;
			return true;
		}

		public void Stop() {
			ServerConnected = false;
			ReceiverRunning = false;
			ClientConnected = false;
		}
	}
}
