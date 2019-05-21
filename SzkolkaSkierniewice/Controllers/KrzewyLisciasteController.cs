using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;
using PagedList;
using System.Net;
using Newtonsoft.Json;

namespace SzkolkaSkierniewice.Controllers
{
    public class KrzewyLisciasteController : Controller
    {
        private IKrzewLisciastyRepository repository;

        public KrzewyLisciasteController(IKrzewLisciastyRepository krzewLisciastyRepository)
        {
            this.repository = krzewLisciastyRepository;
        }

        public ActionResult Details(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KrzewLisciasty krzewLisciasty = repository.KrzewyLisciaste.FirstOrDefault(k => k.ProductID == productID);

            if(krzewLisciasty == null)
            {
                return HttpNotFound();
            }
            return View(krzewLisciasty);
        }
        
        public ActionResult List(string searchString, string sortOrder, string currentFilter, int? page = 1)
        {
            ViewBag.SelectedCategory = "KrzewyLisciaste";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortOrderAlphabetical = "SortAlphabetical";
            ViewBag.SortOrderAlphabeticalDescending = "SortAlphabeticalDescending";
            ViewBag.SortOrderPrice = "SortByPrice";
            ViewBag.SortOrderPriceDescending = "SortByPrcieDescending";
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            if (searchString != null)
            {
                searchString = searchString.ToLower();
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var krzewyLisciaste = repository.KrzewyLisciaste;

            krzewyLisciaste = krzewyLisciaste.OrderBy(m => m.Name);

            if(!string.IsNullOrEmpty(searchString))
            {
                krzewyLisciaste = krzewyLisciaste.Where(x => x.Name.ToLower().Contains(searchString)); 
            }

            switch(sortOrder)
            {
                case "SortAlphabeticalDescending":
                    krzewyLisciaste = krzewyLisciaste.OrderByDescending(m => m.Name);
                    break;
                case "SortByPrice":
                    krzewyLisciaste = krzewyLisciaste.OrderBy(m => m.PriceAfterDicount);
                    break;
                case "SortByPrcieDescending":
                    krzewyLisciaste = krzewyLisciaste.OrderByDescending(m => m.PriceAfterDicount);
                    break;
                case "SortAlphabetical":
                    krzewyLisciaste = krzewyLisciaste.OrderBy(m => m.Name);
                    break;
                default:
                    break;
            }

            return View(krzewyLisciaste.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Results(string prefix)
        {
            var json = repository.KrzewyLisciaste;
            var prefixToLover = prefix.ToLower();
            /*string jsonString = JsonConvert.SerializeObject(json, Formatting.None, 
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });*/

            var name = (from n in json
                        where n.Name.ToLower().Contains(prefixToLover)
                        select new { n.Name });
            return Json(name, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GetImage(int productID)
        {
            KrzewLisciasty krzewLisciasty = repository.KrzewyLisciaste.FirstOrDefault(p => p.ProductID == productID);
            if (krzewLisciasty != null)
            {
                return File(krzewLisciasty.ImageData, krzewLisciasty.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}