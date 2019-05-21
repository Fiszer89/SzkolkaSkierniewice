using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFPromotionRepository:IPromotionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Promotion> Promotions
        {
            get { return context.Promotions; }
        }

        public void SavePromotion(Promotion promotion, List<ProductInPromotion> productsInPromotion)
        {
            if (promotion.PromotionID == 0)
            {
                if (productsInPromotion != null)
                {
                    //ProductInPromotion p = new ProductInPromotion();
                    //p.ProductID = productID;
                    //p.QuantityOfProductsInPromotion = quantity;
                    //p.PromotionID = promotion.PromotionID;
                    //promotion.ProductPromotionList.Add(p);
                }
                context.Promotions.Add(promotion);
            }
            else
            {
                Promotion dbEntry = context.Promotions.Find(promotion.PromotionID);
                if (dbEntry != null)
                {
                    //Promotion
                    dbEntry.Name = promotion.Name;
                    dbEntry.Date = promotion.Date;
                    dbEntry.EndDate = promotion.EndDate;
                    dbEntry.Description = promotion.Description;
                    dbEntry.Price = promotion.Price;
                    dbEntry.Link = promotion.Link;
                    dbEntry.FreeGardenProject = promotion.FreeGardenProject;
                }
            }
            context.SaveChanges();
        }

        public Promotion DeletePromotion(int promotionID)
        {
            Promotion dbEntry = context.Promotions.Find(promotionID);
            if (dbEntry != null)
            {
                context.Promotions.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}