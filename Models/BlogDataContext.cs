using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Models
{
    // Inherits the Entity Framework database context 'DbContext',
    // this is using Microsoft.EntityFrameworkCore.
    public class BlogDataContext : DbContext
    {
        // DbSet is provided by Entity Framework, and exposes a table in the
        // database.
        public DbSet<Post> Posts { get; set; }

        public BlogDataContext(DbContextOptions<BlogDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
