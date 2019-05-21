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
    public class IglakiGruntController : Controller
    {
        private IIglakGruntRepository repository;

        public IglakiGruntController(IIglakGruntRepository iglakGruntRepository)
        {
            this.repository = iglakGruntRepository;
        }

        public ActionResult Details(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IglakGrunt iglakGrunt = repository.IglakiGrunt.FirstOrDefault(k => k.ProductID == productID);

            if (iglakGrunt == null)
            {
                return HttpNotFound();
            }
            return View(iglakGrunt);
        }

        // GET: Product
        public ActionResult List(string sortOrder, string currentFilter, string searchString, int? page = 1)
        {
            ViewBag.SelectedCategory = "IglakiGrunt";
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

            var iglakiGrunt = repository.IglakiGrunt;

            iglakiGrunt = iglakiGrunt.OrderBy(m => m.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                iglakiGrunt = iglakiGrunt.Where(x => x.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "SortAlphabeticalDescending":
                    iglakiGrunt = iglakiGrunt.OrderByDescending(m => m.Name);
                    break;
                case "SortByPrice":
                    iglakiGrunt = iglakiGrunt.OrderBy(m => m.PriceAfterDicount);
                    break;
                case "SortByPrcieDescending":
                    iglakiGrunt = iglakiGrunt.OrderByDescending(m => m.PriceAfterDicount);
                    break;
                case "SortAlphabetical":
                    iglakiGrunt = iglakiGrunt.OrderBy(m => m.Name);
                    break;
                default:
                    break;
            }
            return View(iglakiGrunt.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Results(string prefix)
        {
            var json = repository.IglakiGrunt;
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
            IglakGrunt iglakGrunt = repository.IglakiGrunt.FirstOrDefault(p => p.ProductID == productID);
            if (iglakGrunt != null)
            {
                return File(iglakGrunt.ImageData, iglakGrunt.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}