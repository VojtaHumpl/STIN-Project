using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandEurTodayTests {
		[TestMethod]
		public void ToStringResult() {
			var date = DateTime.Now.ToString("dd.MM.yyyy");
			var cmd = new CommandEurToday();
			var result = cmd.ToString();

			Assert.IsTrue(result.StartsWith(date));
			Assert.IsTrue(result.EndsWith(" CZK"));
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] expStart = { 146, 118, 110, 61, 14, 77, 242, 226 };
			byte[] expEnd = { 32, 67, 90, 75 };
			var cmd = new CommandEurToday();
			var packet = cmd.ToServerPacket();

			byte[] actualStart = new byte[8];
			byte[] actualEnd = new byte[4];

			Array.Copy(packet, 0, actualStart, 0, 8);
			Array.Copy(packet, packet.Length - 5, actualEnd, 0, 4);

			Assert.IsTrue(expStart.SequenceEqual(actualStart));
			Assert.IsTrue(expEnd.SequenceEqual(actualEnd));
		}
	}
}