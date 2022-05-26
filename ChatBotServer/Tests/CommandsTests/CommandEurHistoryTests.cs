using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tests.CommandsTests {
	[TestClass]
	[DoNotParallelize]
	public class CommandEurHistoryTests {

		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandEurHistory();
			Assert.IsTrue(cmd.ToString().StartsWith("Exchange Rate EUR/CZK History:\n"));
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] expStart = { 146, 118, 110, 61, 14, 77, 242, 226, 69, 120, 99, 104, 97, 110, 103, 101, 32, 82,
					97, 116, 101, 32, 69, 85, 82, 47, 67, 90, 75, 32, 72, 105, 115, 116, 111, 114, 121, 58, 10 };

			var cmd = new CommandEurHistory();
			var packet = cmd.ToServerPacket();

			byte[] actualStart = new byte[39];
			Array.Copy(packet, 0, actualStart, 0, 39);

			Assert.IsTrue(expStart.SequenceEqual(actualStart));
		}
	}
}