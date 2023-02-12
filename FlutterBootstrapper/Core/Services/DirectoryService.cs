namespace FlutterBootstrapper.Core.Services {
	internal class DirectoryService : IService {
		// TODO:
		// - Scan current working directory
		// - Traverse to find root flutter directory
		// - cd to the root directory
		// - return the dir path

		public string? GetProjectRootDirectory() {
			string? enumeratedResult = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), Constants.PubspecFileName, new EnumerationOptions() {
				AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseSensitive,
				RecurseSubdirectories = true,
				MaxRecursionDepth = 20,

			}).FirstOrDefault();

			return enumeratedResult ?? ServiceLocator.Instance.Get<GitService>().DiscoverRepository();
		}

		public bool IsDartProjectDirectory(string directory) {
			if (!Directory.Exists(directory)) {
				return false;
			}

			return Directory.EnumerateFiles(directory)
				   .Any(predicate: (x) => Path.GetFileName(x).Equals(Constants.PubspecFileName, StringComparison.Ordinal));
		}

		public void Initiailize() {

		}
	}
}
