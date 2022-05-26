using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ChatBotServer.Commands;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CommandsTests {
	[TestClass]
	[DoNotParallelize]
	public class CommandRecommendTests {

		private const string PATH = "eur_history.txt";

		[TestMethod]
		public void ToStringResult() {
			var cmd = new CommandRecommend();
			Assert.IsTrue(cmd.ToString().StartsWith("I recommend buying right now, because the ") || cmd.ToString().StartsWith("I do not recommend buying right now, because the "));
		}

		[TestMethod]
		public void ServerPacket() {
			File.Delete(PATH);

			var wr = File.AppendText(PATH);
			wr.WriteLine("18.05.2022;20,650");
			wr.WriteLine("19.05.2022;20,650");
			wr.WriteLine("20.05.2022;20,650");
			wr.WriteLine("21.05.2022;21,650");
			wr.WriteLine("22.05.2022;20,650");
			wr.WriteLine("23.05.2022;20,650");
			wr.WriteLine("24.05.2022;21,650");
			wr.WriteLine("25.05.2022;21,650");
			wr.WriteLine("26.05.2022;20,650");
			wr.Close();

			byte[] expStart = { 146, 118, 110, 61, 14, 77, 242, 226, 73, 32, 114, 101, 99, 111, 109, 109, 101, 110, 100,
				32, 98, 117, 121, 105, 110, 103, 32, 114, 105, 103, 104, 116, 32, 110, 111, 119, 44, 32, 98, 101, 99, 97 };

			var cmd = new CommandRecommend();
			cmd.ToString();
			var packet = cmd.ToServerPacket();

			byte[] actualStart = new byte[42];
			Array.Copy(packet, 0, actualStart, 0, 42);

			Assert.IsTrue(expStart.SequenceEqual(actualStart));
		}

		[TestMethod]
		public void NoRecommendAvg() {
			File.Delete(PATH);

			var wr = File.AppendText(PATH);
			wr.WriteLine("18.05.2022;20,650");
			wr.WriteLine("19.05.2022;20,650");
			wr.WriteLine("20.05.2022;20,650");
			wr.WriteLine("21.05.2022;20,650");
			wr.WriteLine("22.05.2022;20,650");
			wr.WriteLine("23.05.2022;20,650");
			wr.WriteLine("24.05.2022;27,650");
			wr.WriteLine("25.05.2022;27,650");
			wr.WriteLine("26.05.2022;27,650");
			wr.Close();

			var cmd = new CommandRecommend();
			Assert.IsTrue(cmd.ToString().StartsWith("I do not recommend buying right now, because the Exchange rate of EUR/CZK is not going down and the price is "));
		}

		[TestMethod]
		public void YesRecommendGoingDown() {
			File.Delete(PATH);

			var wr = File.AppendText(PATH);
			wr.WriteLine("18.05.2022;20,650");
			wr.WriteLine("19.05.2022;20,650");
			wr.WriteLine("20.05.2022;20,650");
			wr.WriteLine("21.05.2022;20,650");
			wr.WriteLine("22.05.2022;20,650");
			wr.WriteLine("23.05.2022;20,650");
			wr.WriteLine("24.05.2022;19,650");
			wr.WriteLine("25.05.2022;18,650");
			wr.WriteLine("26.05.2022;17,650");
			wr.Close();

			var cmd = new CommandRecommend();
			Assert.IsTrue(cmd.ToString().StartsWith("I recommend buying right now, because the Exchange rate of EUR/CZK is going down over the last "));
		}

		[TestMethod]
		public void YesRecommendAvg() {
			File.Delete(PATH);

			var wr = File.AppendText(PATH);
			wr.WriteLine("18.05.2022;20,650");
			wr.WriteLine("19.05.2022;20,650");
			wr.WriteLine("20.05.2022;19,650");
			wr.WriteLine("21.05.2022;20,650");
			wr.WriteLine("22.05.2022;20,650");
			wr.WriteLine("23.05.2022;20,650");
			wr.WriteLine("24.05.2022;19,650");
			wr.WriteLine("25.05.2022;18,950");
			wr.WriteLine("26.05.2022;19,650");
			wr.Close();

			var cmd = new CommandRecommend();
			Assert.IsTrue(cmd.ToString().StartsWith("I recommend buying right now, because the Exchange rate of EUR/CZK changed only by "));
		}
	}
}