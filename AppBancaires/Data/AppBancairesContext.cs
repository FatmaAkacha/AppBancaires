using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppBancaires.Model;

namespace AppBancaires.Data
{
    public class AppBancairesContext : DbContext
    {
        public AppBancairesContext (DbContextOptions<AppBancairesContext> options)
            : base(options)
        {
        }

        public DbSet<AppBancaires.Model.Compte> Compte { get; set; } = default!;
        public DbSet<AppBancaires.Model.Operation> Operation { get; set; } = default!;
        public DbSet<AppBancaires.Model.Client> Client { get; set; } = default!;
    }
}
