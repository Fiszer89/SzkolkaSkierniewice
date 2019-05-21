using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IIglakGruntRepository
    {
        IEnumerable<IglakGrunt> IglakiGrunt { get; }

        void SaveIglakGrunt(IglakGrunt iglakGrunt);

        IglakGrunt DeleteIglakGrunt(int iglakGruntID);
    }
}