using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFBoxRepository:IBoxRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Box> Boxes
        {
            get { return context.Boxes; }
        }

        public void SaveBox(Box box)
        {
            if (box.BoxID == 0)
            {
                context.Boxes.Add(box);
            }
            else
            {
                Box dbEntry = context.Boxes.Find(box.BoxID);
                if (dbEntry != null)
                {
                    //Box
                    dbEntry.Name = box.Name;
                }
            }
            context.SaveChanges();
        }

        public Box DeleteBox(int boxID)
        {
            Box dbEntry = context.Boxes.Find(boxID);
            if (dbEntry != null)
            {
                context.Boxes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}