using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldBank.Controllers
{
    public class IndicatorsController : Controller
    {
        //
        // GET: /Indicator/
        WorldBankDataContext _dc = new WorldBankDataContext();
        public ActionResult Index()
        {
            MakeRequests mr = new MakeRequests();
            return View(mr.getAllIndicators());
        }

        public JsonResult Update(string id)
        {
            MakeRequests mr = new MakeRequests();
            return Json(mr.updateIndicator(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return View(_dc.DIBS_Fields.Where(a => a.SourceID == 38));
        }

    }
}
