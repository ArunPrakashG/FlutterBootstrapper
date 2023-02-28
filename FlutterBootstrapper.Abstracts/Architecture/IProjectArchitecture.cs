namespace FlutterBootstrapper.Abstracts.Architecture
{
    /// <summary>
    /// Contains a map of the directories in respective to the architecture of the project.
    /// <para>
    ///		The paths will be relative to the lib folder.
    /// </para>
    /// </summary>
    public interface IProjectArchitecture
    {
        Uri TemplateUri { get; }

        /// <summary>
        ///  The human readable name of the architecture.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The identifer of the application.
        /// 
        /// <para>This name will be used to select this architecture via CLI arguments.</para>
        /// <para>The value will be converted to lowercase internally and spaces will be removed from between.</para>
        /// </summary>
        string Identifier { get; }
    }
}
