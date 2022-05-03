using ChatBotServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal static class CommandFactory {

		internal static Command? Create(Type? commandType) {
			if (commandType is null)
				return null;
			return (Command?)Activator.CreateInstance(commandType);
		}

	}
}
