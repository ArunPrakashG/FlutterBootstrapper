using FlutterBootstrapper.Core.Services;

namespace FlutterBootstrapper.Core.Commands {
	internal abstract class CommandDependencies {
		protected ProcessService ProcessService => Dependencies.Instance.Get<ProcessService>();

		protected DirectoryService DirectoryService => Dependencies.Instance.Get<DirectoryService>();

		protected FlutterService FlutterService => Dependencies.Instance.Get<FlutterService>();

		protected GitService GitService => Dependencies.Instance.Get<GitService>();
	}
}
