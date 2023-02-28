using System.Reflection;
using CommandLine;
using FlutterBootstrapper.Abstracts.Service;

namespace FlutterBootstrapper.Utilities {
	internal sealed class ServiceLocator {
		private readonly List<IService> Services = new();
		private bool HasInitialized;

		internal static readonly ServiceLocator Instance = new();

		private ServiceLocator() { }

		internal void InitializeServices() {
			if (HasInitialized) {
				return;
			}

			IEnumerable<Type> serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
					 .Where(x => x.GetInterfaces().Contains(typeof(IService)));

			foreach (Type type in serviceTypes) {
				IService service = Activator.CreateInstance(type).Cast<IService>();
				Register(service);
			}

			HasInitialized = true;
		}

		internal void Register<T>(T service) where T : IService {
			if (Services.Contains(service)) {
				throw new InvalidOperationException(nameof(service));
			}

			service.Initiailize();
			Services.Add(service);
		}

		internal void Reset() {
			Services.Clear();
			HasInitialized = false;

			InitializeServices();
		}

		internal T Get<T>() where T : IService {
			IService? service = Services.FirstOrDefault((service) => service is T);

			return service == null ? throw new InvalidOperationException(nameof(T)) : (T) service;
		}
	}
}
