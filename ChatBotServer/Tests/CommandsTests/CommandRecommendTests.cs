using System;
using System.Linq;

using ChatBotServer.Commands;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandRecommendTests {

		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandRecommend();
			Assert.IsTrue(cmd.ToString().StartsWith("I recommend buying right now, because the ") || cmd.ToString().StartsWith("I do not recommend buying right now, because the "));
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] expStart = { 146, 118, 110, 61, 14, 77, 242, 226, 73, 32, 114, 101, 99, 111, 109, 109, 101, 110, 100,
				32, 98, 117, 121, 105, 110, 103, 32, 114, 105, 103, 104, 116, 32, 110, 111, 119, 44, 32, 98, 101, 99, 97 };

			var cmd = new CommandRecommend();
			var packet = cmd.ToServerPacket();

			byte[] actualStart = new byte[42];
			Array.Copy(packet, 0, actualStart, 0, 42);

			Assert.IsTrue(expStart.SequenceEqual(actualStart));
		}
	}
}