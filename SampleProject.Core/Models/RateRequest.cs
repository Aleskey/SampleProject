using System;

namespace SampleProject.Core.Models
{
    public class RateRequest
    {
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
                return (ToDate - FromDate).Ticks > 0 && FromDate.Day == ToDate.Day;
            }
        }
    }
}