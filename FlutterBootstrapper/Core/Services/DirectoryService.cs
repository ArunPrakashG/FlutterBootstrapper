namespace FlutterBootstrapper.Core.Services {
	internal class DirectoryService : IService {
		// TODO:
		// - Scan current working directory
		// - Traverse to find root flutter directory
		// - cd to the root directory
		// - return the dir path

		public string? GetProjectRootDirectory() {
			return Directory.EnumerateFiles(Directory.GetCurrentDirectory(), Constants.PubspecFileName, new EnumerationOptions() {
				AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseSensitive,
				RecurseSubdirectories = true,
				MaxRecursionDepth = 20,

			}).FirstOrDefault();
		}

		public bool DeinitGit(string directory) {
			if (!Directory.Exists(directory)) {
				throw new DirectoryNotFoundException();
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

		public bool IsDartProjectDirectory(string directory) {
			if (!Directory.Exists(directory)) {
				return false;
			}

			return Directory.EnumerateFiles(directory).Any((x) => Path.GetFileName(x).Equals(Constants.PubspecFileName, StringComparison.Ordinal));
		}

		public bool IsGitDirectory(string directory) {
			if (!Directory.Exists(directory)) {
				return false;
			}

			foreach (string dir in Directory.EnumerateDirectories(directory)) {
				string? name = Path.GetFileName(dir);

				if (string.IsNullOrEmpty(name)) {
					continue;
				}

				if (name.Equals(Constants.GitDirectoryName, StringComparison.Ordinal)) {
					return true;
				}
			}

			return false;
		}

		public void Initiailize() {

		}
	}
}
