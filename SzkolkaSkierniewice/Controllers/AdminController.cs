using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Concrete;
using SzkolkaSkierniewice.ViewModels;

namespace SzkolkaSkierniewice.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //private EFDbContext context = new EFDbContext();

        private IDrzewoAlejoweRepository drzewoAlejoweRepository;
        private IIglakGruntRepository iglakGruntRepository;
        private IIglakPojemnikRepository iglakPojemnikRepository;
        private IKrzewLisciastyRepository krzewLisciasityRepository;
        private IBoxRepository boxesRepository;
        private IPromotionRepository promotionRepository;
        private IProductRepository productRepository;
        private IGalleryImageRepository imageRepository;
        
        public AdminController(IDrzewoAlejoweRepository drzewoAlejowe, IIglakGruntRepository iglakGrunt, IIglakPojemnikRepository iglakPojemnik, IKrzewLisciastyRepository krzewLisciasty, IBoxRepository boxes, IPromotionRepository promotion, IProductRepository product, IGalleryImageRepository image)
        {
            drzewoAlejoweRepository = drzewoAlejowe;
            iglakGruntRepository = iglakGrunt;
            iglakPojemnikRepository = iglakPojemnik;
            krzewLisciasityRepository = krzewLisciasty;
            boxesRepository = boxes;
            promotionRepository = promotion;
            productRepository = product;
            imageRepository = image;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region GalleryImage

        public ActionResult GalleryImageList()
        {
            return View(imageRepository.Images);
        }

        public ActionResult CreateGalleryImage()
        {
            var GalleryImage = new GalleryImage();
            GalleryImage.Date = DateTime.Now;
            return View(GalleryImage);
        }

        [HttpPost]
        public ActionResult CreateGalleryImage(GalleryImage galleryImage, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    galleryImage.ImageMimeType = image.ContentType;
                    galleryImage.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(galleryImage.ImageData, 0, image.ContentLength);
                }
                imageRepository.saveImage(galleryImage);
                TempData["message"] = string.Format("Zapisano zdjęcie o ID: {0} ", galleryImage.GalleryImageID);
                return RedirectToAction("GalleryImageList");
            }
            else
            {
                return View(galleryImage);
            }
        }

        [HttpPost]
        public ActionResult DeleteGalleryImage(int galleryImageID)
        {
            GalleryImage deletedGalleryImage = imageRepository.deleteImage(galleryImageID);
            if (deletedGalleryImage != null)
            {
                TempData["message"] = string.Format("Usunieto zdjęcie o ID: {0}", deletedGalleryImage.GalleryImageID);
            }
            return RedirectToAction("GalleryImageList");
        }

        #endregion

        #region DrzewaAlejowe

        public ActionResult DrzewoAlejoweList()
        {
            return View(drzewoAlejoweRepository.DrzewaAlejowe);
        }

        public ActionResult EditDrzewoAlejowe(int productID)
        {
            DrzewoAlejowe drzewoalejowe = drzewoAlejoweRepository.DrzewaAlejowe.FirstOrDefault(d => d.ProductID == productID);
            //drzewoalejowe.Date = DateTime.Now;
            return View(drzewoalejowe);
        }

        [HttpPost]
        public ActionResult EditDrzewoAlejowe(DrzewoAlejowe drzewoAlejowe, HttpPostedFileBase image = null)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    drzewoAlejowe.ImageMimeType = image.ContentType;
                    drzewoAlejowe.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(drzewoAlejowe.ImageData, 0, image.ContentLength);
                }
                drzewoAlejoweRepository.SaveDrzewoAlejowe(drzewoAlejowe);
                TempData["message"] = string.Format("Zapisano {0} ", drzewoAlejowe.Name);
                return RedirectToAction("DrzewoAlejoweList");
            }
            else
            {
                return View(drzewoAlejowe);
            }
        }

        public ActionResult CreateDrzewoAlejowe()
        {
            var drzewoAlejowe = new DrzewoAlejowe();
            drzewoAlejowe.Date = DateTime.Now;
            drzewoAlejowe.Available = true;
            return View("EditDrzewoAlejowe", drzewoAlejowe);
        }

        [HttpPost]
        public ActionResult DeleteDrzewoAlejowe(int productID)
        {
            Product deletedDrzewoAlejowe = drzewoAlejoweRepository.DeleteDrzewoAlejowe(productID); 
            if(deletedDrzewoAlejowe != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deletedDrzewoAlejowe.Name);
            }
            return RedirectToAction("DrzewoAlejoweList");
        }

        #endregion

        #region IlgakiGrunt

        public ActionResult IglakGruntList()
        {
            return View(iglakGruntRepository.IglakiGrunt);
        }

        public ActionResult EditIglakGrunt(int productID)
        {
            IglakGrunt iglakGrunt = iglakGruntRepository.IglakiGrunt.FirstOrDefault(d => d.ProductID == productID);
            //iglakGrunt.Date = DateTime.Now;
            return View(iglakGrunt);
        }

        [HttpPost]
        public ActionResult EditIglakGrunt(IglakGrunt iglakGrunt, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    iglakGrunt.ImageMimeType = image.ContentType;
                    iglakGrunt.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(iglakGrunt.ImageData, 0, image.ContentLength);
                }
                iglakGruntRepository.SaveIglakGrunt(iglakGrunt);
                TempData["message"] = string.Format("Zapisano {0} ", iglakGrunt.Name);
                return RedirectToAction("IglakGruntList");
            }
            else
            {
                return View(iglakGrunt);
            }
        }

        public ActionResult CreateIglakGrunt()
        {
            var iglakGrunt = new IglakGrunt();
            iglakGrunt.Date = DateTime.Now;
            iglakGrunt.Available = true;
            return View("EditIglakGrunt", iglakGrunt);
        }

        [HttpPost]
        public ActionResult DeleteIglakGrunt(int productID)
        {
            IglakGrunt deletedIglakGrunt = iglakGruntRepository.DeleteIglakGrunt(productID);
            if (deletedIglakGrunt != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deletedIglakGrunt.Name);
            }
            return RedirectToAction("IglakGruntList");
        }

        #endregion

        #region IglakiPojemnik

        private void PopulateAssignedBoxesForIglakPojemnik(IglakPojemnik iglakPojemnik)
        {
            var allBoxes = boxesRepository.Boxes;
            var iglakPojemnikBoxes = new HashSet<int>(iglakPojemnik.Boxes.Select(i => i.BoxID));
            var viewModel = new List<AssignedBoxes>();
            foreach(var box in allBoxes)
            {
                viewModel.Add(new AssignedBoxes
                {
                    BoxID = box.BoxID,
                    Title = box.Name,
                    Assigned = iglakPojemnikBoxes.Contains(box.BoxID)
                });                    
            }
            ViewBag.Boxes = viewModel;
        }

        public ActionResult IglakPojemnikList()
        {
            return View(iglakPojemnikRepository.IglakiPojemnik.AsQueryable().Include(b => b.Boxes));
        }

        public ActionResult EditIglakPojemnik(int productID)
        {
            IglakPojemnik iglakPojemnik = iglakPojemnikRepository.IglakiPojemnik.AsQueryable().Include(b => b.Boxes).FirstOrDefault(d => d.ProductID == productID);
            //iglakPojemnik.Date = DateTime.Now;
            PopulateAssignedBoxesForIglakPojemnik(iglakPojemnik);
            return View(iglakPojemnik);
        }

        [HttpPost]
        public ActionResult EditIglakPojemnik(string[] selectedBoxes, IglakPojemnik iglakPojemnik, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    iglakPojemnik.ImageMimeType = image.ContentType;
                    iglakPojemnik.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(iglakPojemnik.ImageData, 0, image.ContentLength);
                }

                iglakPojemnikRepository.SaveIglakPojemnik(selectedBoxes, iglakPojemnik);
                TempData["message"] = string.Format("Zapisano {0} ", iglakPojemnik.Name);
                return RedirectToAction("IglakPojemnikList");
            }
            else
            {
                return View(iglakPojemnik);
            }
        }

        public ActionResult CreateIglakPojemnik()
        {
            var iglakPojemnik = new IglakPojemnik();
            iglakPojemnik.Date = DateTime.Now;
            iglakPojemnik.Available = true;
            iglakPojemnik.Boxes = new List<Box>();
            PopulateAssignedBoxesForIglakPojemnik(iglakPojemnik);
            return View("EditIglakPojemnik", iglakPojemnik);
        }

        [HttpPost]
        public ActionResult DeleteIglakPojemnik(int productID)
        {
            IglakPojemnik deletedIglakPojemnik = iglakPojemnikRepository.DeleteIglakPojemnik(productID);
            if (deletedIglakPojemnik != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deletedIglakPojemnik.Name);
            }
            return RedirectToAction("IglakPojemnikList");
        }

        #endregion

        #region KrzewyLisciaste

        private void PopulateAssignedBoxesForKrzewLisciasty(KrzewLisciasty krzewLisciasty)
        {
            var allBoxes = boxesRepository.Boxes;
            var KrzewLisciastyBoxes = new HashSet<int>(krzewLisciasty.Boxes.Select(i => i.BoxID));
            var viewModel = new List<AssignedBoxes>();
            foreach (var box in allBoxes)
            {
                viewModel.Add(new AssignedBoxes
                {
                    BoxID = box.BoxID,
                    Title = box.Name,
                    Assigned = KrzewLisciastyBoxes.Contains(box.BoxID)
                });
            }
            ViewBag.Boxes = viewModel;
        }

        public ActionResult KrzewLisciastyList()
        {
            return View(krzewLisciasityRepository.KrzewyLisciaste.AsQueryable().Include(b => b.Boxes));
        }

        public ActionResult EditKrzewLisciasty(int productID)
        {   
            KrzewLisciasty krzewLisciasty = krzewLisciasityRepository.KrzewyLisciaste.FirstOrDefault(d => d.ProductID == productID);
            //krzewLisciasty.Date = DateTime.Now;
            PopulateAssignedBoxesForKrzewLisciasty(krzewLisciasty);
            return View(krzewLisciasty);
        }

        [HttpPost]
        public ActionResult EditKrzewLisciasty(string[] selectedBoxes, KrzewLisciasty krzewLisciasty, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    krzewLisciasty.ImageMimeType = image.ContentType;
                    krzewLisciasty.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(krzewLisciasty.ImageData, 0, image.ContentLength);
                }

                krzewLisciasityRepository.SaveKrzewLisciasty(selectedBoxes, krzewLisciasty);
                TempData["message"] = string.Format("Zapisano {0} ", krzewLisciasty.Name);
                return RedirectToAction("KrzewLisciastyList");
            }
            else
            {
                return View(krzewLisciasty);
            }
        }

        public ActionResult CreateKrzewLisciasty()
        {
            var krzewLisciasty = new KrzewLisciasty();
            krzewLisciasty.Boxes = new List<Box>();
            krzewLisciasty.Date = DateTime.Now;
            krzewLisciasty.Available = true;
            PopulateAssignedBoxesForKrzewLisciasty(krzewLisciasty);
            return View("EditKrzewLisciasty", krzewLisciasty);
        }

        [HttpPost]
        public ActionResult DeleteKrzewLisciasty(int productID)
        {
            KrzewLisciasty deletedKrzewLisciasty = krzewLisciasityRepository.DeleteKrzewLisciasty(productID);
            if (deletedKrzewLisciasty != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deletedKrzewLisciasty.Name);
            }
            return RedirectToAction("KrzewLisciastyList");
        }

        #endregion

        #region Boxes

        public ActionResult BoxesList()
        {
            return View(boxesRepository.Boxes);
        }

        public ActionResult EditBox(int boxID)
        {
            Box box = boxesRepository.Boxes.FirstOrDefault(d => d.BoxID == boxID);
            return View(box);
        }

        [HttpPost]
        public ActionResult EditBox(Box box)
        {
            if (ModelState.IsValid)
            {
                boxesRepository.SaveBox(box);
                TempData["message"] = string.Format("Zapisano {0} ", box.Name);
                return RedirectToAction("BoxesList");
            }
            else
            {
                return View(box);
            }
        }

        public ActionResult CreateBox()
        {
            return View("EditBox", new Box());
        }

        [HttpPost]
        public ActionResult DeleteBox(int boxID)
        {
            Box deletedBox = boxesRepository.DeleteBox(boxID);
            if (deletedBox != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deletedBox.Name);
            }
            return RedirectToAction("BoxesList");
        }

        #endregion

        #region promotions

        public void AddtoProductInPromotionList(int quantity, int productID, int promotionID, List<ProductInPromotion> productInPromotion)
        { 
            ProductInPromotion p = new ProductInPromotion();
            p.ProductID = productID;
            p.PromotionID = promotionID;
            p.QuantityOfProductsInPromotion = quantity;
            productInPromotion.Add(p);
        }

        public void PopulateProductsDropDownList()
        {
            /*var productsQuery = from d in productRepository.Products
                           orderby d.Name
                           select d;*/
            ViewBag.ProductID = new SelectList(productRepository.Products, "ProductID", "Name");
        }

        public ActionResult PromotionList()
        {
            return View(promotionRepository.Promotions.AsQueryable().Include(p => p.ProductPromotionList.Select(q => q.Product)));
        }

        public ActionResult EditPromotion(int promotionID)
        {
            int quantity = 0;
            Promotion promotion = promotionRepository.Promotions.AsQueryable().Include(p => p.ProductPromotionList
                .Select(q => q.Product))
                .FirstOrDefault(d => d.PromotionID == promotionID);
            ViewBag.Quantity = quantity;
            PopulateProductsDropDownList();
            return View(promotion);
        }

        [HttpPost]
        public ActionResult EditPromotion(Promotion promotion, List<ProductInPromotion> productsInPromotion)
        {
            //var productsInPromotion = new List<ProductInPromotion>();
            if (ModelState.IsValid)
            {
                promotionRepository.SavePromotion(promotion, productsInPromotion);
                TempData["message"] = string.Format("Zapisano {0} ", promotion.Name);
                return RedirectToAction("PromotionList");
            }
            else
            {
                return View(promotion);
            }
        }

        public ActionResult CreatePromotion()
        {
            int quantity = 0;
            var promotion = new Promotion();
            promotion.ProductPromotionList = new List<ProductInPromotion>();
            promotion.Date = DateTime.Now;
            promotion.EndDate = DateTime.Now;
            ViewBag.Quantity = quantity;
            PopulateProductsDropDownList();
            //ViewBag.ProductID = new SelectList(productRepository.Products, "ProductID", "Name");
            return View("EditPromotion", promotion);
        }

        [HttpPost]
        public ActionResult DeletePromotion(int promotionID)
        {
            Promotion deletedPromotion = promotionRepository.DeletePromotion(promotionID);
            if (deletedPromotion != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deletedPromotion.Name);
            }
            return RedirectToAction("PromotionList");
        }

        #endregion
    }
}