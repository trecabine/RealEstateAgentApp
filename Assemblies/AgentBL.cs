using Assemblies.DataContext;
using CsvHelper;
using RealEstateAgencyApp.Entities;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;

namespace Assemblies
{
    public class AgentBL : IAgentBL
    {
        private ApplicationDbContext _applicationDbContext;
        private IAgencyBL _agencyBL;

        public AgentBL(IAgencyBL agencyBL)
        {
            _applicationDbContext = new ApplicationDbContext();
            _agencyBL = agencyBL;
        }

        public void ProcessAgentCSVFromCSVFile(StreamReader readCsvFile)
        {
            var listOfAgentCSVs = ReturnAgentCSVFromCSVFile(readCsvFile);

            if (listOfAgentCSVs.Count > 0)
            {
                foreach (var agentCSV in listOfAgentCSVs)
                {
                    if (!string.IsNullOrEmpty(agentCSV.AgentName))
                    {
                        var validatedAgentEmailAddress =
                            Utilities.ValidateAndReturnCleansedEmail(agentCSV.AgentEmail);
                        var validatedAgentPhoneNumber =
                            Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentCSV.AgentPhone);
                        var validatedAgentMobileNumber =
                            Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentCSV.AgentMobile);

                        var trimmedAgentName = agentCSV.AgentName.Trim();

                        var agent = ReturnAgentIfExistByNameAndMobile(trimmedAgentName, validatedAgentMobileNumber);
                        if (agent == null)
                        {
                            agent = new Agent();
                            _applicationDbContext.Agents.Add(agent);

                            agent.Name = trimmedAgentName;
                        }
                        agent = ProcessAgentEmail(agent, validatedAgentEmailAddress);
                        agent = ProcessAgentPhoneAndMobile(agent, validatedAgentMobileNumber, validatedAgentPhoneNumber);

                        if (!string.IsNullOrEmpty(agentCSV.AgencyName))
                        {
                            var trimmedAgencyName = agentCSV.AgencyName.Trim();
                            var validatedAgencyPhoneNumber =
                                Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentCSV.AgencyPhone);

                            var agency = _agencyBL.ProcessAgencyToDatabase(trimmedAgencyName, validatedAgencyPhoneNumber);
                            
                            agent.Agency = agency;
                        }
                    }
                }

                try
                {
                    _applicationDbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                }
            }
        }

        private Agent ReturnAgentIfExistByNameAndMobile(string agentName, string agentMobile)
        {
            var listOfAgents = _applicationDbContext.Agents.Where(x => x.Name == agentName).ToList();
            if (listOfAgents.Count > 0)
            {
                foreach (var agent in listOfAgents)
                {
                    if (!string.IsNullOrEmpty(agentMobile) && !string.IsNullOrEmpty(agent.Mobile))
                    {
                        if (agentMobile == agent.Mobile)
                        {
                            return agent;
                        }
                    }
                    return agent;
                }
            }
            return null;
        }

        private Agent ProcessAgentEmail(Agent agent, string agentEmail)
        {
            if (!string.IsNullOrEmpty(agentEmail))
            {
                agent.Email = agentEmail;
            }
            return agent;
        }

        private Agent ProcessAgentPhoneAndMobile(Agent agent, string agentMobile, string agentPhoneNumber)
        {
            if (!string.IsNullOrEmpty(agentPhoneNumber))
            {
                agent.Phone = agentPhoneNumber;
            }

            if (!string.IsNullOrEmpty(agentMobile))
            {
                agent.Mobile = agentMobile;
            }
            return agent;
        }

        private List<AgentCSV> ReturnAgentCSVFromCSVFile(StreamReader readCsvFile)
        {
            using (CsvReader csvReader = new CsvReader(readCsvFile))
            {
                var agentNameField = "";
                var agentEmailField = "";
                var agentPhoneField = "";
                var agentMobileField = "";

                var agencyNameField = "";
                var agencyPhoneField = "";

                var agentCSVs = csvReader.GetRecords<AgentCSV>().ToList();

                return agentCSVs;
            }
        }
    }
}