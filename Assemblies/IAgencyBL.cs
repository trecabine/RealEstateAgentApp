using RealEstateAgencyApp.Entities;

namespace Assemblies
{
    public interface IAgencyBL
    {
        Agency ProcessAgencyToDatabase(string agencyName, string agencyPhoneNumber);
    }
}