using Assemblies.DataContext;
using RealEstateAgencyApp.Entities;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Assemblies
{
    public class AgencyBL : IAgencyBL
    {
        private ApplicationDbContext _applicationDbContext;

        public AgencyBL(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private Agency ReturnAgencyIfExistByPhoneNumber(string agencyPhoneNumber)
        {
            if (!string.IsNullOrEmpty(agencyPhoneNumber))
            {
                return _applicationDbContext.Agencies.FirstOrDefault(a => a.AgencyPhone == agencyPhoneNumber);
            }
            return null;
        }

        private Agency ReturnAgencyIfExistByName(string agencyName)
        {
            return _applicationDbContext.Agencies.FirstOrDefault(a => a.AgencyName == agencyName);
        }

        public Agency ProcessAgencyToDatabase(string agencyName, string agencyPhoneNumber)
        {
            var agency = ReturnAgencyIfExistByPhoneNumber(agencyPhoneNumber);

            if (agency == null)
            {
                agency = ReturnAgencyIfExistByName(agencyName);

                if (agency == null)
                {
                    agency = new Agency();
                    agency.AgencyName = agencyName;
                    _applicationDbContext.Agencies.Add(agency);
                }

                agency.AgencyPhone = agencyPhoneNumber;
            }
            else
            {
                if (!string.IsNullOrEmpty(agencyPhoneNumber))
                {
                    if (agency.AgencyPhone != agencyPhoneNumber)
                    {
                        agency.AgencyPhone = agencyPhoneNumber;
                    }
                }
            }

            //try
            //{
            //    _applicationDbContext.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    var entry = ex.Entries.Single();
            //    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            //}

            return agency;
        }
    }
}
