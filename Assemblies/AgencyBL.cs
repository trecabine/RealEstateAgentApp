using Assemblies.DataContext;
using RealEstateAgencyApp.Entities;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Assemblies
{
    public class AgencyBL : IAgencyBL
    {
        public AgencyBL()
        {
        }

        private Agency ReturnAgencyIfExistByPhoneNumber(string agencyPhoneNumber)
        {
            var agency = new Agency();
            using (var dbContext = new ApplicationDbContext())
            {
                if (!string.IsNullOrEmpty(agencyPhoneNumber))
                {
                    agency = dbContext.Agencies.FirstOrDefault(a => a.AgencyPhone == agencyPhoneNumber);
                }
            }
            return agency;
        }

        private Agency ReturnAgencyIfExistByName(string agencyName)
        {
            var agency = new Agency();
            using (var dbContext = new ApplicationDbContext())
            {
                agency = dbContext.Agencies.FirstOrDefault(a => a.AgencyName == agencyName);
            }
            return agency;
        }

        public Agency ProcessAgencyToDatabase(string agencyName, string agencyPhoneNumber)
        {
            var agency = ReturnAgencyIfExistByPhoneNumber(agencyPhoneNumber);

            if (agency == null)
            {
                agency = new Agency();
                agency.AgencyName = agencyName;
                agency.AgencyPhone = agencyPhoneNumber;

                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Agencies.Add(agency);
                    SaveChanges(dbContext);
                }
            }

            return agency;
        }

        private void SaveChanges(ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            }
        }
    }
}
