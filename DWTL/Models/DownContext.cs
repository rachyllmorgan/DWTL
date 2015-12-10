using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DWTL.Models
{
    public class DownContext : ApplicationDbContext
    {
        public virtual DbSet<DownUser> DownUsers { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
    }
}