using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DWTL.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string Content { get; set; }

        public object Author { get; set; }
        public DateTime Date { get; set; }
        public Competition Competition { get; set; }
        public string Picture { get; set; }
    }
}