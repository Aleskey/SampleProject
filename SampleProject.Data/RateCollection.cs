using System.Collections.Generic;
using System.Linq;
using SampleProject.Data.Model;

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
