using ChatBotServer.Commands;
using ChatBotServer.TCPCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.CommunicationTests {
	[TestClass]
	public class MessageHandlerTests {
		[TestMethod]
		public void KeywordsReflection() {
			var msgh = new MessageHandler();
			Assert.AreEqual(5, msgh.CommandTypes.Count);
			Assert.AreEqual(9, msgh.Keywords.Count);
		}

		[TestMethod]
		public void AnalyzeWrongMessage() {
			var msgh = new MessageHandler();
			Assert.IsNull(msgh.AnalyzeMessage("hello there"));
			Assert.IsNull(msgh.AnalyzeMessage("current time name"));
		}

		[TestMethod]
		public void AnalyzeCorrectMessage() {
			var msgh = new MessageHandler();
			Assert.IsInstanceOfType(msgh.AnalyzeMessage("your name"), typeof(CommandName));
		}

		[TestMethod]
		public void ResponseWrongMessage() {
			var msgh = new MessageHandler();
			Assert.AreEqual("I don't understand, try \"help\"", msgh.GetResponseToMessage(null));
		}

		[TestMethod]
		public void ResponseCorrectMessage() {
			var msgh = new MessageHandler();
			Assert.AreEqual("My name is John", msgh.GetResponseToMessage(new CommandName()));
		}

		[TestMethod]
		public void ResponsePacketWrongMessage() {
			var msgh = new MessageHandler();
			byte[] exp = { 146, 118, 110, 61, 14, 77, 242, 226, 73, 32, 100, 111, 110, 39, 116, 32, 117, 110, 100,
						101, 114, 115, 116, 97, 110, 100, 44, 32, 116, 114, 121, 32, 34, 104, 101, 108, 112, 34, 220 };
			Assert.IsTrue(exp.SequenceEqual(msgh.GetResponsePacketToMessage(null)));
		}
	}
}