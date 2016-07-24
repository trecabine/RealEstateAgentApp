using Assemblies.Identities;
using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateAgencyApp.Entities;
using System.Data.Entity;

namespace Assemblies.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Agency> Agencies { get; set; }
    }
}