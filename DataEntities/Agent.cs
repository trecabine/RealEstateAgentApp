using System.ComponentModel.DataAnnotations;

namespace RealEstateAgencyApp.Entities
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public Agency Agency { get; set; }
    }
}