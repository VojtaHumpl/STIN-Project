using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {

	internal class CommandName : Command {
		public override List<string> Keys { get; } = new List<string> { "your", "name" };
		private string Name { get; set; } = "John";

		public CommandName() : base() { }

		public override byte[] ToServerPacket() {
			return ProtocolParser.CreatePacket(this.ToString());
		}

		public override string ToString() {
			var res = "";
			res += $"My name is {Name}";
			return res;
		}

	}
}
