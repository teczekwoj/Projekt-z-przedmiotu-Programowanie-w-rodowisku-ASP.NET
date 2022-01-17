using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Models;


namespace LibraryMVC.Models
{
    public class FakeContext : DbContext
    {
        public FakeContext(DbContextOptions<FakeContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryItem> LibraryItems { get; set; }

        public DbSet<LibraryApi.Models.LibraryItem> LibraryItem{ get; set; }
    }
}