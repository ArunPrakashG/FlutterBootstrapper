using FlutterBootstrapper.Commands;
using FlutterBootstrapper.Core;

internal class Program {
	private static readonly CommandParser CommandParser = new();

	private static async Task<int> Main(string[] args) {
		ServiceLocator.Instance.InitializeServices();
		return await CommandParser.ExecuteAsync(args);
	}
}
