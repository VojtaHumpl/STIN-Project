using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandEurHistoryTests {
		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandEurHistory();
			Assert.IsTrue(cmd.ToString().StartsWith("Exchange Rate EUR/CZK History:\n"));
		}

		/*[TestMethod]
		public void Checksum() {
			var checksum = 207;

			var cmd = new CommandName();
			MethodInfo methodInfo = typeof(CommandName).GetMethod("CalculateChecksum", BindingFlags.NonPublic | BindingFlags.Instance);
			object[] parameters = { };
			var calculatedChecksum = (int)methodInfo.Invoke(cmd, parameters);

			Assert.AreEqual(checksum, calculatedChecksum);
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] exp = { 146, 118, 110, 61, 14, 77, 242, 226, 77, 121, 32, 110, 97, 109,
							101, 32, 105, 115, 32, 74, 111, 104, 110, 207 };
			var cmd = new CommandEurHistory();
			var actual = cmd.ToServerPacket();
			Assert.IsTrue(exp.SequenceEqual(actual));
		}*/
	}
}