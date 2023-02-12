namespace FlutterBootstrapper {
	internal static class Logger {
		internal static void Log(string message) {
			string logMessage = $"{DateTime.Now} | {message}";
			Console.WriteLine(logMessage);
		}
	}
}
