using CommandLine;

namespace FlutterBootstrapper.Commands {
	[Verb("auth")]
	internal class AuthCommand : CommandMeta, ICommand {
		public async Task ProcessAsync() {
			await Task.Delay(1);
		}
	}
}
