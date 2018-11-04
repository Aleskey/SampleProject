using System;

namespace SampleProject.DataAccess.Entities
{
    public class Rate : Entity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public decimal Price { get; set; }
    }
}