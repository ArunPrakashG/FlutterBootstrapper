using FlutterBootstrapper.Abstracts.Architecture.Models;
using FlutterBootstrapper.Abstracts.Command;

namespace FlutterBootstrapper.ExampleLibrary
{
    internal class CommandProcessor : ICreateProcessor, IProjectProcessor
    {
        public Task<GenerateResult> OnGenerateFeature(GenerateFeatureRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GenerateResult> OnGenerateService(GenerateServiceRequest request)
        {
            throw new NotImplementedException();
        }

        public Task OnPackagesSync(string projectDirectory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> OnPackagesSync()
        {
            throw new NotImplementedException();
        }

        public Task OnTemplateCloned(string clonedDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
