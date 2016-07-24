using Assemblies.DataContext;
using RealEstateAgencyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            agencyPhoneNumber = Utilities.ValidateAndReturnCleansedAusPhoneNumber(agencyPhoneNumber);

            if (string.IsNullOrEmpty(agencyPhoneNumber))
            {
                return _applicationDbContext.Agencies.First(a => a.AgencyPhone == agencyPhoneNumber);
            }
            return null;
        }

        public Agency ProcessAgencyToDatabase(string agencyName, string agencyPhoneNumber)
        {
            var agency = ReturnAgencyIfExistByPhoneNumber(agencyPhoneNumber);
            agencyPhoneNumber = Utilities.ValidateAndReturnCleansedAusPhoneNumber(agencyPhoneNumber);

            if (agency == null)
            {
                agency = new Agency();
            }
            
            if (string.IsNullOrEmpty(agencyPhoneNumber))
            {
                agency.AgencyName = agencyName;
                agency.AgencyPhone = agencyPhoneNumber;
            }

            return agency;
        }
    }
}
