using System.Globalization;
using System.Text;
using FlutterBootstrapper.Abstracts.Architecture;

namespace FlutterBootstrapper.Core.Architectures {

	public readonly struct CustomMVVM : IProjectArchitecture {
		public string Name => "Mvvm";

		public string FeaturesDirectory => "view";

		public string Routing => "view_model";

		public string Navigation => "core/routes";

		public string Services => "core/services";

		public Uri TemplateUri => throw new NotImplementedException();

		public string Identifier => throw new NotImplementedException();

		public string GetViewWidgetsPath(string creationName) {
			return Path.Combine(FeaturesDirectory, creationName, "widgets");
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

		public string GetViewModelTemplate(string creationName) {
			return "";
		}
	}
}
