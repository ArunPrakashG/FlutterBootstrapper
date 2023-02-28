namespace FlutterBootstrapper.Abstracts.Operators
{
    public interface IProjectOperator : IOperator
    {
        public Task<bool> SyncPackages();
    }
}
