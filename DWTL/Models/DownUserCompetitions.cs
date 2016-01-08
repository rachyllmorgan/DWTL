using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWTL.Models
{
    public class DownUserCompetitions : Competition
    {
        public Competition comps { get; set; }
        public List<DownUser> members { get; set; }
    }
}