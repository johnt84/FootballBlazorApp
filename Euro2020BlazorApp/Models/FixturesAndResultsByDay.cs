using System;
using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class FixturesAndResultsByDay
    {
        public DateTime FixtureDate { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }
}
