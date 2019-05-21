using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFGalleryImageRepositroy:IGalleryImageRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<GalleryImage> Images
        {
            get { return context.GalleryImages; }
        }

        public void saveImage(GalleryImage Image)
        {
            context.GalleryImages.Add(Image);
            context.SaveChanges();
        }

        public GalleryImage deleteImage(int ImageId)
        {
            GalleryImage dbEntry = context.GalleryImages.Find(ImageId);
            if(dbEntry != null)
            {
                context.GalleryImages.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}