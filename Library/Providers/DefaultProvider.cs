namespace Library.Providers
{
    public class DefaultProvider : IProvider
    {
        public IProviderSettings Settings { get; }

        public DefaultProvider()
        {
            Settings = new ProviderSettings(MessagePriority.Low);
        }

        public virtual void Write(string message)
        {

        }
    }
}