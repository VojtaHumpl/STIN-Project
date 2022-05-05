using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandHelp : Command {
		public override List<string> Keys { get; } = new List<string> { "help" };

		public CommandHelp() : base() {}

		protected override int CalculateChecksum() {
			var res = base.CalculateChecksum();

			var msg = this.ToString();
			for (int i = 0; i < msg!.Length; i++) {
				res ^= msg[i];
			}

			return res;
		}

		public override byte[] ToServerPacket() {
			var header = base.ToServerPacket();
			var messagee = Encoding.UTF8.GetBytes(this.ToString());
			var checksum = CalculateChecksum();
			var packet = new byte[header.Length + messagee.Length + 1];

			Array.Copy(header, 0, packet, 0, header.Length);
			Array.Copy(messagee, 0, packet, header.Length, messagee.Length);
			packet[^1] = (byte)checksum;

			return packet;
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
