using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWTL.Models
{
    public class Competition
    {
        [Key]
        public int CompetitionId { get; set; }

        public string Name { get; set; }
        public object Author { get; set; }
        public int Bet { get; set; }
        public int Pot { get; set; } //need to calculate this from total bets

        public List<Post> Posts { get; set; }
    }
}