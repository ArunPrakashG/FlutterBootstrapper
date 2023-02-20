using System.Reflection;
using CommandLine;

namespace FlutterBootstrapper.Core.Commands {
	internal sealed class CommandParser {

		internal async Task<int> ExecuteAsync(string[] args) {
			Type[] commandTypes = LoadCommands();

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

		private async Task OnParseError(IEnumerable<Error> errors) {
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

		private Type[] LoadCommands() {
			return Assembly.GetExecutingAssembly()
				  .GetTypes()
				  .Where(x => x.GetInterfaces().Contains(typeof(ICommand)) && x.GetCustomAttribute<VerbAttribute>() != null)
				  .ToArray();
		}
	}
}
