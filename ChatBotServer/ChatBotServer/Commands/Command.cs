using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {

	internal abstract class Command : ICommand {
		public abstract List<string> Keys { get; }

		protected Command() {}

		public abstract byte[] ToServerPacket();
	}
}
