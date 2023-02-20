using FlutterBootstrapper.Utilities.Attributes;

namespace FlutterBootstrapper {
	internal sealed class Enums {
		internal enum EProjectArchitecture {
			TDD,
			DDD,
			MVM
		}

		public enum EContext : int {
			[StringValue("view")]
			View,

			[StringValue("service")]
			Service,
		}
	}
}
