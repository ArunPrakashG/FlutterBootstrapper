using FlutterBootstrapper.Core.Services;
using FlutterBootstrapper.Utilities;

namespace FlutterBootstrapper.Core.Commands {
	internal abstract class CommandMeta {
		protected ProcessService ProcessService => ServiceLocator.Instance.Get<ProcessService>();

		protected DirectoryService DirectoryService => ServiceLocator.Instance.Get<DirectoryService>();

		protected FlutterService FlutterService => ServiceLocator.Instance.Get<FlutterService>();

		protected GitService GitService => ServiceLocator.Instance.Get<GitService>();
	}
}
