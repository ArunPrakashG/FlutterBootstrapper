using CommandLine;

namespace FlutterBootstrapper.Core.Commands {
	[Verb("auth")]
	internal class AuthCommand : CommandMeta, ICommand {
		public async Task ProcessAsync() {
			await Task.Delay(1);
		}
	}
}
