using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandHelp : Command {
		public override List<string> Keys { get; } = new List<string> { "help" };

		public CommandHelp() : base() {}

		public override byte[] ToServerPacket() {
			return ProtocolParser.CreatePacket(this.ToString());
		}

		public override string ToString() {
			var res = "";
			res += $"Available commands:\n";
			res += $"your name -> returns bot's name\n";
			res += $"current time -> returns current time UTC+02:00\n";
			res += $"eur today -> returns today's exchange rate of eur/czk\n";
			res += $"eur history -> returns the history of eur/czk exchange rate";
			return res;
		}
	}
}
