using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWTL.Models
{
    public enum CompType { Weight, Dist_Ran }

    public class Competition : IComparable
    {
        [Key]
        public int CompetitionId { get; set; }
        public int DownUserId { get; set; }
        public string Name { get; set; }

        [Required]
        public CompType CompType { get; set; }

        // need a member that holds dates of comp
        public int Bet { get; set; }
        public int Pot { get; set; } //need to calculate this from total bets

        public virtual List<DownUser> Members { get; set; }
        public virtual List<Post> Posts { get; set; }

        public int CompareTo(object obj)
        {
            Competition a_comp = obj as Competition;

            int answer = this.Name.CompareTo(a_comp.Name);
            return answer;
        }
    }
}