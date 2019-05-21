using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IGalleryImageRepository
    {
        IEnumerable<GalleryImage> Images { get; }
        void saveImage(GalleryImage image);
        GalleryImage deleteImage(int GalleryImageID);
    }
}