using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatBotServer.TCPCommunication;

namespace ChatBotServer.Commands {
	internal class CommandRecommend : Command {
		public override List<string> Keys { get; } = new List<string> { "recommend", "buy" };
		private const int SAMPLE_SIZE = 3;	// how many days to look back at

		public override byte[] ToServerPacket() {
			return ProtocolParser.CreatePacket(this.ToString());
		}

		private bool RecommendBuying(out string reason) {
			var avg = CommandHelpers.GetAverageExchangeRate();

			double[] sample = new double[SAMPLE_SIZE];

			var date = DateTime.Now;
			for (int i = 0; i < sample.Length; i++) {
				sample[i] = CommandHelpers.GetExchangeRateOnDateFromFile(date.AddDays(-i));
			}

			// rate going down check
			int rate = 0;
			for (int i = 1; i < sample.Length; i++) {
				if (sample[i - 1] < sample[i]) {
					rate++;
				} else {
					rate--;
					break;
				}
			}
			if (rate == SAMPLE_SIZE - 1) {
				throw new Exception($"wtf {rate} {SAMPLE_SIZE}");
				reason = $"Exchange rate of EUR/CZK is going down over the last {SAMPLE_SIZE} days";
				return true;
			}

			// rate not rising over 10% over avg check
			var today = DateTime.Now;
			var rateToday = CommandHelpers.GetExchangeRateOnDateFromFile(today);

			var delta = (rateToday - avg) / avg;

			if (0.1 > delta) {
				reason = $"Exchange rate of EUR/CZK changed only by {100 * delta:0.##}% over the last {SAMPLE_SIZE} days";
				return true;
			}

			reason = $"Exchange rate of EUR/CZK is not going down and the price is {100 * delta:0.##}% above average over the last {SAMPLE_SIZE} days";
			return false;
		}

		public override string ToString() {
			string reason = "";
			var res = RecommendBuying(out reason);

			if (res)
				return $"I recommend buying right now, because the {reason}";
			else
				return $"I do not recommend buying right now, because the {reason}";
		}
	}
}
