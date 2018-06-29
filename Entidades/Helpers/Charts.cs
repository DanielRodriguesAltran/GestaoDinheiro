using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Helpers
{
    public class MorrisBarChart
    {
        public int? id { get; set; }
        public string label { get; set; }
        public double value { get; set; }

    }
    public class MovementsMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}