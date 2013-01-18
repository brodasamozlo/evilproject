using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EvilProject.Models
{
    public partial class EP_DB : DbContext
    {
        public EP_DB()
            : base("name=EP_DB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<PageNews>();
            modelBuilder.Entity<TODO>();
            //throw new UnintentionalCodeFirstException();
        }

        public DbSet<PageNews> PageNewses { get; set; }
        public DbSet<TODO> TODOes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}