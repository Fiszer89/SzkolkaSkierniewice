using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IPromotionRepository
    {
        IEnumerable<Promotion> Promotions { get; }

        void SavePromotion(Promotion promotion, List<ProductInPromotion> productsInPromotion);

        Promotion DeletePromotion(int promotionID);
    }
}