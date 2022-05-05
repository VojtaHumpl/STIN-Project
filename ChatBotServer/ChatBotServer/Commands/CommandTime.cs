using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandTime : Command {
		
		public override List<string> Keys { get; } = new List<string> { "current", "time" };

		public CommandTime() { }

		public override byte[] ToServerPacket() {
			return ProtocolParser.CreatePacket(this.ToString());
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
