using KaelStore.Service.Contract;
using System;

namespace KaelStore.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}