using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
	}
}