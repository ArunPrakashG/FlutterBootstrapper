namespace FlutterBootstrapper {
	internal static class Constants {
		internal const string PubspecFileName = "pubspec.yaml";
		internal const string GitDirectoryName = ".git";
		internal static readonly string[] IgnoredDirectories = new string[] {
			"build",
			"android",
			"ios",
		};
	}
}
