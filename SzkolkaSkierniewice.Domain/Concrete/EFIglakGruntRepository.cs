using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFIglakGruntRepository:IIglakGruntRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<IglakGrunt> IglakiGrunt
        {
            get { return context.IglakiGrunt; }
        }

        public void SaveIglakGrunt(IglakGrunt iglakGrunt)
        {
            if (iglakGrunt.ProductID == 0)
            {
                context.IglakiGrunt.Add(iglakGrunt);
            }
            else
            {
                IglakGrunt dbEntry = context.IglakiGrunt.Find(iglakGrunt.ProductID);
                if (dbEntry != null)
                {
                    //Product
                    dbEntry.Name = iglakGrunt.Name;
                    dbEntry.Date = iglakGrunt.Date;
                    dbEntry.Description = iglakGrunt.Description;
                    dbEntry.Price = iglakGrunt.Price;
                    dbEntry.Discount = iglakGrunt.Discount;
                    dbEntry.Description = iglakGrunt.Description;
                    dbEntry.Available = iglakGrunt.Available;
                    if (dbEntry.Available == true)
                    {
                        dbEntry.Quantity = iglakGrunt.Quantity;
                    }
                    else
                    {
                        dbEntry.Quantity = null;
                    }
                    dbEntry.HeightMin = iglakGrunt.HeightMin;
                    dbEntry.HeightMax = iglakGrunt.HeightMax;
                    dbEntry.ImageData = iglakGrunt.ImageData;
                    dbEntry.ImageMimeType = iglakGrunt.ImageMimeType;
                    //IglakiGrunt
                }
            }
            context.SaveChanges();
        }

        public IglakGrunt DeleteIglakGrunt(int iglakGruntID)
        {
            IglakGrunt dbEntry = context.IglakiGrunt.Find(iglakGruntID);
            if (dbEntry != null)
            {
                context.IglakiGrunt.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}