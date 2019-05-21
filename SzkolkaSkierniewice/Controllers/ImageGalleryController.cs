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
    public class ImageGalleryController : Controller
    {
        private IGalleryImageRepository repository;

        public ImageGalleryController(IGalleryImageRepository galleryRepositroy)
        {
            this.repository = galleryRepositroy;
        }
        // GET: ImgageGallery
        public ActionResult List(int? page = 1)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            var images = repository.Images;

            images = images.OrderBy(d => d.Date);

            return View(images.ToList().ToPagedList(pageNumber, pageSize));
        }

        public FileContentResult GetPhoto(int galleryImageID)
        {
            GalleryImage galleryImage = repository.Images.FirstOrDefault(p => p.GalleryImageID == galleryImageID);
            if (galleryImage != null)
            {
                return File(galleryImage.ImageData, galleryImage.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}