using FlutterBootstrapper.Utilities.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FlutterBootstrapper.Core.Services {
	internal class FlutterService : IService {

		internal async Task GetPubspec(string projectDirectory) {
			string pubspecFilePath = Path.Combine(projectDirectory, Constants.PubspecFileName);

			if (!File.Exists(pubspecFilePath)) {
				Logger.Log("Pubspec file doesn't exist in the current directory.");
				return;
			}

			string pubspecContents = await File.ReadAllTextAsync(pubspecFilePath);
			IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance)
				.IgnoreUnmatchedProperties()
				.WithDuplicateKeyChecking()
				.Build();

			PubspecDocument document = deserializer.Deserialize<PubspecDocument>(pubspecContents);
			Logger.Log(document.Flutter?.UsesMaterialDesign.ToString() ?? "NA");
		}

		public void Initiailize() {

		}
	}
}
