using FlutterBootstrapper.Abstracts.Service;
using FlutterBootstrapper.Utilities;
using FlutterBootstrapper.Utilities.Models;

namespace FlutterBootstrapper.Core.Services {
	internal class DirectoryService : IService {
		// TODO:
		// - Scan current working directory
		// - Traverse to find root flutter directory
		// - cd to the root directory
		// - return the dir path

		internal bool IsFlutterDirectory(string directory) {
			EnumerationOptions options = new() {
				AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseSensitive,
			};

			return Directory.Exists(directory) && Directory.EnumerateFiles(directory, Constants.PubspecFileName, options).Any();
		}

		internal string HasDirectory(string target, string directoryName) {
			return Directory.EnumerateDirectories(target, directoryName, new EnumerationOptions() {
				AttributesToSkip = FileAttributes.System | FileAttributes.Offline | FileAttributes.Encrypted,
				RecurseSubdirectories = true,
				ReturnSpecialDirectories = false,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseInsensitive,
			}).Select((x) => new DataDirectory(x)).First((x) => !Constants.IgnoredDirectories.Contains(x.DirectoryName)).DirectoryPath;
		}

		internal IEnumerable<DataDirectory> TraverseDirectory(string directory) {
			return Directory.EnumerateDirectories(directory, "*", new EnumerationOptions() {
				AttributesToSkip = FileAttributes.System | FileAttributes.Offline | FileAttributes.Encrypted,
				RecurseSubdirectories = true,
				ReturnSpecialDirectories = false,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseInsensitive,
			}).Select((x) => new DataDirectory(x)).Where((x) => !Constants.IgnoredDirectories.Contains(x.DirectoryName));
		}

		public string? GetProjectRootDirectory() {
			IEnumerable<string> enumeratedResults = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), Constants.PubspecFileName, new EnumerationOptions() {
				AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
				IgnoreInaccessible = true,
				MatchCasing = MatchCasing.CaseSensitive,
				RecurseSubdirectories = true,
				MaxRecursionDepth = 20,
			});

			if (!enumeratedResults.Any()) {
				return ServiceLocator.Instance.Get<GitService>().DiscoverRepository();
			}

			if (enumeratedResults.Count() > 1) {
				throw new InvalidOperationException("There are multiple repositories in your current working directory.");
			}

			return Path.GetDirectoryName(enumeratedResults.FirstOrDefault());
		}

		public bool IsDartProjectDirectory(string directory) {
			if (!Directory.Exists(directory)) {
				return false;
			}

			return Directory.EnumerateFiles(directory)
				   .Any(predicate: (x) => Path.GetFileName(x).Equals(Constants.PubspecFileName, StringComparison.Ordinal));
		}

		public void Initiailize() {

		}
	}
}
