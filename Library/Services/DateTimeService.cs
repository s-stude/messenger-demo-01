using System;

namespace Library.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetToday()
        {
            return DateTime.Today;
        }
    }
}