using System;

namespace Library.Formatters
{
    public class MessageFormatter
    {
        private readonly string _messageFormat; // "{date} {message}"

        public MessageFormatter()
        {
            _messageFormat = "{priority} {date} {message}";
        }

        public MessageFormatter(string messageFormat) : this()
        {
            if (string.IsNullOrWhiteSpace(messageFormat) == false)
            {
                _messageFormat = messageFormat;
            }
        }

        public string Format(Message message)
        {
            var res = _messageFormat;

            var date = DateTime.Now.ToString("yyyy-MM-dd");
            res = res.Replace("{date}", date);

            res = res.Replace("{message}", message.Text);

            res = res.Replace("{priority}", message.Priority.ToString().ToUpper());

            return res;
        }
    }
}