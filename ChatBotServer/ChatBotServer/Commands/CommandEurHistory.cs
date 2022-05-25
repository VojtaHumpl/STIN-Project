using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandEurHistory : Command {
		public override List<string> Keys { get; } = new List<string> { "eur", "history" };


		public override byte[] ToServerPacket() {
			return ProtocolParser.CreatePacket(this.ToString());
		}

		private string GetHistory() {
			CommandHelpers.UpdateHistory();

			var data = File.ReadLines(CommandHelpers.HistoryFilePath);
			string res = "";

			foreach (var line in data) {
				var words = line.Split(";");
				res += $"{words[0]}: {words[1]} CZK\n";
			}

			return res;
		}

		public override string ToString() {
			return $"Exchange Rate EUR/CZK History:\n{GetHistory()}";
		}
	}
}
