using System;

namespace SampleProject.Models
{
    /// <summary>
    /// Represents rate model
    /// </summary>
    public class RateModel
    {
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        public DateTime ToDate { get; set; }
    }
}
