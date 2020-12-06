using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class KursInfoController : ApiController
    {
        private KursDBEntities db = new KursDBEntities();

        // GET: api/KursInfo
        public IQueryable<KursInfo> GetKursInfo()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.KursInfo;
        }

        // GET: api/KursInfo/5
        [ResponseType(typeof(KursInfo))]
        public IHttpActionResult GetKursInfo(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            KursInfo kursInfo = db.KursInfo.Find(id);
            if (kursInfo == null)
            {
                return NotFound();
            }

            return Ok(kursInfo);
        }

        // PUT: api/KursInfo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKursInfo(int id, KursInfo kursInfo)
        {
            db.Configuration.ProxyCreationEnabled = false;


            if (id != kursInfo.KursID)
            {
                return BadRequest();
            }

            db.Entry(kursInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KursInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/KursInfo
        [ResponseType(typeof(KursInfo))]
        public IHttpActionResult PostKursInfo(KursInfo kursInfo)
        {
          

            db.KursInfo.Add(kursInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kursInfo.KursID }, kursInfo);
        }

        // DELETE: api/KursInfo/5
        [ResponseType(typeof(KursInfo))]
        public IHttpActionResult DeleteKursInfo(int id)
        {
            KursInfo kursInfo = db.KursInfo.Find(id);
            if (kursInfo == null)
            {
                return NotFound();
            }

            db.KursInfo.Remove(kursInfo);
            db.SaveChanges();

            return Ok(kursInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KursInfoExists(int id)
        {
            return db.KursInfo.Count(e => e.KursID == id) > 0;
        }
    }
}