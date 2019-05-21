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
    public class IglakiPojemnikController : Controller
    {
        private IIglakPojemnikRepository repository;

        public IglakiPojemnikController(IIglakPojemnikRepository iglakPojemnikRepository)
        {
            this.repository = iglakPojemnikRepository;
        }

        public ActionResult Details(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IglakPojemnik iglakPojemnik = repository.IglakiPojemnik.FirstOrDefault(k => k.ProductID == productID);

            if (iglakPojemnik == null)
            {
                return HttpNotFound();
            }
            return View(iglakPojemnik);
        }

        // GET: Product
        public ActionResult List(string sortOrder, string currentFilter, string searchString, int? page = 1)
        {
            ViewBag.SelectedCategory = "IglakiPojemnik";
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

            var iglakiPojemnik = repository.IglakiPojemnik;

            iglakiPojemnik = iglakiPojemnik.OrderBy(m => m.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                iglakiPojemnik = iglakiPojemnik.Where(x => x.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "SortAlphabeticalDescending":
                    iglakiPojemnik = iglakiPojemnik.OrderByDescending(m => m.Name);
                    break;
                case "SortByPrice":
                    iglakiPojemnik = iglakiPojemnik.OrderBy(m => m.PriceAfterDicount);
                    break;
                case "SortByPrcieDescending":
                    iglakiPojemnik = iglakiPojemnik.OrderByDescending(m => m.PriceAfterDicount);
                    break;
                case "SortAlphabetical":
                    iglakiPojemnik = iglakiPojemnik.OrderBy(m => m.Name);
                    break;
                default:
                    break;
            }
            return View(iglakiPojemnik.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Results(string prefix)
        {
            var json = repository.IglakiPojemnik;
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
            IglakPojemnik iglakPojemnik = repository.IglakiPojemnik.FirstOrDefault(p => p.ProductID == productID);
            if (iglakPojemnik != null)
            {
                return File(iglakPojemnik.ImageData, iglakPojemnik.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}