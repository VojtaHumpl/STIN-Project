using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandTime : Command {
		
		public override List<string> Keys { get; } = new List<string> { "current", "time" };

		public CommandTime() { }

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
			TimeZoneInfo cestZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
			DateTime cestTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), cestZone);
			res += $"The current time is {cestTime:dddd, dd MMMM yyyy HH':'mm':'ss} UTC+02:00";
			return res;
		}


	}
}
