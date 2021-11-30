namespace Library.Providers
{
    public interface IProvider
    {
        IProviderSettings Settings { get; }
        void Write(string message);
    }
}