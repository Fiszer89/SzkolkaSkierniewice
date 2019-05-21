using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFIglakPojemnikRepository:IIglakPojemnikRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<IglakPojemnik> IglakiPojemnik
        {
            get { return context.IglakiPojemnik; }
        }

        public void SaveIglakPojemnik(string[] selectedbox, IglakPojemnik iglakPojemnik)
        {
            if (iglakPojemnik.ProductID == 0)
            {
                if (selectedbox != null)
                {
                    iglakPojemnik.Boxes = new List<Box>();
                    foreach (var box in selectedbox)
                    {
                        var boxnumber = int.Parse(box);
                        var boxToAdd = context.Boxes.Where(b => b.BoxID == boxnumber).FirstOrDefault();
                        iglakPojemnik.Boxes.Add(boxToAdd);
                    }
                }
                context.IglakiPojemnik.Add(iglakPojemnik);
            }
            else
            {
                IglakPojemnik dbEntry = context.IglakiPojemnik.Include(b => b.Boxes).Where(k => k.ProductID == iglakPojemnik.ProductID).FirstOrDefault();
                if (dbEntry != null)
                {
                    UpdateAssignedBoxesForIglakPojemnik(selectedbox, dbEntry);
                    //Product
                    dbEntry.Name = iglakPojemnik.Name;
                    dbEntry.Date = iglakPojemnik.Date;
                    dbEntry.Description = iglakPojemnik.Description;
                    dbEntry.Price = iglakPojemnik.Price;
                    dbEntry.Discount = iglakPojemnik.Discount;
                    dbEntry.Description = iglakPojemnik.Description;
                    dbEntry.Available = iglakPojemnik.Available;
                    if (dbEntry.Available == true)
                    {
                        dbEntry.Quantity = iglakPojemnik.Quantity;
                    }
                    else
                    {
                        dbEntry.Quantity = null;
                    }
                    dbEntry.HeightMin = iglakPojemnik.HeightMin;
                    dbEntry.HeightMax = iglakPojemnik.HeightMax;
                    dbEntry.ImageData = iglakPojemnik.ImageData;
                    dbEntry.ImageMimeType = iglakPojemnik.ImageMimeType;
                    //IglakiGrunt
                }
            }
            context.SaveChanges();
        }

        public IglakPojemnik DeleteIglakPojemnik(int iglakPojemnikID)
        {
            IglakPojemnik dbEntry = context.IglakiPojemnik.Find(iglakPojemnikID);
            if (dbEntry != null)
            {
                context.IglakiPojemnik.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        private void UpdateAssignedBoxesForIglakPojemnik(string[] selectedBoxes, IglakPojemnik iglakToUpdate)
        {
            if (selectedBoxes == null)
            {
                iglakToUpdate.Boxes = new List<Box>();
                return;
            }

            var selectedBoxesHS = new HashSet<string>(selectedBoxes);
            var krzewIglastyBoxes = new HashSet<int>(iglakToUpdate.Boxes.Select(b => b.BoxID));
            foreach (var box in context.Boxes)
            {
                if (selectedBoxesHS.Contains(box.BoxID.ToString()))
                {
                    if (!krzewIglastyBoxes.Contains(box.BoxID))
                    {
                        iglakToUpdate.Boxes.Add(box);
                    }
                }
                else
                {
                    if (krzewIglastyBoxes.Contains(box.BoxID))
                    {
                        iglakToUpdate.Boxes.Remove(box);
                    }
                }
            }
        }
    }
}