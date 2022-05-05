using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.TCPCommunication {

	
	internal static class ProtocolParser {
		private static string HeaderHashKey { get; } = "headerklic";

		internal static string ParsePacket(byte[] data) {
			var header = new byte[8];
			Array.Copy(data, 0, header, 0, 8);
			byte[] content = new byte[data.Length - 8 - 1];
			Array.Copy(data, 8, content, 0, data.Length - 8 - 1);
			int checksum = (int)data[^1];

			if (!header.SequenceEqual(CalculateHeaderHash())) {
				Console.WriteLine("Packet header is wrong");
				return string.Empty;
			}

			var calculatedChecksum = 1;
			for (int i = 0; i < header.Length; i++) {
				calculatedChecksum ^= header[i];
			}

			var message = Encoding.UTF8.GetString(content);
			for (int i = 0; i < content.Length; i++) {
				calculatedChecksum ^= message[i];
			}

			if (checksum != calculatedChecksum) {
				Console.WriteLine("Packet checksums do not match");
				return string.Empty;
			}

			return message;
		}

		internal static byte[] CreatePacket(string data) {
			var header = CalculateHeaderHash();
			var message = Encoding.UTF8.GetBytes(data);
			var checksum = 1;
			var packet = new byte[header.Length + message.Length + 1];

			for (int i = 0; i < header.Length; i++) {
				checksum ^= header[i];
			}

			for (int i = 0; i < message.Length; i++) {
				checksum ^= message[i];
			}

			Array.Copy(header, 0, packet, 0, header.Length);
			Array.Copy(message, 0, packet, header.Length, message.Length);
			packet[^1] = (byte)checksum;

			return packet;
		}

		// TODO calc only once
		internal static byte[] CalculateHeaderHash() {
			var headerHash = SHA256.Create();
			var headerBytes = headerHash.ComputeHash(Encoding.UTF8.GetBytes(HeaderHashKey));
			var res = new byte[8];
			Array.Copy(headerBytes, 0, res, 0, 8);
			return res;
		}
	}
}
