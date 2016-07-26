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
        private IAgencyBL _agencyBL;

        public AgentBL(IAgencyBL agencyBL)
        {
            _agencyBL = agencyBL;
        }

        public void ProcessAgentCSVFromCSVFile(StreamReader readCsvFile)
        {
            var listOfAgentCSVs = ReturnAgentCSVFromCSVFile(readCsvFile);

            using (var dbContext = new ApplicationDbContext())
            {
                if (listOfAgentCSVs.Count > 0)
                {
                    foreach (var agentCSV in listOfAgentCSVs)
                    {
                        if (string.IsNullOrEmpty(agentCSV.AgentName))
                        {
                            return;
                        }

                        var validatedAgentEmailAddress =
                                Utilities.ValidateAndReturnCleansedEmail(agentCSV.AgentEmail);
                        var validatedAgentPhoneNumber =
                            Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentCSV.AgentPhone);
                        var validatedAgentMobileNumber =
                            Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentCSV.AgentMobile);

                        var trimmedAgentName = agentCSV.AgentName.Trim();

                        var agent = ReturnAgentIfExistByNameAndMobile(trimmedAgentName, validatedAgentMobileNumber);
                        
                        agent = ProcessAgentEmail(agent, validatedAgentEmailAddress);
                        agent = ProcessAgentPhoneAndMobile(agent, validatedAgentMobileNumber,
                            validatedAgentPhoneNumber);

                        if (!string.IsNullOrEmpty(agentCSV.AgencyName))
                        {
                            var validatedAgencyPhoneNumber =
                                Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentCSV.AgencyPhone);

                            var agency = _agencyBL.ProcessAgencyToDatabase(agentCSV.AgencyName.Trim(),
                                validatedAgencyPhoneNumber);

                            agent.Agency = agency;
                        }

                        if (agent.Id == 0)
                        {
                            agent.Name = trimmedAgentName;
                            dbContext.Agents.Add(agent);
                        }
                    }
                }

                SaveChanges(dbContext);
            }
        }

        private Agent ReturnAgentIfExistByNameAndMobile(string agentName, string agentMobile)
        {
            var agentEntity = new Agent();
            using (var dbContext = new ApplicationDbContext())
            {
                var listOfAgents = dbContext.Agents.Where(x => x.Name == agentName).ToList();
                if (listOfAgents.Count > 0)
                {
                    var agentWithMobile = listOfAgents.FirstOrDefault(x => x.Mobile == agentMobile);
                    var agentWithoutMobile = listOfAgents.FirstOrDefault(x => x.Name == agentName);

                    if (agentWithMobile != null)
                    {
                        agentEntity = agentWithMobile;
                    }
                    else
                    {
                        agentEntity = agentWithoutMobile;
                    }
                }
            }
            return agentEntity;
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