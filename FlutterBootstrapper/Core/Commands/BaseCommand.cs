using CommandLine;
using FlutterBootstrapper.Abstracts.Command;

namespace FlutterBootstrapper.Core.Commands {
	[Verb("base", isDefault: true)]
	internal class BaseCommand : CommandMeta, ICommand {
		public async Task ProcessAsync() {
			await Task.Delay(1);
		}
	}
}
