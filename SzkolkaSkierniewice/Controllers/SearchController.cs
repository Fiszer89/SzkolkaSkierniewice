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
    public class SearchController : Controller
    {
        private IProductRepository repository;

        public SearchController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ActionResult Promotions()
        {
            var products = repository.Products;

            products = products.Where(n => n.Discount > 0).OrderBy(m => m.Date).Take(3);

            return PartialView("PromotionSummary",products.ToList());
        }

        public ActionResult PromotionList(string searchString, string sortOrder, string currentFilter, int? page = 1)
        {
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

            var products = repository.Products.Where(d => d.Discount > 0);

            products = products.OrderBy(m => m.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "SortAlphabeticalDescending":
                    products = products.OrderByDescending(m => m.Name);
                    break;
                case "SortByPrice":
                    products = products.OrderBy(m => m.PriceAfterDicount);
                    break;
                case "SortByPrcieDescending":
                    products = products.OrderByDescending(m => m.PriceAfterDicount);
                    break;
                case "SortAlphabetical":
                    products = products.OrderBy(m => m.Name);
                    break;
                default:
                    break;
            }

            return View(products.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Search
        public ActionResult Index(string search, string currentFilter, int? page = 1)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            if (search != null)
            {
                search = search.ToLower();
                page = 1;
            }
            else
            {
                search = currentFilter;
            }


            var products = repository.Products;

            products = products.OrderBy(m => m.Name);

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(x => x.Name.ToLower().Contains(search));
            }

            return View(products.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Results(string prefix)
        {
            var json = repository.Products;
            var prefixToLover = prefix.ToLower();
            /*string jsonString = JsonConvert.SerializeObject(json, Formatting.None, 
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });*/

            var name = (from n in json
                        where n.Name.ToLower().Contains(prefixToLover)
                        select new { n.Name });
            return Json(name, JsonRequestBehavior.AllowGet);
        }
    }
}