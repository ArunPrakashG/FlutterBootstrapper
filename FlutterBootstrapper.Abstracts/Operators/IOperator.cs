namespace FlutterBootstrapper.Abstracts.Operators
{
    public interface IOperator
    {
        protected string BaseDirectory { get; set; }

        protected Task Initialize(string baseDirectory);
    }
}
