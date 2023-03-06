using CommandLine;
using FlutterBootstrapper.Abstracts.Command;

namespace FlutterBootstrapper.Core.Commands {
	[Verb("auth")]
	internal class AuthCommand : CommandDependencies, ICommand {
		public async Task ProcessAsync() {
			await Task.Delay(1);
		}
	}
}
