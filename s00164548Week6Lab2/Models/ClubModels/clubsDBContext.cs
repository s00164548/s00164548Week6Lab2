using Rad2016SampleWepApp.Models.ClubModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace s00164548Week6Lab2.Models.ClubModels
{
    public class clubsDBContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ClubEvent> ClubEvents { get; set; }



        public clubsDBContext()
                : base("DefaultConnection")
        {
        }
    }
}