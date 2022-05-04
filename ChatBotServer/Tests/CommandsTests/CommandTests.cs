using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatBotServer.Commands;

namespace Tests.CommandsTest {
	[TestClass]
	public class CommandTests {

		[TestMethod]
		public void TypeTest() {
			Command cmd = new CommandName();
			Assert.AreEqual(typeof(Command), cmd.GetType().BaseType);
			Assert.AreNotEqual(typeof(CommandName), cmd.GetType().BaseType);
		}
	}
}