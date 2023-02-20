using System.Globalization;
using System.Text;

namespace FlutterBootstrapper.Core.Architectures {

	public readonly struct CustomMVVM : IProjectArchitecture {
		public string Name => "Mvvm";

		public string View => "view";

		public string Logic => "view_model";

		public string Navigation => "core/routes";

		public string Services => "core/services";

		public string GetViewWidgetsPath(string creationName) {
			return Path.Combine(View, creationName, "widgets");
		}

		public string GetViewTemplate(string creationName) {
			creationName = creationName.Replace("Page", "", StringComparison.InvariantCultureIgnoreCase).Replace("Widget", "", StringComparison.InvariantCultureIgnoreCase);

			StringBuilder stringBuilder = new();
			stringBuilder.AppendLine("import 'package:flutter/material.dart';")
				.AppendLine(CultureInfo.InvariantCulture, $"class {creationName}Page extends StatelessWidget {{")
				.AppendLine(CultureInfo.InvariantCulture, $"  const {creationName}Page({{super.key}});")
				.AppendLine("")
				.AppendLine("  @override")
				.AppendLine("  Widget build(BuildContext context) {")
				.AppendLine("    return Container();")
				.AppendLine("  }")
				.AppendLine("}");

			return stringBuilder.ToString();
		}

		public string GetViewWidgetTemplate(string creationName) {
			return "";
		}
	}
}