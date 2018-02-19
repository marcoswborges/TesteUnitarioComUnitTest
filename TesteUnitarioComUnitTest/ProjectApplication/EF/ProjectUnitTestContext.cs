using ProjectApplication.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.EF
{
    public class ProjectUnitTestContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
    }
}
