using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IKrzewLisciastyRepository
    {
        IEnumerable<KrzewLisciasty> KrzewyLisciaste { get; }

        void SaveKrzewLisciasty(string[] selectedbox, KrzewLisciasty krzewLisciasty);

        KrzewLisciasty DeleteKrzewLisciasty(int productID);
    }
}