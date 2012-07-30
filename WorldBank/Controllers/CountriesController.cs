using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WorldBank.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace WorldBank.Controllers
{
    public class CountriesController : Controller
    {
        //
        // GET: /Countries/

        public ActionResult Index()
        {
            MakeRequests mr = new MakeRequests();
            return View(mr.getAllCountries());
        }

    }
}
