using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CommandsTest {
	[TestClass]
	public class CommandNameTests {

		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandName();
			Assert.AreEqual("My name is John", cmd.ToString());
		}
	}
}
