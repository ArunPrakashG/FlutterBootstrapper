namespace FlutterBootstrapper.Abstracts.Operators
{
    public interface IDirectoryOperator : IOperator
    {
        public Task<bool> CreateDirectory(string targetPath, bool replace = false);

        public Task<string> GetDirectory(string query);

        public Task<bool> WriteAsync(string targetPath, string contents, bool replace = true);

        public Task<byte[]> ReadAsync(string filePath);
    }
}
