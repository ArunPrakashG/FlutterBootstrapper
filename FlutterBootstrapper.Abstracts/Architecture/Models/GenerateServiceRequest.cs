using FlutterBootstrapper.Abstracts.Operators;

namespace FlutterBootstrapper.Abstracts.Architecture.Models
{
    public sealed class GenerateServiceRequest : RequestBase
    {
        public GenerateServiceRequest(IDirectoryOperator directoryOperator, ITemplateRepository templateRepository, IProjectOperator projectOperator) : base(directoryOperator, templateRepository, projectOperator)
        {
        }
    }
}
