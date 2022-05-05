using ChatBotServer.TCPCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {

	internal abstract class Command : ICommand {
		public abstract List<string> Keys { get; }
		protected byte[] HeaderHash { get; set; }

		protected Command() {
			HeaderHash = ProtocolParser.CalculateHeaderHash();
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
	}
}
