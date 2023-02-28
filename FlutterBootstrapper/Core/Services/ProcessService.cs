using CliWrap;
using FlutterBootstrapper.Abstracts.Service;
using FlutterBootstrapper.Utilities;

namespace FlutterBootstrapper.Core.Services {
	internal class ProcessService : IService {
		private DirectoryService DirectoryService => ServiceLocator.Instance.Get<DirectoryService>();

		internal async Task<bool> IsFlutterInstalled() {
			CommandResult result = await Cli.Wrap("flutter").WithArguments("help").ExecuteAsync();

			return result.ExitCode == 0;
		}

		internal async Task<bool> SyncDartPackages(string projectDirectory) {
			if (!await IsFlutterInstalled()) {
				throw new InvalidOperationException("Flutter is not installed on this machine");
			}

			if (!DirectoryService.IsDartProjectDirectory(projectDirectory)) {
				throw new InvalidOperationException("Not a project directory: " + projectDirectory);
			}

			string? envVariablesMachine = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
			string? envVariablesUser = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);

			CommandResult result = await Cli.Wrap("flutter")
				.WithWorkingDirectory(projectDirectory)
				.WithEnvironmentVariables(s => s.Set("PATH", envVariablesMachine).Set("Path", envVariablesUser))
				.WithArguments(x => x.Add("pub").Add("get"))
				.ExecuteAsync();

			return result.ExitCode == 0;
		}

		public void Initiailize() {

		}
	}
}
