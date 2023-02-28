using FlutterBootstrapper.Abstracts.Architecture.Models;

namespace FlutterBootstrapper.Abstracts.Command
{
    public interface ICreateProcessor
    {
        Task<GenerateResult> OnGenerateFeature(GenerateFeatureRequest request);

        Task<GenerateResult> OnGenerateService(GenerateServiceRequest request);
    }
}
