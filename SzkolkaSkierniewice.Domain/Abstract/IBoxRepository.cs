using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolkaSkierniewice.Domain.Entities;

namespace SzkolkaSkierniewice.Domain.Abstract
{
    public interface IBoxRepository
    {
        IEnumerable<Box> Boxes { get; }

        void SaveBox(Box box);

        Box DeleteBox(int boxID);
    }
}