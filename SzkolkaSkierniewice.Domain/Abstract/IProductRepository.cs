using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}