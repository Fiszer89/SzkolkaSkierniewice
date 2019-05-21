using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Abstract;

namespace SzkolkaSkierniewice.Domain.Concrete
{
    public class EFProductRepository:IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}