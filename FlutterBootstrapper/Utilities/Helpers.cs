using System.Globalization;
using System.Reflection;
using FlutterBootstrapper.Utilities.Attributes;
using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper.Utilities {
	internal static class Helpers {
		public static string? GetStringValue(this Enum value) {
			FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

			if ((fieldInfo?.GetCustomAttributes(typeof(StringValueAttribute), false)) is StringValueAttribute[] attribs) {
				return attribs.First().Value ?? "";
			}

			return null;
		}
		public static void DeleteDirectory(string targetDir) {
			File.SetAttributes(targetDir, FileAttributes.Normal);

			string[] files = Directory.GetFiles(targetDir);
			string[] dirs = Directory.GetDirectories(targetDir);

			foreach (string file in files) {
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			foreach (string dir in dirs) {
				DeleteDirectory(dir);
			}

			Directory.Delete(targetDir, false);
		}

		public static EProjectArchitecture ParseArchitecture(string? architecture) {
			if (architecture == null) {
				return EProjectArchitecture.MVM;
			}

			return architecture.ToLower(CultureInfo.InvariantCulture) switch {
				"tdd" => EProjectArchitecture.TDD,
				"ddd" => EProjectArchitecture.DDD,
				_ => EProjectArchitecture.MVM,
			};
		}
	}
}
