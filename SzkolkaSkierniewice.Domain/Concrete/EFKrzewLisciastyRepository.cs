using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFKrzewLisciastyRepository:IKrzewLisciastyRepository
    {
        private EFDbContext context = new EFDbContext();
        
        public IEnumerable<KrzewLisciasty> KrzewyLisciaste
        {
            get { return context.KrzewyLisciaste; }
        }

        public void SaveKrzewLisciasty(string[] selectedbox, KrzewLisciasty krzewLisciasty)
        {
            if (krzewLisciasty.ProductID == 0)
            {
                if(selectedbox != null)
                {
                    krzewLisciasty.Boxes = new List<Box>();
                    foreach(var box in selectedbox)
                    {
                        var boxnumber = int.Parse(box);
                        var boxToAdd = context.Boxes.Where(b => b.BoxID == boxnumber).FirstOrDefault();
                        krzewLisciasty.Boxes.Add(boxToAdd);
                    }
                }
                context.KrzewyLisciaste.Add(krzewLisciasty);
            }
            else
            {
                KrzewLisciasty dbEntry = context.KrzewyLisciaste.Include(b => b.Boxes).Where(k => k.ProductID == krzewLisciasty.ProductID).FirstOrDefault();
                if (dbEntry != null)
                {
                    UpdateAssignedBoxesForKrzewLisciasty(selectedbox, dbEntry);
                    //Product
                    dbEntry.Name = krzewLisciasty.Name;
                    dbEntry.Date = krzewLisciasty.Date;
                    dbEntry.Description = krzewLisciasty.Description;
                    dbEntry.Price = krzewLisciasty.Price;
                    dbEntry.Discount = krzewLisciasty.Discount;
                    dbEntry.Description = krzewLisciasty.Description;
                    dbEntry.Available = krzewLisciasty.Available;
                    if (dbEntry.Available == true)
                    {
                        dbEntry.Quantity = krzewLisciasty.Quantity;
                    }
                    else
                    {
                        dbEntry.Quantity = null;
                    }
                    dbEntry.HeightMin = krzewLisciasty.HeightMin;
                    dbEntry.HeightMax = krzewLisciasty.HeightMax;
                    dbEntry.ImageData = krzewLisciasty.ImageData;
                    dbEntry.ImageMimeType = krzewLisciasty.ImageMimeType;
                    //IglakiGrunt
                }
            }
            context.SaveChanges();
        }

        public KrzewLisciasty DeleteKrzewLisciasty(int krzewLisciastyID)
        {
            KrzewLisciasty dbEntry = context.KrzewyLisciaste.Find(krzewLisciastyID);
            if (dbEntry != null)
            {
                context.KrzewyLisciaste.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        private void UpdateAssignedBoxesForKrzewLisciasty(string[] selectedBoxes, KrzewLisciasty lisciastyToUpdate)
        {
            if (selectedBoxes == null)
            {
                lisciastyToUpdate.Boxes = new List<Box>();
                return;
            }

            var selectedBoxesHS = new HashSet<string>(selectedBoxes);
            var krzewLisciastyBoxes = new HashSet<int>(lisciastyToUpdate.Boxes.Select(b => b.BoxID));
            foreach (var box in context.Boxes)
            {
                if (selectedBoxesHS.Contains(box.BoxID.ToString()))
                {
                    if (!krzewLisciastyBoxes.Contains(box.BoxID))
                    {
                        lisciastyToUpdate.Boxes.Add(box);
                    }
                }
                else
                {
                    if (krzewLisciastyBoxes.Contains(box.BoxID))
                    {
                        lisciastyToUpdate.Boxes.Remove(box);
                    }
                }
            }
        }
    }
}