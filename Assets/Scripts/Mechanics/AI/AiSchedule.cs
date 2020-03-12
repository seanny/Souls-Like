using System;

namespace SoulsLike
{
    public enum DayOfWeek
    {
        Any,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
        Weekdays, // Monday to Friday
        Weekends // Saturday & Sunday
    };

    public enum Month
    {
        Any,
        Janurary,
        Feburary,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
        Winter, // Dec, Jan, Feb
        Spring, // March, April, May
        Summer, // June, July, August
        Autumn // September, October, November
    }

    [Serializable]
    public class AiSchedule
    {
        public DayOfWeek dayOfWeek;
        public Month month;
        public int hour = -1;
        public int minute = -1;
        public float duration = -1;

        /// <summary>
        /// Ai Schedule Constructor
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="month"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="duration"></param>
        public AiSchedule(DayOfWeek dayOfWeek, Month month, int hour, int minute, float duration)
        {
            this.dayOfWeek = dayOfWeek;
            this.month = month;
            this.hour = hour;
            this.minute = minute;
            this.duration = duration;
        }
    }
}
