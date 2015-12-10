using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DWTL.Models
{
    public class DownUser
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(15)]
        [MinLength(5)]
        [RegularExpression(@"^[a-zA-Z\d]+[-_a-zA-Z\d]{0,2}[a-zA-Z\d]+")]
        public string Handle { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Weight { get; set; }
        public string Picture { get; set; }

        public List<Competition> Competition { get; set; }

    }
}