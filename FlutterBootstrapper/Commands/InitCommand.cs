using CommandLine;
using FlutterBootstrapper.Core;
using FlutterBootstrapper.Core.Services;
using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper.Commands {
	[Verb("init")]
	internal class InitCommand : CommandMeta, ICommand {
		[Option('n', "name", Required = true)]
		public string? ProjectName { get; private set; }

		[Option('a', "arch", Default = "mvm")]
		public string? Architecture { get; private set; }

		[Option('p', "packages", Separator = ',')]
		public IEnumerable<string>? Packages { get; private set; }

		[Option("delete-existing", Default = true)]
		public bool DeleteExisting { get; private set; }

		[Option("sync-packages", Default = true)]
		public bool SyncPackages { get; private set; }

		public async Task ProcessAsync() {
			if (ProjectName == null) {
				throw new InvalidOperationException("Project name is invalid.");
			}

			Logger.Log($"Current Working Directory: {Directory.GetCurrentDirectory()}");

			EProjectArchitecture architecture = Helpers.ParseArchitecture(Architecture);
			string currentDirectory = Directory.GetCurrentDirectory();

			string? cloneDir = GitService.CloneTemplate(currentDirectory, architecture, ProjectName, DeleteExisting);

			if (cloneDir == null) {
				Logger.Log("Failed to Clone template to current working directory.");
				return;
			}

			if (GitService.DeinitGit(cloneDir)) {
				Logger.Log("Cleared cached data of remote repository.");
			}

			if (SyncPackages) {
				Logger.Log("Syncing packages...");
				_ = await ProcessService.SyncDartPackages(cloneDir);
			}

			Logger.Log("Successfully cloned the template to current working directory.");
		}
	}
}
