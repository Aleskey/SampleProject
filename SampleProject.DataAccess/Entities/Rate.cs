using System;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.DataAccess.Entities
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public decimal Price { get; set; }
    }
}