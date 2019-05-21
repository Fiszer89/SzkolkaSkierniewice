using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;
using PagedList;

namespace SzkolkaSkierniewice.Controllers
{
    public class BoxesController : Controller
    {
        private IBoxRepository repository;

        public BoxesController(IBoxRepository boxRepository)
        {
            this.repository = boxRepository;
        }
    }
}