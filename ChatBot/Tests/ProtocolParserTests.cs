using ChatBot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tests {
	[TestClass]
	public class ProtocolParserTests {
		[TestMethod]
		public void PacketCreate() {
			var p = ProtocolParser.CreatePacket("What is your name");
			byte[] exp = { 146, 118, 110, 61, 14, 77, 242, 226, 87, 104, 97, 116, 32, 105,
							115, 32, 121, 111, 117, 114, 32, 110, 97, 109, 101, 227 };
			Assert.IsTrue(exp.SequenceEqual(p));
		}

		[TestMethod]
		public void ParsePacket() {
			byte[] packet = { 146, 118, 110, 61, 14, 77, 242, 226, 87, 104, 97, 116, 32, 105,
								115, 32, 121, 111, 117, 114, 32, 110, 97, 109, 101, 227 };
			string expected = "What is your name";
			Assert.AreEqual(expected, ProtocolParser.ParsePacket(packet));
		}

		[TestMethod]
		public void ParsePacketWrongHeader() {
			byte[] packet = { 146, 118, 110, 12, 14, 77, 242, 226, 87, 104, 97, 116, 32, 105,
								115, 32, 121, 111, 117, 114, 32, 110, 97, 109, 101, 227 };
			string expected = string.Empty;
			Assert.AreEqual(expected, ProtocolParser.ParsePacket(packet));
		}

		[TestMethod]
		public void ParsePacketChecksumWrong() {
			byte[] packet = { 146, 118, 110, 61, 14, 77, 242, 226, 87, 104, 97, 116, 32, 105,
								115, 32, 121, 111, 117, 114, 32, 110, 97, 109, 101, 226 };
			string expected = string.Empty;
			Assert.AreEqual(expected, ProtocolParser.ParsePacket(packet));
		}

		[TestMethod]
		public void HeaderHash() {
			var hash = ProtocolParser.CalculateHeaderHash();
			byte[] header = { 146, 118, 110, 61, 14, 77, 242, 226 };
			Assert.IsTrue(header.SequenceEqual(hash));
		}
	}
}