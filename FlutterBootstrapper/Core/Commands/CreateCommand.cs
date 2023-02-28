using System.Globalization;
using System.Text;
using CommandLine;
using FlutterBootstrapper.Abstracts.Command;
using FlutterBootstrapper.Core.Architectures;
using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper.Core.Commands {
	[Verb("create")]
	internal class CreateCommand : CommandMeta, ICommand {
		[Option('c', "context", Required = true)]
		public EContext Context { get; private set; }

		[Option('n', "name", Required = true)]
		public string? Name { get; private set; }

		public async Task ProcessAsync() {
			if (string.IsNullOrEmpty(Name)) {
				throw new ArgumentNullException(nameof(Name));
			}

			CustomMVVM arch = new();

			// Find the view directory of the arch
			// create a directory under the Name
			// find logic directory of the arch
			// create a directory under the Name there as well
			string? projectRootDir = DirectoryService.GetProjectRootDirectory();

			if (string.IsNullOrEmpty(projectRootDir) || !Directory.Exists(projectRootDir)) {
				Logger.Log("Failed to discover project root directory. Verify if project exists in the current path.");
				return;
			}

			string libDir = Path.Combine(projectRootDir, "lib");
			string creationName = Name!.ToLower(CultureInfo.InvariantCulture).Replace(' ', '_');

			switch (Context) {
				case EContext.View:
					string viewDir = Path.Combine(libDir, arch.FeaturesDirectory, creationName);
					string viewWidgetsDir = Path.Combine(libDir, arch.GetViewWidgetsPath(creationName));

					Directory.CreateDirectory(viewDir);
					Directory.CreateDirectory(viewWidgetsDir);

					await File.WriteAllTextAsync(Path.Combine(viewDir, $"{creationName}_page.dart"), arch.GetViewTemplate(Name), Encoding.UTF8);
					break;
				case EContext.Service:
					string serviceDir = Path.Combine(libDir, arch.Services, creationName);

					await FlutterService.GetPubspec(projectRootDir);

					if (Directory.Exists(serviceDir)) {
						Logger.Log("Specified service directory already exists in the path.");
						return;
					}

					Directory.CreateDirectory(serviceDir);
					break;
				default:
					break;
			}
		}
	}
}
