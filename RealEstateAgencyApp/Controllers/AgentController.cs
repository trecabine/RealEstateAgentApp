using Assemblies;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateAgencyApp.Controllers
{
    public class AgentController : Controller
    {
        private IAgentBL _agentBL;

        public AgentController(IAgentBL agentBL)
        {
            _agentBL = agentBL;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAgent(HttpPostedFile postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    if (postedFile.FileName.EndsWith(".csv"))
                    {
                        var stream = postedFile.InputStream;
                        var readCsvFile = new StreamReader(stream);

                        var listOfAgentsFromCsvFile = _agentBL.ReturnAgentCSVFromCSVFile(readCsvFile);



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

        private void CheckCSVFile()
        {

        }
    }
}