using System;
using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class FixturesByDay
    {
        public DateTime FixtureDate { get; set; }
        public List<Fixture> Fixtures { get; set; }
    }
}
