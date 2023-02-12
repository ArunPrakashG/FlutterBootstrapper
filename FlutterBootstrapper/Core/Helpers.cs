using System.Globalization;
using static FlutterBootstrapper.Enums;

namespace FlutterBootstrapper.Core {
	internal class Helpers {
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
