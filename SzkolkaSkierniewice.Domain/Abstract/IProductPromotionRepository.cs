using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    interface IProductPromotionRepository
    {
        IEnumerable<ProductInPromotion> ProductPromotion { get; }

        void SaveProductPromotion(ProductInPromotion productPromotion);

        ProductInPromotion DeleteProductPromotion(int productPromotionID);
    }
}
