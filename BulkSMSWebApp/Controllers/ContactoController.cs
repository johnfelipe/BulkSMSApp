using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BulkSMSWebApp.DAL;
using BulkSMSWebApp.Models;

namespace BulkSMSWebApp.Controllers
{
    public class ContactoController : Controller
    {
        private BulkSMSContext db = new BulkSMSContext();


        public JsonResult getContacts(string term)
        {

            //List<String> contactos;

            var contactos = db.Contactos.Where(c => c.Nombres.ToLower().Contains(term.ToLower()) || c.Apellidos.ToLower().Contains(term.ToLower()))
                .Select(n => n.Nombres+" "+n.Apellidos).ToList();

            return Json(contactos, JsonRequestBehavior.AllowGet); 

        }


        // GET: Contacto
        public ActionResult Index(string SearchString)
        {
        
            var contactos = db.Contactos.Include(c => c.Departamento).Include(c => c.Estado);
   
            if (!String.IsNullOrEmpty(SearchString))
            {
                contactos = contactos.Where(c => c.Nombres.ToLower().Contains(SearchString.ToLower()) || (c.Nombres+" "+c.Apellidos).ToLower().Contains(SearchString.ToLower()) ||  c.Apellidos.ToLower().Contains(SearchString.ToLower()));
            }

            return View(contactos.ToList());
        }



        // GET: Contacto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // GET: Contacto/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "ID", "NombreDepartameto");
            ViewBag.EstadoID = new SelectList(db.Estados, "ID", "NombreEstado");
            ViewData["Titulo"] = "Create";
            return View();
        }

        // POST: Contacto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Celular,Nombres,Apellidos,Email,EstadoID,DepartamentoID")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.EstadoID = 1;
                db.Contactos.Add(contacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "ID", "NombreDepartameto", contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "ID", "NombreEstado", contacto.EstadoID);
            return View(contacto);
        }

        // GET: Contacto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "ID", "NombreDepartameto", contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "ID", "NombreEstado", contacto.EstadoID);
            ViewData["Titulo"] = "Edit";
            return View(contacto);
        }

        // POST: Contacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Celular,Nombres,Apellidos,Email,EstadoID,DepartamentoID")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "ID", "NombreDepartameto", contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "ID", "NombreEstado", contacto.EstadoID);
            return View(contacto);
        }

        // GET: Contacto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // POST: Contacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            contacto.EstadoID = 2; // no lo eliminamos sino que lo desactivamos
            //db.Contactos.Remove(contacto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contacto/Activar/5
        public ActionResult Activar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        [HttpPost, ActionName("Activar")]
        [ValidateAntiForgeryToken]
        public ActionResult ActivarConfirmado(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            contacto.EstadoID = 1; // Cambiamos el estado a "Activo"
            db.SaveChanges(); // Guardamos los cambios
            return RedirectToAction("Index");
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
