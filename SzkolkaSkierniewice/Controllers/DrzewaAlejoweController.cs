using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;
using PagedList;
using System.Net;

namespace SzkolkaSkierniewice.Controllers
{
    public class DrzewaAlejoweController : Controller
    {
        private IDrzewoAlejoweRepository repository;

        public DrzewaAlejoweController(IDrzewoAlejoweRepository DrzewoAlejoweRepository)
        {
            this.repository = DrzewoAlejoweRepository;
        }

        public ActionResult Details(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DrzewoAlejowe drzewoAlejowe = repository.DrzewaAlejowe.FirstOrDefault(k => k.ProductID == productID);

            if (drzewoAlejowe == null)
            {
                return HttpNotFound();
            }
            return View(drzewoAlejowe);
        }

        // GET: Product
        public ActionResult List(string sortOrder, string currentFilter, string searchString, int? page = 1)
        {
            ViewBag.SelectedCategory = "DrzewaAlejowe";
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

            var drzewaAlejowe = repository.DrzewaAlejowe;

            drzewaAlejowe = drzewaAlejowe.OrderBy(m => m.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                drzewaAlejowe = drzewaAlejowe.Where(x => x.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "SortAlphabeticalDescending":
                    drzewaAlejowe = drzewaAlejowe.OrderByDescending(m => m.Name);
                    break;
                case "SortByPrice":
                    drzewaAlejowe = drzewaAlejowe.OrderBy(m => m.PriceAfterDicount);
                    break;
                case "SortByPrcieDescending":
                    drzewaAlejowe = drzewaAlejowe.OrderByDescending(m => m.PriceAfterDicount);
                    break;
                case "SortAlphabetical":
                    drzewaAlejowe = drzewaAlejowe.OrderBy(m => m.Name);
                    break;
                default:
                    break;
            }
            return View(drzewaAlejowe.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Results(string prefix)
        {
            var json = repository.DrzewaAlejowe;
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
            DrzewoAlejowe drzewoAlejowe = repository.DrzewaAlejowe.FirstOrDefault(p => p.ProductID == productID);
            if(drzewoAlejowe != null)
            {
                return File(drzewoAlejowe.ImageData, drzewoAlejowe.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}