using FlutterBootstrapper.Abstracts.Operators;

namespace FlutterBootstrapper.Abstracts.Architecture.Models
{
    public abstract class RequestBase
    {
        public readonly IDirectoryOperator DirectoryOperator;
        public readonly ITemplateRepository TemplateRepository;
        public readonly IProjectOperator ProjectOperator;

        protected RequestBase(IDirectoryOperator directoryOperator, ITemplateRepository templateRepository, IProjectOperator projectOperator)
        {
            DirectoryOperator = directoryOperator ?? throw new ArgumentNullException(nameof(directoryOperator));
            TemplateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
            ProjectOperator = projectOperator ?? throw new ArgumentNullException(nameof(projectOperator));
        }
    }
}
