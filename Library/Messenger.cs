using System;
using System.Collections.Generic;
using Library.Formatters;
using Library.Providers;

namespace Library
{
    public class Messenger
    {
        private readonly List<IProvider> _providers;
        private readonly IMessageFormatter _messageFormatter;

        public Messenger()
        {
            _messageFormatter = new MessageFormatter();
            _providers = new List<IProvider>();
        }

        public Messenger(IProvider provider) : this(provider, string.Empty)
        {
        }

        public Messenger(IProvider provider, string format) : this()
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            _messageFormatter = new MessageFormatter(format);
            _providers.Add(provider);
        }

        public Messenger(IProvider provider, IMessageFormatter messageFormatter) : this()
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (messageFormatter == null) throw new ArgumentNullException(nameof(messageFormatter));

            _messageFormatter = messageFormatter;
            _providers.Add(provider);
        }

        public Messenger(IEnumerable<IProvider> providers) : this(providers, string.Empty)
        {
        }
        public Messenger(IEnumerable<IProvider> providers, string format) : this()
        {
            _messageFormatter = new MessageFormatter(format);
            _providers = new List<IProvider>(providers);
        }

        public void Send(MessagePriority messagePriority, string text)
        {
            foreach (var provider in _providers)
            {
                //if (provider.SatisfyRule(messagePriority))
                //{

                //}

                if (provider.Settings.Priority <= messagePriority)
                {
                    Message message = new Message
                    {
                        Priority = messagePriority,
                        Text = text
                    };

                    var str = _messageFormatter.Format(message);

                    provider.Write(str);
                }

            }
        }

        public void Send(string message)
        {
            Send(MessagePriority.Low, message);
        }
    }
}