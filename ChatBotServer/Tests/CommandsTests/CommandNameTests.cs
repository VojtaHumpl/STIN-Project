﻿using ChatBotServer.Commands;
using ChatBotServer.TCPCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandNameTests {

		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandName();
			Assert.AreEqual("My name is John", cmd.ToString());
		}

		[TestMethod]
		public void Checksum() {
			var checksum = 207;

			var cmd = new CommandName();
			var calculatedChecksum = cmd.ToServerPacket()[^1];

			Assert.AreEqual(checksum, calculatedChecksum);
		}

		[TestMethod]
		public void ServerPacket() {
			byte[] exp = { 146, 118, 110, 61, 14, 77, 242, 226, 77, 121, 32, 110, 97, 109,
							101, 32, 105, 115, 32, 74, 111, 104, 110, 207 };
			var cmd = new CommandName();
			var actual = cmd.ToServerPacket();
			Assert.IsTrue(exp.SequenceEqual(actual));
		}
	}
}
