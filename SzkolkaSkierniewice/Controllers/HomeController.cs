using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using OfficeOpenXml;
using SzkolkaSkierniewice.Models;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Infrastructure.Logic;

namespace SzkolkaSkierniewice.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDrzewoAlejoweRepository drzewoAlejoweRepository;
        private readonly IIglakGruntRepository iglakGruntRepository;
        private readonly IIglakPojemnikRepository iglakPojemnikRepository;
        private readonly IKrzewLisciastyRepository krzewLisciasityRepository;

        public HomeController(IDrzewoAlejoweRepository drzewoAlejowe, IIglakGruntRepository iglakGrunt, IIglakPojemnikRepository iglakPojemnik, IKrzewLisciastyRepository krzewLisciasty, IBoxRepository boxes, IPromotionRepository promotion, IProductRepository product, IGalleryImageRepository image)
        {
            drzewoAlejoweRepository = drzewoAlejowe;
            iglakGruntRepository = iglakGrunt;
            iglakPojemnikRepository = iglakPojemnik;
            krzewLisciasityRepository = krzewLisciasty;
        }

        [HttpGet]
        public FileContentResult ExportToExcel()
        {
            List<ExcelSheet> sheetsList = new List<ExcelSheet>();

            List<IglakGrunt> iglakiGrunt = iglakGruntRepository.IglakiGrunt.ToList();
            string[] columnsIglakiGrunt = { "Name", "HeightMin", "HeightMax", "Quantity", "Price", "Discount", "PriceAfterDicount", };
            ExcelSheet iglakiGruntSheet = new ExcelSheet(ExcelExport.ListToDataTable(iglakiGrunt), "Iglaki w gruncie", true, columnsIglakiGrunt);

            List<KrzewLisciasty> krzewyLisciaste = krzewLisciasityRepository.KrzewyLisciaste.ToList();
            string[] columnsLisciaste = { "Name", "HeightMin", "HeightMax", "Quantity", "Price", "Discount", "PriceAfterDicount", };
            ExcelSheet krzewyLisciasteSheet = new ExcelSheet(ExcelExport.ListToDataTable(krzewyLisciaste), "Krzewy liściaste", true, columnsLisciaste);

            List<IglakPojemnik> iglakiPojemnik = iglakPojemnikRepository.IglakiPojemnik.ToList();
            string[] columnsIglakiPojemnik = { "Name", "HeightMin", "HeightMax", "Quantity", "Price", "Discount", "PriceAfterDicount", };
            ExcelSheet iglakiPojemnikSheet = new ExcelSheet(ExcelExport.ListToDataTable(iglakiPojemnik), "Iglaki w pojemnikach", true, columnsIglakiPojemnik);

            List<DrzewoAlejowe> drzewaAlejowe = drzewoAlejoweRepository.DrzewaAlejowe.ToList();
            string[] columnsDrzewaAlejowe = { "Name", "HeightMin", "HeightMax", "Quantity", "Price", "Discount", "PriceAfterDicount", "WidthMin", "WidthMax" };
            ExcelSheet drzewaAlejoweSheet = new ExcelSheet(ExcelExport.ListToDataTable(drzewaAlejowe), "Drzewa alejowe", true, columnsDrzewaAlejowe);

            sheetsList.Add(iglakiGruntSheet);
            sheetsList.Add(krzewyLisciasteSheet);
            sheetsList.Add(iglakiPojemnikSheet);
            sheetsList.Add(drzewaAlejoweSheet);

            byte[] filecontent = Infrastructure.Logic.ExcelExport.ExportExcel(sheetsList);
            return File(filecontent, Infrastructure.Logic.ExcelExport.ExcelContentType, "Oferta.xlsx");
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Offer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress(c.Email.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.From = from;
                    msg.To.Add("szkolkaskc@gmail.com");
                    msg.Subject = "Wiadomość od klienta";
                    msg.IsBodyHtml = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("szkolkaskc@gmail.com", "adrian89");
                    sb.Append("First name: " + c.FirstName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.LastName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + c.Email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.Comment);
                    msg.Body = sb.ToString();
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View();
        }
    }
}