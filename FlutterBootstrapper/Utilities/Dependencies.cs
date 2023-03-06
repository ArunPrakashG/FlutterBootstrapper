using System.Composition.Convention;
using System.Composition.Hosting;
using System.Reflection;
using CommandLine;

namespace FlutterBootstrapper.Utilities {
	internal sealed class Dependencies<T> {
		private readonly List<T> DependenciesCollection = new();
		private static readonly SemaphoreSlim Semaphore = new(1, 1);
		private bool HasInitialized;

		internal async Task LoadExternal(string path) {
			if (HasInitialized) {
				return;
			}

			foreach (T? instance in await FromPath(path).ConfigureAwait(false)) {
				Register(instance);
			}

			HasInitialized = true;
		}

		internal void LoadInternal() {
			if (HasInitialized) {
				return;
			}

			IEnumerable<Type> serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
					 .Where(x => x.GetInterfaces().Contains(typeof(T)));

			foreach (Type type in serviceTypes) {
				T service = Activator.CreateInstance(type).Cast<T>();
				Register(service);
			}

			HasInitialized = true;
		}

		internal List<T> GetAll() {
			return DependenciesCollection;
		}

		internal void Register(T service) {
			if (DependenciesCollection.Contains(service)) {
				throw new InvalidOperationException(nameof(service));
			}

			DependenciesCollection.Add(service);
		}

		internal void Clear() {
			DependenciesCollection.Clear();
			HasInitialized = false;
		}

		internal T? Get(Func<T, bool> predicate) {
			T? service = DependenciesCollection.FirstOrDefault(predicate);

			return service;
		}

		private async Task<IEnumerable<T>> FromPath(string path) {
			HashSet<Assembly>? assemblies = LoadAssembliesFromPath(path);

			if (assemblies == null || assemblies.Count <= 0) {
				Console.WriteLine($"No assemblies found in path {path}");
				return Enumerable.Empty<T>();
			}

			await Semaphore.WaitAsync().ConfigureAwait(false);

			try {
				ConventionBuilder conventions = new();
				conventions.ForTypesDerivedFrom<T>().Export<T>();
				ContainerConfiguration configuration = new ContainerConfiguration().WithAssemblies(assemblies, conventions);

				using CompositionHost container = configuration.CreateContainer();
				return container.GetExports<T>();
			} catch (Exception e) {
				Console.WriteLine(e);
				return Enumerable.Empty<T>();
			} finally {
				Semaphore.Release();
			}
		}

		private static HashSet<Assembly>? LoadAssembliesFromPath(string path) {
			if (string.IsNullOrEmpty(path)) {
				Logger.Log(nameof(path));
				return null;
			}

			if (!Directory.Exists(path)) {
				return null;
			}

			HashSet<Assembly> assemblies = new();

			foreach (string assemblyPath in Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)) {
				try {
					assemblies.Add(Assembly.LoadFrom(assemblyPath));
				} catch (Exception e) {
					Console.WriteLine(e);
					continue;
				}
			}

			return assemblies;
		}
	}
}
