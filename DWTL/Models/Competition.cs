using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWTL.Models
{
    public class Competition : IComparable
    {
        [Key]
        public int CompetitionId { get; set; }

        public string Name { get; set; }
        public object Author { get; set; }
        public int Bet { get; set; }
        public int Pot { get; set; } //need to calculate this from total bets

        public List<Post> Posts { get; set; }

        public int CompareTo(object obj)
        {
            Competition a_comp = obj as Competition;

            int answer = this.Name.CompareTo(a_comp.Name);
            return answer;
        }
    }
}