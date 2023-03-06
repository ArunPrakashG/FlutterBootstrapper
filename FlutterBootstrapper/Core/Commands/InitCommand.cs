using CommandLine;
using FlutterBootstrapper.Abstracts.Architecture;
using FlutterBootstrapper.Abstracts.Command;
using FlutterBootstrapper.Core.Services;
using FlutterBootstrapper.Utilities;
using FlutterBootstrapper.Utilities.Models;

namespace FlutterBootstrapper.Core.Commands {
	[Verb("init")]
	internal class InitCommand : CommandDependencies, ICommand {
		[Option('n', "name", Required = true)]
		public string? ProjectName { get; private set; }

		[Option('a', "arch")]
		public string? Architecture { get; private set; }

		[Option("packages", Separator = ',')]
		public IEnumerable<string>? Packages { get; private set; }

		[Option("dev-packages", Separator = ',')]
		public IEnumerable<string>? DevPackages { get; private set; }

		[Option("delete-existing", Default = false)]
		public bool DeleteExisting { get; private set; }

		[Option("sync-packages", Default = false)]
		public bool SyncPackages { get; private set; }

		public async Task ProcessAsync() {
			if (string.IsNullOrEmpty(ProjectName)) {
				throw new ArgumentNullException(nameof(ProjectName));
			}

			Logger.Log($"Current Working Directory: {Directory.GetCurrentDirectory()}");

			Architecture = Architecture?.Trim().Replace(" ", "_");

			Dependencies<IProjectArchitecture> projectArcs = new();
			string currentDirectory = Directory.GetCurrentDirectory();
			await projectArcs.LoadExternal(Path.Combine(Directory.GetCurrentDirectory(), "Architectures"));

			IProjectArchitecture? selectedArch = projectArcs.Get((arch) => arch.Identifier.Replace(" ", "_").Equals(Architecture, StringComparison.OrdinalIgnoreCase));

			if (selectedArch == null) {
				Logger.Log("Selected Architecture is invalid.");
				return;
			}

			string? cloneDir = GitService.CloneTemplate(currentDirectory, ProjectName, selectedArch.TemplateUri, DeleteExisting);

			if (cloneDir == null) {
				Logger.Log("Failed to Clone template to current working directory.");
				return;
			}

			if (GitService.DeinitGit(cloneDir)) {
				Logger.Log("Cleared cached data of remote repository.");
			}

			foreach (DataDirectory dir in DirectoryService.TraverseDirectory(Path.Combine(cloneDir, "lib"))) {
				Logger.Log(dir.DirectoryName);
			}

			// TODO BUG: SyncPackages always returns true
			if (SyncPackages) {
				Logger.Log("Syncing packages...");
				await ProcessService.SyncDartPackages(cloneDir);
			}

			Logger.Log("Successfully cloned the template to current working directory.");
		}
	}
}
