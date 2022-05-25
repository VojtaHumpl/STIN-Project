using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal static class CommandHelpers {

		private static string ExhangeRateURL { get; } = "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";
		private static DateTime HistoryFrom { get; } = new DateTime(2022, 5, 1);
		internal static string HistoryFilePath { get; } = "eur_history.txt";


		internal static void UpdateHistory() {
			if (!File.Exists(HistoryFilePath)) {
				File.Create(HistoryFilePath);
			}

			if (new FileInfo(HistoryFilePath).Length == 0) {
				int i = 0;
				using StreamWriter file = new(HistoryFilePath, append: true);
				while ((DateTime.Now - HistoryFrom.AddDays(i)).TotalDays >= 0) {
					var date = HistoryFrom.AddDays(i).ToString("dd.MM.yyyy");
					var exchangeRate = GetExchangeRateOnDateFromWeb(HistoryFrom.AddDays(i));
					file.WriteLine($"{date};{exchangeRate}");
					i++;
				}
			} else {
				var lastLine = File.ReadLines(HistoryFilePath).Last().Split(";");
				var lastDate = lastLine[0];
				var datetime = DateTime.ParseExact(lastDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

				int i = 1;
				using StreamWriter file = new(HistoryFilePath, append: true);
				while ((DateTime.Now - datetime.AddDays(i)).TotalDays >= 0) {
					var date = datetime.AddDays(i).ToString("dd.MM.yyyy");
					var exchangeRate = GetExchangeRateOnDateFromWeb(datetime.AddDays(i));
					file.WriteLine($"{date};{exchangeRate}");
					i++;
				}
			}
		}

		internal static double GetExchangeRateOnDateFromFile(DateTime date) {
			var data = File.ReadLines(HistoryFilePath);
			foreach (var line in data) {
				var words = line.Split(";");
				var currDate = DateTime.ParseExact(words[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
				if (currDate.Date == date.Date) {
					return double.Parse(words[1]);
				}
			}

			throw new ArgumentOutOfRangeException("Date in the argument is not found in the history file.");
		}

		internal static string GetExchangeRateOnDateFromWeb(DateTime date) {
			string exchangeRate = "";
			using (HttpClient client = new()) {
				using HttpResponseMessage response = client.GetAsync(ExhangeRateURL + "?date=" + date.ToString("dd.MM.yyyy")).Result;
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

		internal static double GetAverageExchangeRate() {
			var data = File.ReadLines(HistoryFilePath);
			double sum = 0;

			foreach (var line in data) {
				var words = line.Split(";");
				sum += double.Parse(words[1]);
			}

			return sum / data.Count();
		}
	}
}
