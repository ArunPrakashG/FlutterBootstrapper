namespace FlutterBootstrapper.Core.Services {
	internal class FlutterService : IService {
		internal bool IsFlutterDirectory(string directory) {
			EnumerationOptions options = new() {
				AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseSensitive,
			};

			return Directory.Exists(directory) && Directory.EnumerateFiles(directory, Constants.PubspecFileName, options).Any();
		}

		public void Initiailize() {

		}
	}
}
