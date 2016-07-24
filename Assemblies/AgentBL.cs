using Assemblies.DataContext;
using CsvHelper;
using RealEstateAgencyApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assemblies
{
    public class AgentBL : IAgentBL
    {
        private ApplicationDbContext _applicationDbContext;
        private IAgencyBL _agencyBL;

        public AgentBL(ApplicationDbContext applicationDbContext, IAgencyBL agencyBL)
        {
            _applicationDbContext = applicationDbContext;
            _agencyBL = agencyBL;
        }

        public void ProcessAgentCSV(List<AgentCSV> listOfAgentCSVs)
        {
            foreach (var agentCSV in listOfAgentCSVs)
            {
                if (!string.IsNullOrEmpty(agentCSV.AgentName))
                {
                    var agent = ReturnAgentIfExistByNameAndMobile(agentCSV.AgentName, agentCSV.AgentMobile);
                    if (agent == null)
                    {
                        agent = new Agent();
                    }
                    agent = ProcessAgentEmail(agent, agentCSV.AgentEmail);
                    agent = ProcessAgentPhoneAndMobile(agent, agentCSV.AgentPhone, agentCSV.AgentPhone);
                }

                if (!string.IsNullOrEmpty(agentCSV.AgentName))
                {
                }
            }
        }

        //private string ValidateAndReturnCleansedAgentPhoneNumber(string agentMobileNumber)
        //{
        //    if (Utilities.ValidateIfAusPhoneNumberIsValid(agentMobileNumber))
        //    {
        //        return Utilities.CleansePhoneNumber(agentMobileNumber);
        //    }

        //    return string.Empty;
        //}

        private string ValidateAndReturnCleansedAgentEmail(string agentEmail)
        {
            if (Utilities.ValidateIfEmailIsValid(agentEmail))
            {
                return agentEmail.Trim();
            }

            return string.Empty;
        }

        private Agent ReturnAgentIfExistByNameAndMobile(string agentName, string agentMobile)
        {
            var agent = _applicationDbContext.Agents.First(x => x.Name == agentName);
            if (agent != null)
            {
                var validatedAgentMobile = Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentMobile);

                if (validatedAgentMobile != string.Empty)
                {
                    if (validatedAgentMobile == agent.Mobile)
                    {
                        return agent;
                    }
                }
            }

            return null;
        }

        private Agent ProcessAgentEmail(Agent agent, string agentEmail)
        {
            agentEmail = ValidateAndReturnCleansedAgentEmail(agentEmail);
            if (!string.IsNullOrEmpty(agentEmail))
            {
                agent.Email = agentEmail;
            }
            return agent;
        }

        private Agent ProcessAgentPhoneAndMobile(Agent agent, string agentMobile, string agentPhoneNumber)
        {
            agentPhoneNumber = Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentPhoneNumber);
            if (!string.IsNullOrEmpty(agentPhoneNumber))
            {
                agent.Phone = agentPhoneNumber;
            }

            agentMobile = Utilities.ValidateAndReturnCleansedAusPhoneNumber(agentMobile);
            if (!string.IsNullOrEmpty(agentMobile))
            {
                agent.Mobile = agentMobile;
            }
            return agent;
        }

        private void UpdateAgentEmailAddress(Agent agent, string agentEmailAddress)
        {
            agent.Email = agentEmailAddress;
            _applicationDbContext.SaveChanges();
        }

        private void CreateNewAgent(string agentName, string agentEmailAddress, string agentMobileNo)
        {
            var agent = new Agent();
            agent.Name = agentName;
            agent.Email = agentEmailAddress;
            agent.Mobile = agentMobileNo;

            _applicationDbContext.SaveChanges();
        }

        public List<AgentCSV> ReturnAgentCSVFromCSVFile(StreamReader readCsvFile)
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

//  while (csvReader.Read())
//                {
//                    var agentCSV = new AgentCSV();

//var agent
//                    if (csvReader.TryGetField<string>("AgentName", out agentNameField))
//                    {                        
//                        if (!string.IsNullOrEmpty(agentNameField))
//                        {                            
//                            if (!csvReader.TryGetField<string>("AgentEmail", out agentEmailField))
//                            {
//                                //LogError
//                            }                           

//                            if (!csvReader.TryGetField<string>("AgentPhone", out agentPhoneField))
//                            {
//                                //LogError
//                            }

//                            if (!csvReader.TryGetField<string>("AgentMobile", out agentMobileField))
//                            {
//                                //LogError
//                            }

//                            agentCSV.AgentName = agentNameField;
//                            agentCSV.AgentEmail = agentEmailField;
//                            agentCSV.AgentPhone = agentPhoneField;
//                            agentCSV.AgentMobile = agentMobileField;

//                        }
//                    }

//                    if (csvReader.TryGetField<string>("AgencyName", out agencyNameField))
//                    {
//                        if (!string.IsNullOrEmpty(agencyNameField))
//                        {
//                            agentCSV.AgencyName = agencyNameField;
//                        }
//                    }




//                    v
//                }
//            }
