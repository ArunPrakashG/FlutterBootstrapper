using System.Reflection;
using CommandLine;
using FlutterBootstrapper;
using FlutterBootstrapper.Commands;
using FlutterBootstrapper.Core;
using FlutterBootstrapper.Core.Services;

internal class Program {
	private static readonly List<IService> Services = new();

	private static async Task<int> Main(string[] args) {
		InitializeServices();

		Parser parser = new(with => with.AutoVersion = true);

		ParserResult<object> result = parser.ParseArguments(args, LoadVerbs());
		_ = await result.WithParsedAsync<InitCommand>(OnGenerateCommand);
		_ = await result.WithParsedAsync<BaseCommand>(OnBaseCommand);
		_ = await result.WithNotParsedAsync(OnParseError);

		return 0;
	}

	private static Type[] LoadVerbs() {
		return Assembly.GetExecutingAssembly()
				 .GetTypes()
				 .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
				 .ToArray();
	}

	private static void InitializeServices() {
		IEnumerable<Type> serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
				 .Where(x => x.GetInterfaces().Contains(typeof(IService)));

		foreach (Type type in serviceTypes) {
			IService service = Activator.CreateInstance(type).Cast<IService>();
			service.Initiailize();
			Services.Add(service);
		}
	}

	internal static T GetService<T>() where T : IService {
		IService? service = Services.FirstOrDefault((service) => service is T);

		return service == null ? throw new InvalidOperationException(nameof(T)) : (T) service;
	}

	private static async Task OnGenerateCommand(InitCommand command) {
		CommandLineService cliService = GetService<CommandLineService>();
		DirectoryService dirService = GetService<DirectoryService>();

		if (!await cliService.IsGitInstalled()) {
			Logger.Log("Git is not installed");
			return;
		}

		Logger.Log($"Current Working Directory: {Directory.GetCurrentDirectory()}");
		string? cloneDir = await cliService.CloneTemplate(Directory.GetCurrentDirectory(), Helpers.ParseArchitecture(command.Architecture), command.DeleteExisting);

		if (cloneDir == null) {
			Logger.Log("Failed to Clone template to current working directory.");
			return;
		}

		_ = dirService.DeinitGit(cloneDir);

		if (command.SyncPackages) {
			Logger.Log("Syncing packages...");
			_ = await cliService.SyncDartPackages(cloneDir);
		}

		Logger.Log("Successfully cloned the template to current working directory.");
	}

	private static async Task OnBaseCommand(BaseCommand command) {
		await Task.Delay(1);
	}

	private static async Task OnParseError(IEnumerable<Error> errors) {
		await Task.Delay(1);

		foreach (Error error in errors) {
			if (error.CanCast<UnknownOptionError>()) {
				UnknownOptionError unknownOptionError = error.Cast<UnknownOptionError>();
				Logger.Log($"{error.Tag} - {error} - {unknownOptionError.Token}");
				continue;
			}

			Logger.Log($"{error.Tag} - {error}");
		}
	}
}
