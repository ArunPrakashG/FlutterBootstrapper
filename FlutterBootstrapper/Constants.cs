using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper {
	internal class Constants {
		internal const string PubspecFileName = "pubspec.yaml";
		internal const string GitDirectoryName = ".git";
	}

	internal class RepositoryTemplates {
		private const string CleanArchitecture = "https://github.com/ahmedtalal/flutter-clean-architecture";
		private const string Mvvm = "https://github.com/ArunPrakashG/mgu_result_checker";

		internal static Uri GetTemplateUrl(EProjectArchitecture arch) {
			switch (arch) {
				case EProjectArchitecture.MVM:
					return new Uri(Mvvm);
				case EProjectArchitecture.TDD:
					return new Uri(CleanArchitecture);
				case EProjectArchitecture.DDD:
				default:
					goto case EProjectArchitecture.MVM;
			}
		}
	}
}
