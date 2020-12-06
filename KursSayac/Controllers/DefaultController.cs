using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using KursSayac.Models;

namespace KursSayac.Controllers
{
    public class DefaultController : Controller
    {
        KursDBEntities1 db = new KursDBEntities1();
        // GET: Default
        public ActionResult Index()
        {

            IEnumerable<mvcKursInfoModel> KursInfoList;
            HttpResponseMessage response = GobalVariables.WebApiClient.GetAsync("KursInfo").Result;
            KursInfoList = response.Content.ReadAsAsync<IEnumerable<mvcKursInfoModel>>().Result;


     //var detail = db.KursInfo.ToList();
     //       return View(detail);

            return View(KursInfoList);

        }


        public ActionResult CreateInfo()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateInfo(KursInfo k)
        {

            HttpResponseMessage response = GobalVariables.WebApiClient.PostAsJsonAsync("KursInfo",k).Result;

            //db.KursInfo.Add(k);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EditInfo(int id )
        {


            //mvcKursInfoModel KursInfo;
            HttpResponseMessage response = GobalVariables.WebApiClient.GetAsync("KursInfo/" + id.ToString()).Result;
            //KursInfo = response.Content.ReadAsAsync<mvcKursInfoModel>().Result;
            //var kurs = db.KursInfo.Find(id);

            return View(response.Content.ReadAsAsync<mvcKursInfoModel>().Result);
        }


        [HttpPost]
        public ActionResult EditInfo(int id, KursInfo k)
        {

            HttpResponseMessage response = GobalVariables.WebApiClient.PutAsJsonAsync("KursInfo/" + id.ToString(), k).Result;



            //var kurs = db.KursInfo.Find(id);
            //kurs.KursName = k.KursName;
            //db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult DeleteInfo(int id)
        {

            HttpResponseMessage response = GobalVariables.WebApiClient.DeleteAsync("KursInfo/" + id.ToString()).Result;


            //var kurs = db.KursInfo.Find(id);
            //db.KursInfo.Remove(kurs);

            return RedirectToAction("Index");
        }



        public ActionResult InfoDetails(int id)
        {


            var kursdetail = db.KursDetail.Where(x => x.KursID == id);


            return View(kursdetail);
        }










        public ActionResult CreateDetail()
        {
            ViewBag.KursID = new SelectList(db.KursInfo, "KursID", "KursName");

            return View();
        }

        [HttpPost]
        public ActionResult CreateDetail(KursDetail k)
        {

            db.KursDetail.Add(k);
            db.SaveChanges();
            return RedirectToAction("InfoDetails/"+k.KursID);
        }








        public ActionResult EditDetails(int id)
        {

            var kurs = db.KursDetail.Find(id);

            return View(kurs);
        }


        [HttpPost]
        public ActionResult EditDetails(int id, KursDetail k)
        {

            var kurs = db.KursDetail.Find(id);
            kurs.KursDate = k.KursDate;
            kurs.Person = k.Person;

            db.SaveChanges();
            string action = "InfoDetails/" + id;

            return RedirectToAction(action);
        }




    }
}