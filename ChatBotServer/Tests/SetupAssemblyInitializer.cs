using System;

using ChatBotServer.Commands;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
	[TestClass]
	public class SetupAssemblyInitializer {
		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context) {
			CommandHelpers.UpdateHistory();
		}
	}
}