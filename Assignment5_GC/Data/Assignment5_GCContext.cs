using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment5_GC.Models;

namespace Assignment5_GC.Data
{
    public class Assignment5_GCContext : DbContext
    {
        public Assignment5_GCContext (DbContextOptions<Assignment5_GCContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment5_GC.Models.Music> Music { get; set; } = default!;
    }
}
