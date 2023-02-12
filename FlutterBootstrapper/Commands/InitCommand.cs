using CommandLine;

namespace FlutterBootstrapper.Commands {
	[Verb("init")]
	internal class InitCommand {
		[Option('a', "arch")]
		public string? Architecture { get; private set; }

		[Option('p', "packages", Separator = ',')]
		public IEnumerable<string>? Packages { get; private set; }

		[Option("delete-existing", Default = true)]
		public bool DeleteExisting { get; set; }

		[Option("sync-packages", Default = true)]
		public bool SyncPackages { get; set; }
	}
}
