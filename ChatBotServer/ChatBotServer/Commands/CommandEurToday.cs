using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandEurToday : Command {
		protected static List<string> Keys { get; } = new List<string> { "eur", "today" };
		private string URL { get; } = "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";
		
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

		private string GetExchangeRateOnDate(DateTime date) {
			string exchangeRate = "";
			using (HttpClient client = new()) {
				using HttpResponseMessage response = client.GetAsync(URL + "?date=" + date.ToString("dd.MM.yyyy")).Result;
				using HttpContent content = response.Content;

				var stringContent = content.ReadAsStringAsync().Result;
				var lines = stringContent.Split("\n");
				foreach (var line in lines) {
					var words = line.Split("|");
					if (words[0] == "EMU") {
						exchangeRate = words[4];
					}
				}
			}

			return exchangeRate;
		}

		public override string? ToString() {
			var res = "";
			var date = DateTime.Now.ToString("dd.MM.yyyy");
			var exchangeRate = GetExchangeRateOnDate(DateTime.Now);
			res += $"{date}: {exchangeRate} CZK";
			return res;
		}

	}
}
