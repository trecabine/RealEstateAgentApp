using RealEstateAgencyApp.Entities;
using System.Collections.Generic;
using System.IO;

namespace Assemblies
{
    public interface IAgentBL
    {        
        List<AgentCSV> ReturnAgentCSVFromCSVFile(StreamReader readCsvFile);

        void ProcessAgentCSV(List<AgentCSV> listOfAgentCSVs);
    }
}
