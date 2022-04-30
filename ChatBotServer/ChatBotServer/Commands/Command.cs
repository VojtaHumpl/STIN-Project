using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotServer.Commands {
	internal abstract class Command : ICommand {

		protected abstract int CalculateChecksum();

		public virtual byte[] ToByteArray() {
			// header
			throw new NotImplementedException();
		}

		public override string? ToString() {
			return base.ToString();
		}
	}
}
