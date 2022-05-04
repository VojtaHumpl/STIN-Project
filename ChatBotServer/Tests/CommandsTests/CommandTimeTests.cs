using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandTimeTests {
		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandTime();
			Assert.IsTrue(cmd.ToString().StartsWith("The current time is "));
			Assert.IsTrue(cmd.ToString().EndsWith(" UTC+02:00"));
			Assert.IsTrue(cmd.ToString().Length == 61);
		}
	}
}