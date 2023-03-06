using System.Reflection;
using CommandLine;
using FlutterBootstrapper;
using FlutterBootstrapper.Abstracts.Architecture;
using FlutterBootstrapper.Abstracts.Command;
using FlutterBootstrapper.Utilities;

internal class Program {
	private static readonly Dependencies<IProjectArchitecture> ProjectArchitectureCache = new();

	private static string ArchitectureLibraryPath => Path.Combine(Directory.GetCurrentDirectory(), "Architectures");

	private static async Task<int> Main(string[] args) {
		await ProjectArchitectureCache.LoadExternal(ArchitectureLibraryPath);

		return await ProcessArgumentsAsync(args);
	}

	private static async Task<int> ProcessArgumentsAsync(string[] args) {
		Type[] commandTypes = LoadInternalCommands();

		using Parser parser = new(with => with.AutoVersion = true);

		try {
			ParserResult<object> result = parser.ParseArguments(args, commandTypes);

			_ = await result.WithParsedAsync(async (command) => {
				foreach (Type type in commandTypes) {
					if (type != command.GetType()) {
						continue;
					}

					await command.Cast<ICommand>().ProcessAsync();
				}
			});

			_ = await result.WithNotParsedAsync(OnParseError);

			return 0;
		} catch (Exception e) {
			Logger.Log(e.Message);
			Logger.Log(e.StackTrace ?? "Stacktrace not available.");

			return -1;
		}
	}

	private static async Task OnParseError(IEnumerable<Error> errors) {
		await Task.Delay(1);

		foreach (Error error in errors) {
			switch (error) {
				case UnknownOptionError:
					UnknownOptionError unknownOptionError = error.Cast<UnknownOptionError>();
					Logger.Log($"{unknownOptionError} | {unknownOptionError.Token}");
					break;
				default:
					Logger.Log($"{error.Tag} - {error}");
					break;
			}
		}
	}

	private static Type[] LoadInternalCommands() {
		return Assembly.GetExecutingAssembly()
			  .GetTypes()
			  .Where(x => x.GetInterfaces().Contains(typeof(ICommand)) && x.GetCustomAttribute<VerbAttribute>() != null)
			  .ToArray();
	}
}
