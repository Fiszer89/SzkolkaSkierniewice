using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IIglakPojemnikRepository
    {
        IEnumerable<IglakPojemnik> IglakiPojemnik { get; }

        void SaveIglakPojemnik(string[] selectedbox, IglakPojemnik iglakPojemnik);

        IglakPojemnik DeleteIglakPojemnik(int productID);
    }
}