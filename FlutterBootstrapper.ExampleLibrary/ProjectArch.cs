using FlutterBootstrapper.Abstracts.Architecture;

namespace FlutterBootstrapper.ExampleLibrary
{
    internal class ProjectArch : IProjectArchitecture
    {
        public Uri TemplateUri => new("");

        public string Name => "Example Arch";

        public string Identifier => "example_arch";
    }
}
