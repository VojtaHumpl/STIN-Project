using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandHelp : Command {
		protected static List<string> Keys { get; } = new List<string> { "help" };

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

		public override string? ToString() {
			var res = "";
			res += $"Available commands:\n";
			res += $"your name\n";
			res += $"current time\n";
			res += $"eur today\n";
			res += $"eur history";
			return res;
		}
	}
}
