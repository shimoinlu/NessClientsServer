using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NessClientsServer.Models;

namespace NessClientsServer.Data
{
    public class NessClientsServerContext : DbContext
    {
        public NessClientsServerContext (DbContextOptions<NessClientsServerContext> options)
            : base(options)
        {
        }

        public DbSet<NessClientsServer.Models.Client> Client { get; set; } = default!;
    }
}
