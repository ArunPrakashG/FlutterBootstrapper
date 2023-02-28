using YamlDotNet.Serialization;

namespace FlutterBootstrapper.Abstracts.Architecture.Models
{
    [YamlSerializable]
    public sealed class PubspecDocument
    {
        [YamlMember]
        public string? Name { get; private set; }

        [YamlMember]
        public string? Description { get; private set; }

        [YamlMember]
        public string? PublishTo { get; private set; }

        [YamlMember]
        public string? Version { get; private set; }

        [YamlMember]
        public ProjectEnvironment? Environment { get; private set; }

        [YamlMember]
        public Dictionary<string, object>? Dependencies { get; private set; }

        [YamlMember]
        public Dictionary<string, object>? DevDependencies { get; private set; }

        [YamlMember]
        public FlutterConfiguration? Flutter { get; private set; }
    }

    [YamlSerializable]
    public sealed class FlutterConfiguration
    {
        [YamlMember]
        public bool UsesMaterialDesign { get; private set; } = false;
    }

    [YamlSerializable]
    public sealed class ProjectEnvironment
    {
        [YamlMember]
        public string? Sdk { get; private set; }
    }
}
