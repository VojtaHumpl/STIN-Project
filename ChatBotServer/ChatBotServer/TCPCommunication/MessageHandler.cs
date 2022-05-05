using ChatBotServer.Commands;
using ChatBotServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.TCPCommunication {
	internal class MessageHandler {
		internal Dictionary<Type, List<string>> CommandTypes { get; init; } = new();
		internal List<string> Keywords { get; init; } = new();

		internal MessageHandler() {
			var commands = Reflection.GetEnumerableOfType<Command>();
			foreach (var cmd in commands) {
				CommandTypes.Add(cmd.GetType(), cmd.Keys);
				Keywords.AddRange(cmd.Keys);
			}
		}

		internal Command? AnalyzeMessage(string message) {
			var words = message.Split(" ");
			var foundKeywords = new List<string>();
			foreach (var word in words) {
				if (Keywords.Contains(word)) {
					foundKeywords.Add(word);
				}
			}

			foreach (var cmd in CommandTypes) {
				if (cmd.Value.Count == foundKeywords.Count && foundKeywords.All(cmd.Value.Contains)) {
					return CommandFactory.Create(cmd.Key);
				}
			}

			return null;
		}

		internal byte[] GetResponsePacketToMessage(Command cmd) {
			return cmd is null ? ProtocolParser.CreatePacket("I don't understand, try \"help\"") : cmd.ToServerPacket();
		}

		internal string GetResponseToMessage(Command cmd) {
			return cmd is null ? "I don't understand, try \"help\"" : cmd.ToString();
		}

	}
}
