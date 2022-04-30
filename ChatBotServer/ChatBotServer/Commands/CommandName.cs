using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {

	internal class CommandName : Command {
		protected static List<string> Keys { get; } = new List<string> { "your", "name" };

		public CommandName() { }


		protected override int CalculateChecksum() {
			throw new NotImplementedException();
		}

		public override byte[] ToByteArray() {
			// get command base.ToByteArr
			// zprava
			// calculate check
			throw new NotImplementedException();
		}

		public override string? ToString() {
			return base.ToString();
		}

	}
}
