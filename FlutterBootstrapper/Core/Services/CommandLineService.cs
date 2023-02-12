using CliWrap;
using FlutterBootstrapper.Exceptions;
using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper.Core.Services {
	internal class CommandLineService : IService {
		private DirectoryService DirectoryService => Program.GetService<DirectoryService>();

		internal async Task<bool> IsGitInstalled() {
			CommandResult result = await Cli.Wrap("git").WithArguments("--version").ExecuteAsync();

			return result.ExitCode == 0;
		}

		internal async Task<bool> IsFlutterInstalled() {
			CommandResult result = await Cli.Wrap("flutter").WithArguments("help").ExecuteAsync();

			return result.ExitCode == 0;
		}

		internal async Task<bool> CreateGitBranch(string branchName, string gitDirectory) {
			if (!await IsGitInstalled()) {
				return false;
			}

			if (!Directory.Exists(gitDirectory)) {
				throw new DirectoryNotFoundException();
			}

			if (!DirectoryService.IsGitDirectory(gitDirectory)) {
				throw new InvalidOperationException("Not a git directory: " + gitDirectory);
			}

			CommandResult result = await Cli.Wrap("git").WithArguments($"branch {branchName}").ExecuteAsync();

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

		internal async Task<string?> CloneTemplate(string toDirectory, EProjectArchitecture arch, bool deleteExisting = false) {
			if (!await IsGitInstalled()) {
				throw new GitNotInstalledException();
			}

			if (!Directory.Exists(toDirectory)) {
				throw new DirectoryNotFoundException();
			}

			Uri templateUri = RepositoryTemplates.GetTemplateUrl(arch);
			string dirName = templateUri.Segments.Last();
			string newDirPath = Path.Combine(toDirectory, dirName);

			if (Directory.Exists(newDirPath)) {
				if (!deleteExisting) {
					throw new InvalidOperationException();
				}

				Helpers.DeleteDirectory(newDirPath);
			}

			CommandResult result = await Cli.Wrap("git").WithWorkingDirectory(toDirectory).WithArguments($"clone {templateUri}").ExecuteAsync();

			if (result.ExitCode != 0) {
				return null;
			}

			return !Directory.Exists(newDirPath) ? throw new DirectoryNotFoundException(newDirPath) : newDirPath;
		}

		public void Initiailize() {

		}
	}
}
