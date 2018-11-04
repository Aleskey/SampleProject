﻿using System.Collections.Generic;
using System.Linq;

namespace SampleProject.Data
{
    public class RateCollection
    {
        public RateCollection()
        {
            Rates = Enumerable.Empty<Rate>();
        }

        public IEnumerable<Rate> Rates { get; set; }
    }
}
