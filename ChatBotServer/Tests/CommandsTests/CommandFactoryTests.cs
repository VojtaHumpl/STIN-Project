using ChatBotServer.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.CommandsTests {
	[TestClass]
	public class CommandFactoryTests {
		[TestMethod]
		public void ReturnType() {
			Type cmdType = new CommandName().GetType();
			Assert.IsInstanceOfType(CommandFactory.Create(cmdType), typeof(CommandName));
		}

		[TestMethod]
		public void ReturnNull() {
			Assert.IsNull(CommandFactory.Create(null));
			Assert.IsNull(CommandFactory.Create(typeof(int)));
		}
	}
}