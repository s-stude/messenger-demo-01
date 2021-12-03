using Library.Services;

namespace Library.Formatters
{
    public class MessageFormatter : IMessageFormatter
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly string _messageFormat; // "{date} {message}"

        public MessageFormatter()
        {
            _messageFormat = "{priority} {date} {message}";
            _dateTimeService = new DateTimeService();
        }

        public MessageFormatter(string messageFormat, IDateTimeService dateTimeService = null) : this()
        {
            _dateTimeService = dateTimeService ?? new DateTimeService();

            if (string.IsNullOrWhiteSpace(messageFormat) == false)
            {
                _messageFormat = messageFormat;
            }
        }

        public string Format(Message message)
        {
            var res = _messageFormat;

            var date = _dateTimeService.GetToday().ToString("yyyy-MM-dd");
            res = res.Replace("{date}", date);

            res = res.Replace("{message}", message.Text);

            res = res.Replace("{priority}", message.Priority.ToString().ToUpper());

            return res;
        }
    }
}