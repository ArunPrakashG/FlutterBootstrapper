namespace FlutterBootstrapper.Abstracts.Command
{
    public interface IProjectProcessor
    {
        Task<bool> OnPackagesSync();
    }
}
