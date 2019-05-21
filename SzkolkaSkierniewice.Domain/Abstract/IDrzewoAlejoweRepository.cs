using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IDrzewoAlejoweRepository
    {
        IEnumerable<DrzewoAlejowe> DrzewaAlejowe { get; }

        void SaveDrzewoAlejowe(DrzewoAlejowe drzewoAlejowe);

        DrzewoAlejowe DeleteDrzewoAlejowe(int productID);
    }
}