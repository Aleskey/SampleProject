using System;

namespace SampleProject.Core.Models
{
    public class RateRequest
    {
        private const int MaxHours = 24;

        public RateRequest(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        public DateTimeOffset FromDate { get; }

        public DateTimeOffset ToDate { get; }

        public bool IsValid
        {
            get
            {
                var difference = ToDate - FromDate;
                return difference.Ticks > 0 && MaxHours > difference.TotalHours && FromDate.Day == ToDate.Day;
            }
        }
    }
}