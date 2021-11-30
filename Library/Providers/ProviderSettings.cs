namespace Library.Providers
{
    public class ProviderSettings : IProviderSettings
    {
        public ProviderSettings(MessagePriority messagePriority)
        {
            Priority = messagePriority;
        }

        public MessagePriority Priority { get; }
    }
}