using System;

namespace GiraffeTheLogger.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static double ToUnixTimestampInSeconds(this DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return timeSpan.TotalSeconds;
        }

        public static DateTime ToDateTime(this double timeStampInSeconds)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            return dateTime.AddSeconds(timeStampInSeconds).ToLocalTime();
        }
    }
}