using ChatBotServer.TCPCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.CommunicationTests {
	[TestClass]
	public class TCPServerTests {
		[TestMethod]
		public void ServerConstructors() {
			var server1 = new TCPServer();
			var server2 = new TCPServer(256);
			Assert.AreEqual(1024, server1.ReadBuffer.Length);
			Assert.AreEqual(256, server2.ReadBuffer.Length);
		}

		[TestMethod]
		public void ServerListening1() {
			CancellationTokenSource s_cts = new CancellationTokenSource();
			var server = new TCPServer();
			Task.Run(() => {
				s_cts.CancelAfter(1500);
				server.StartServer(9999);
			});
			Thread.Sleep(100);		// wait for init
			Assert.IsFalse(server.ServerConnected);
			Assert.IsTrue(server.ReceiverRunning);
			Thread.Sleep(2000);
		}

		[TestMethod]
		public void ServerListening2() {
			CancellationTokenSource s_cts = new CancellationTokenSource();
			var server = new TCPServer();
			Task.Run(() => {
				s_cts.CancelAfter(1500);
				server.StartServer("127.0.0.1", 9999);
			});
			Thread.Sleep(100);      // wait for init
			Assert.IsFalse(server.ServerConnected);
			Assert.IsTrue(server.ReceiverRunning);
			Thread.Sleep(2000);
		}

		[TestMethod]
		public void ServerReset() {
			CancellationTokenSource s_cts = new CancellationTokenSource();
			var server = new TCPServer();
			Task.Run(() => {
				s_cts.CancelAfter(2000);
				server.RestartConnection();
			});
			Thread.Sleep(200);      // wait for init
			Assert.IsFalse(server.ServerConnected);
			Assert.IsTrue(server.ReceiverRunning);
		}

		[TestMethod]
		public void SendDataException() {
			var server = new TCPServer();
			Assert.ThrowsException<NullReferenceException>(() => server.SendData(new byte[]{ 1, 1 }));	
		}

		[TestMethod]
		public void Stops() {
			var server = new TCPServer();
			server.StopClient();
			Assert.IsFalse(server.ClientConnected);

			server = new();
			server.StopServerConnection();
			Assert.IsFalse(server.ServerConnected);
			Assert.IsFalse(server.ReceiverRunning);

			server = new();
			server.Stop();
			Assert.IsFalse(server.ClientConnected);
			Assert.IsFalse(server.ServerConnected);
			Assert.IsFalse(server.ReceiverRunning);
		}

		[TestMethod]
		public void MessageEventArgs() {
			var msge = new MessageEventArgs(new byte[] { 1, 1 });
			var exp = new byte[] { 1, 1 };
			Assert.IsTrue(exp.SequenceEqual(msge.Data));
		}
	}
}