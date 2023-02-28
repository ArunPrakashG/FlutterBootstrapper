namespace FlutterBootstrapper.Abstracts.Architecture.Models
{
    public sealed class GenerateResult
    {
        public readonly bool IsSuccess;
        public readonly string ResultPath;
        public readonly Exception? Exception;

        public GenerateResult(bool isSuccess, string resultPath, Exception? exception)
        {
            IsSuccess = isSuccess;
            ResultPath = resultPath;
            Exception = exception;
        }
    }
}
