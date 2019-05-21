using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFDrzewoAlejoweRepository:IDrzewoAlejoweRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<DrzewoAlejowe> DrzewaAlejowe
        {
            get { return context.DrzewaAlejowe; }
        }

        public void SaveDrzewoAlejowe(DrzewoAlejowe drzewoAlejowe)
        {
            if(drzewoAlejowe.ProductID == 0)
            {
                context.DrzewaAlejowe.Add(drzewoAlejowe);
            }
            else
            {
                DrzewoAlejowe dbEntry = context.DrzewaAlejowe.Find(drzewoAlejowe.ProductID);
                if(dbEntry != null)
                {
                    //Product
                    dbEntry.Name = drzewoAlejowe.Name;
                    dbEntry.Date = drzewoAlejowe.Date;
                    dbEntry.Description = drzewoAlejowe.Description;
                    dbEntry.Price = drzewoAlejowe.Price;
                    dbEntry.Discount = drzewoAlejowe.Discount;
                    dbEntry.Description = drzewoAlejowe.Description;
                    dbEntry.Available = drzewoAlejowe.Available;
                    if (dbEntry.Available == true)
                    {
                        dbEntry.Quantity = drzewoAlejowe.Quantity;
                    }
                    else
                    {
                        dbEntry.Quantity = null;
                    }
                    dbEntry.HeightMin = drzewoAlejowe.HeightMin;
                    dbEntry.HeightMax = drzewoAlejowe.HeightMax;
                    dbEntry.ImageData = drzewoAlejowe.ImageData;
                    dbEntry.ImageMimeType = drzewoAlejowe.ImageMimeType;
                    //DrzewoAlejowe
                    dbEntry.WidthMin = drzewoAlejowe.WidthMin;
                    dbEntry.WidthMax = drzewoAlejowe.WidthMax;
                }
            }
            context.SaveChanges();
        }

        public DrzewoAlejowe DeleteDrzewoAlejowe(int drzewoAlejoweID)
        {
            DrzewoAlejowe dbEntry = context.DrzewaAlejowe.Find(drzewoAlejoweID);
            if(dbEntry != null)
            {
                context.DrzewaAlejowe.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}