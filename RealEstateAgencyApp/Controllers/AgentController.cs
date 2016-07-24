using Assemblies;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace RealEstateAgencyApp.Controllers
{
    public class AgentController : Controller
    {
        private IAgentBL _agentBL;

        public AgentController()
        {
            
        }

        public AgentController(IAgentBL agentBL)
        {
            _agentBL = agentBL;
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (file.FileName.EndsWith(".csv"))
                    {
                        var stream = file.InputStream;
                        var readCsvFile = new StreamReader(stream);

                        _agentBL.ProcessAgentCSVFromCSVFile(readCsvFile);

                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }

            return View();
        }       
    }
}