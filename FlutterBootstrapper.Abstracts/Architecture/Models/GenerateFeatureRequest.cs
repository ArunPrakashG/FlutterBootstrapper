using FlutterBootstrapper.Abstracts.Operators;

namespace FlutterBootstrapper.Abstracts.Architecture.Models
{
    public sealed class GenerateFeatureRequest : RequestBase
    {
        public GenerateFeatureRequest(IDirectoryOperator directoryOperator, ITemplateRepository templateRepository, IProjectOperator projectOperator) : base(directoryOperator, templateRepository, projectOperator)
        {
        }
    }
}
