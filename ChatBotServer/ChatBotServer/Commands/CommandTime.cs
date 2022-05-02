using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandTime : Command {
		
		protected static List<string> Keys { get; } = new List<string> { "current", "time" };

		public CommandTime() { }

		protected override int CalculateChecksum() {
			throw new NotImplementedException();
		}

		public override byte[] ToServerPacket() {
			return base.ToServerPacket();
		}

		public override string? ToString() {
			return base.ToString();
		}


	}
}
