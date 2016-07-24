using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgencyApp.Entities
{
    public class Agency
    {
        [Key]
        public int Id { get; set; }
        public string AgencyName { get; set; }
        public string AgencyPhone { get; set; }
        
        public List<Agent> Agents { get; set; }
    }
}