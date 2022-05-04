using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal class CommandEurHistory : Command {
		protected static List<string> Keys { get; } = new List<string> { "eur", "history" };
		private string URL { get; } = "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";
		private DateTime HistoryFrom { get; } = new DateTime(2022, 4, 1);

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

		private string GetHistory() {
			var res = "";
			int i = 0;
			while ((DateTime.Now - HistoryFrom.AddDays(i)).TotalDays >= 0) {
				var date = HistoryFrom.AddDays(i).ToString("dd.MM.yyyy");
				var exchangeRate = GetExchangeRateOnDate(HistoryFrom.AddDays(i));
				if ((DateTime.Now - HistoryFrom.AddDays(i)).TotalDays <= 1)
					res += $"{date}: {exchangeRate} CZK";
				else
					res += $"{date}: {exchangeRate} CZK\n";
				i++;
			}

			return res;
		}

		public override string ToString() {
			return $"Exchange Rate EUR/CZK History:\n{GetHistory()}";
		}
	}
}
