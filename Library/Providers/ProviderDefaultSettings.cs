using System;

namespace Library.Providers
{
    public class ProviderDefaultSettings : IProviderSettings
    {
        public ProviderDefaultSettings()
        {
            Priority = messagePriority;
            SendRule = sendRule;
        }

        public MessagePriority Priority { get; }
        public ISendRule SendRule { get; }
    }

    public interface ISendRule
    {
        bool ShouldSendFor(MessagePriority priority);
    }

    public class PriorityBasedSendRule : ISendRule
    {
        public bool ShouldSendFor(Message message, MessagePriority priority)
        {
            return message.Priority <= priority;
        }
    }

    public class SendRule
    {
        public static ISendRule OnlyOnMonday => new OnlyOnMondaySendRule();
    }

    public class OnlyOnMondaySendRule : ISendRule
    {
        public bool ShouldSendFor(MessagePriority priority)
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Monday;
        }
    }
}
