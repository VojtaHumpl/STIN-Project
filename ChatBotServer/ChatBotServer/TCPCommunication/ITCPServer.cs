using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.TCPCommunication {
	internal interface ITCPServer {
		public bool StartServer(string ip, int port);
		public void Stop();
		public void SendData(byte[] message);
	}
}
