namespace FlutterBootstrapper {
	internal class RepositoryTemplates {
		private const string CustomMvvm = "https://github.com/ArunPrakashG/flutter_mvvm_template";

		internal static Uri GetTemplateUrl() {
			return new Uri(CustomMvvm);
		}
	}
}
