using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;
using System.Collections.Generic;
using System;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFProductPromotionRepository:IProductPromotionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<ProductInPromotion> ProductPromotion
        {
            get { return context.ProductsInPromotion; }
        }

        public void SaveProductPromotion(ProductInPromotion productPromotion)
        {
            if (productPromotion.ProductInPromotionID == 0)
            {
                context.ProductsInPromotion.Add(productPromotion);
            }
            else
            {
                ProductInPromotion dbEntry = context.ProductsInPromotion.Find(productPromotion.ProductInPromotionID);
                if (dbEntry != null)
                {
                    //ProductPromotion
                    dbEntry.ProductID = productPromotion.ProductID;
                    dbEntry.PromotionID = productPromotion.PromotionID;
                    dbEntry.QuantityOfProductsInPromotion = productPromotion.QuantityOfProductsInPromotion;
                }
            }
            context.SaveChanges();
        }

        public ProductInPromotion DeleteProductPromotion(int productPromotionID)
        {
            ProductInPromotion dbEntry = context.ProductsInPromotion.Find(productPromotionID);
            if (dbEntry != null)
            {
                context.ProductsInPromotion.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}