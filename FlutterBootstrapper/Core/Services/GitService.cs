using System.Globalization;
using LibGit2Sharp;
using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper.Core.Services {
	internal sealed class GitService : IService {
		internal string Clone(string url, string directory) {
			CloneOptions cloneOptions = new() {
				IsBare = false,
				RecurseSubmodules = true,
				// We can later on add support for custom credentials via an auth command
				// We will then save this credentials on a local db for further requests
				// CredentialsProvider = (url, userFromUrl, type) => new DefaultCredentials(),
			};

			return Repository.Clone(url, directory, cloneOptions);
		}

		internal void CreateGitBranch(string branchName, string gitDirectory) {
			if (!Directory.Exists(gitDirectory)) {
				throw new DirectoryNotFoundException();
			}

			if (!IsGitRepository(gitDirectory)) {
				throw new InvalidOperationException("Not a git directory: " + gitDirectory);
			}

			_ = new Repository(gitDirectory).CreateBranch(branchName);
		}

		internal string? CloneTemplate(string directory, EProjectArchitecture arch, string projectName, bool deleteExisting = false) {
			if (!Directory.Exists(directory)) {
				throw new DirectoryNotFoundException();
			}

			projectName = projectName.Trim().ToLower(CultureInfo.InvariantCulture).Replace(' ', '_');

			Uri templateUri = RepositoryTemplates.GetTemplateUrl(arch);
			string tempDirectoryName = Guid.NewGuid().ToString("N");
			string tempDirectory = Path.Combine(directory, tempDirectoryName);

			_ = Clone(templateUri.ToString(), tempDirectory);

			if (tempDirectory == null || !Directory.Exists(tempDirectory)) {
				throw new DirectoryNotFoundException(tempDirectory);
			}

			string projectDirectory = Path.Combine(directory, projectName);

			if (Directory.Exists(projectDirectory)) {
				if (deleteExisting) {
					throw new InvalidOperationException($"Directory already exists. {projectDirectory}");
				}

				Directory.Delete(projectDirectory, true);
			}

			Directory.Move(tempDirectory, projectDirectory);

			return projectDirectory;
		}

		public bool DeinitGit(string directory) {
			if (!Directory.Exists(directory)) {
				throw new DirectoryNotFoundException();
			}

			if (!IsGitRepository(directory)) {
				throw new InvalidOperationException($"{directory} is not a git repository.");
			}

			foreach (string dir in Directory.EnumerateDirectories(directory)) {
				string? name = Path.GetFileName(dir);

				if (string.IsNullOrEmpty(name)) {
					continue;
				}

				if (name.Equals(Constants.GitDirectoryName, StringComparison.Ordinal)) {
					Helpers.DeleteDirectory(dir);
					return true;
				}
			}

			return false;
		}

		internal string? DiscoverRepository() {
			return Repository.Discover(Directory.GetCurrentDirectory());
		}

		internal string? DiscoverRepository(string directory) {
			return Repository.Discover(directory);
		}

		internal bool IsGitRepository() {
			return Repository.IsValid(Directory.GetCurrentDirectory());
		}

		internal bool IsGitRepository(string directory) {
			return Repository.IsValid(directory);
		}

		public void Initiailize() {

		}
	}
}
