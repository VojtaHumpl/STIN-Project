using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandTimeTests {
		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandTime();
			Assert.IsTrue(cmd.ToString().StartsWith("The current time is "));
			Assert.IsTrue(cmd.ToString().EndsWith(" UTC+02:00"));
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] expStart = { 146, 118, 110, 61, 14, 77, 242, 226, 84, 104, 101, 32, 99, 117, 114,
									114, 101, 110, 116, 32, 116, 105, 109, 101, 32, 105, 115, 32 };
			byte[] expEnd = { 32, 85, 84, 67, 43, 48, 50, 58, 48, 48 };
			var cmd = new CommandTime();
			var packet = cmd.ToServerPacket();

			byte[] actualStart = new byte[28];
			byte[] actualEnd = new byte[10];

			Array.Copy(packet, 0, actualStart, 0, 28);
			Array.Copy(packet, packet.Length - 11, actualEnd, 0, 10);

			Assert.IsTrue(expStart.SequenceEqual(actualStart));
			Assert.IsTrue(expEnd.SequenceEqual(actualEnd));
		}
	}
}
