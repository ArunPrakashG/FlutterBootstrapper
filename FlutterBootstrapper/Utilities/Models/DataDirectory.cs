namespace FlutterBootstrapper.Utilities.Models {
	internal readonly struct DataDirectory {
		internal readonly string DirectoryPath;

		internal string DirectoryName => new DirectoryInfo(DirectoryPath).Name;

		public DataDirectory(string path) => DirectoryPath = path ?? throw new ArgumentNullException(nameof(path));

		internal DataDirectory? GetParent() {
			DirectoryInfo? parent = Directory.GetParent(DirectoryPath);

			if (parent == null) {
				return null;
			}

			return new DataDirectory(parent.FullName);
		}

		internal IEnumerable<DataDirectory> GetSubdirectories() {
			return Directory.EnumerateDirectories(DirectoryPath, "*", new EnumerationOptions() {
				AttributesToSkip = FileAttributes.System | FileAttributes.Offline | FileAttributes.Encrypted,
				RecurseSubdirectories = false,
				ReturnSpecialDirectories = false,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseInsensitive,
			}).Select((x) => new DataDirectory(x)).Where((x) => !Constants.IgnoredDirectories.Contains(x.DirectoryName));
		}
	}
}
