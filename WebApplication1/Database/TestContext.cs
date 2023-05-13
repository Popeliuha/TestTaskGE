using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Database
{
    public class TestEntitie
    {
        public int Id { get; set; }
        public string SavedText { get; set; }
    }
    public class TestContext : DbContext
    {
        public TestContext()
            : base(@"Server=DESKTOP-0OQGPH0\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;")
        {
        }

        public virtual DbSet<TestEntitie> TestEntitiesDB { get; set; }
    }
}