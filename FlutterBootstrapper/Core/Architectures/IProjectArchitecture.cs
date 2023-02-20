namespace FlutterBootstrapper.Core.Architectures {
	/// <summary>
	/// Contains a map of the directories in respective to the architecture of the project.
	/// <para>
	///		The paths will be relative to the lib folder.
	/// </para>
	/// </summary>
	public interface IProjectArchitecture {
		/// <summary>
		///  The name of the architecture.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The path corresponding to the User Interface Page file of the architecture.
		/// </summary>
		string View { get; }

		/// <summary>
		/// The path corresponding to the Widgets associated with a particular view of the architecture.
		/// </summary>
		string GetViewWidgetsPath(string creationName);

		/// <summary>
		/// The path corresponding to the Business Logic file of the architecture.
		/// </summary>
		string Logic { get; }

		/// <summary>
		/// The path corresponding to the Navigation file of the architecture.
		/// </summary>
		string Navigation { get; }

		/// <summary>
		/// The path corresponding to the Services directory of the architecture.
		/// </summary>
		string Services { get; }

		string GetViewTemplate(string creationName);

		string GetViewWidgetTemplate(string creationName);
	}
}
