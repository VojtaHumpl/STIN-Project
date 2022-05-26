using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandHelpTests {
		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandHelp();
			Assert.AreEqual("Available commands:\n" +
				"your name -> returns bot's name\n" +
				"current time -> returns current time UTC+02:00\n" +
				"eur today -> returns today's exchange rate of eur/czk\n" +
				"eur history -> returns the history of eur/czk exchange rate" +
				"buy recommend -> gives a recommendation whether to buy euro or not",
				cmd.ToString());

			Assert.AreNotEqual("random text", cmd.ToString());
		}

		[TestMethod]
		public void Checksum() {
			var checksum = 188;

			var cmd = new CommandHelp();
			var calculatedChecksum = cmd.ToServerPacket()[^1];

			Assert.AreEqual(checksum, calculatedChecksum);
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] exp = { 146, 118, 110, 61, 14, 77, 242, 226, 65, 118, 97, 105, 108, 97, 98, 108, 101, 32, 99, 111, 109, 109, 97, 110, 100,
				115, 58, 10, 121, 111, 117, 114, 32, 110, 97, 109, 101, 32, 45, 62, 32, 114, 101, 116, 117, 114, 110, 115, 32, 98, 111, 116,
				39, 115, 32, 110, 97, 109, 101, 10, 99, 117, 114, 114, 101, 110, 116, 32, 116, 105, 109, 101, 32, 45, 62, 32, 114, 101, 116,
				117, 114, 110, 115, 32, 99, 117, 114, 114, 101, 110, 116, 32, 116, 105, 109, 101, 32, 85, 84, 67, 43, 48, 50, 58, 48, 48, 10,
				101, 117, 114, 32, 116, 111, 100, 97, 121, 32, 45, 62, 32, 114, 101, 116, 117, 114, 110, 115, 32, 116, 111, 100, 97, 121, 39,
				115, 32, 101, 120, 99, 104, 97, 110, 103, 101, 32, 114, 97, 116, 101, 32, 111, 102, 32, 101, 117, 114, 47, 99, 122, 107, 10,
				101, 117, 114, 32, 104, 105, 115, 116, 111, 114, 121, 32, 45, 62, 32, 114, 101, 116, 117, 114, 110, 115, 32, 116, 104, 101,
				32, 104, 105, 115, 116, 111, 114, 121, 32, 111, 102, 32, 101, 117, 114, 47, 99, 122, 107, 32, 101, 120, 99, 104, 97, 110,
				103, 101, 32, 114, 97, 116, 101, 98, 117, 121, 32, 114, 101, 99, 111, 109, 109, 101, 110, 100, 32, 45, 62, 32, 103, 105, 118,
				101, 115, 32, 97, 32, 114, 101, 99, 111, 109, 109, 101, 110, 100, 97, 116, 105, 111, 110, 32, 119, 104, 101, 116, 104, 101,
				114, 32, 116, 111, 32, 98, 117, 121, 32, 101, 117, 114, 111, 32, 111, 114, 32, 110, 111, 116, 188 };
			var cmd = new CommandHelp();
			var actual = cmd.ToServerPacket();

			Assert.IsTrue(exp.SequenceEqual(actual));
		}
	}
}
