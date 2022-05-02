using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {

	internal abstract class Command : ICommand {

		private string HeaderHashKey { get; set; } = "sheeesh";
		protected byte[] HeaderHash { get; set; }

		protected Command() {
			var headerHash = SHA256.Create(HeaderHashKey);
			HeaderHash = new byte[8];
			var headerBytes = Encoding.UTF8.GetBytes(headerHash.ToString());
			Array.Copy(headerBytes, 0, HeaderHash, 0, 8);
		}

		protected virtual int CalculateChecksum() {
			int res = 1;
			for (int i = 0; i < HeaderHash.Length; i++) {
				res ^= HeaderHash[i];
			}

			return res;
		}

		public virtual byte[] ToServerPacket() {
			return HeaderHash;
		}

		public override string? ToString() {
			var res = "";
			res += "ChadBot: ";
			return res;
		}
	}
}
