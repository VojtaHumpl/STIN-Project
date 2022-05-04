using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CommandsTest {
	[TestClass]
	public class CommandHelpTests {
		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandHelp();
			Assert.AreEqual("Available commands:\n" +
				"your name -> returns bot's name\n" +
				"current time -> returns current time UTC+02:00\n" +
				"eur today -> returns today's exchange rate of eur/czk\n" +
				"eur history -> returns the history of eur/czk exchange rate",
				cmd.ToString());

			Assert.AreNotEqual("random text", cmd.ToString());
		}
	}
}
