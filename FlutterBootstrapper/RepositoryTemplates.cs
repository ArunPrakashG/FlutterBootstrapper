using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper {
	internal class RepositoryTemplates {
		private const string CleanArchitecture = "https://github.com/ahmedtalal/flutter-clean-architecture";
		private const string Mvvm = "https://github.com/jitsm555/Flutter-MVVM";

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
