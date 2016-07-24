using System.IO;

namespace Assemblies
{
    public interface IAgentBL
    {
        void ProcessAgentCSVFromCSVFile(StreamReader readCsvFile);
    }
}
