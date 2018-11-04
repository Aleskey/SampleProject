using System;

namespace SampleProject.Data
{
    public class TimeSpanRange
    {
        public TimeSpanRange(TimeSpan fromTime, TimeSpan toTime)
        {
            if (fromTime > toTime)
            {
                throw new ArgumentException("From time cannot be greater than to time");
            }

            this.FromTime = fromTime;
            this.ToTime = toTime;
        }

        public TimeSpan FromTime { get; }

        public TimeSpan ToTime { get; }
    }
}