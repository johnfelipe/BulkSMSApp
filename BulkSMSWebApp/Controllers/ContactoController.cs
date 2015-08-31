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
using BulkSMSWebApp.Helpers;
using BulkSMSWebApp.ViewModels;

namespace BulkSMSWebApp.Controllers
{
    public class ContactoController : BaseController
    {
        private BulkSMSContext db = new BulkSMSContext();


        public bool numberExist(String number)
        {
            Boolean flag = false;

            var result = db.Telefonos.Where(c => c.NumeroTel == number).Select(c => c.NumeroTel).Count();

            if (result >= 1)
            {
                flag = true;
            }

            return flag;
        }
        public JsonResult getContacts(string term)
        {

            var contactos = db.Contactos.Where(c => c.Nombres.ToLower().Contains(term.ToLower()) || c.Apellidos.ToLower().Contains(term.ToLower()))
                .Select(n => n.Nombres + " " + n.Apellidos).ToList();

            return Json(contactos, JsonRequestBehavior.AllowGet);

        }

        public PartialViewResult GetTelephones(int? id)
        {
            var viewModel = new ContactoCelGrupo();
            if (Request.IsAjaxRequest())
            {
                if (id != null)
                {
                    ViewBag.ContactoID = id.Value;
                    viewModel.Telefonos = viewModel.Contactos.Where(i => i.ContactoID == id.Value).Single().Telefonos;
                    return PartialView("_TelefonosContacto", viewModel);
                }
            }

            return PartialView();
        }

        // GET: Contacto
        [NoCache]
        public ActionResult Index(int? id)
        {

            var viewModel = new ContactoCelGrupo();

            viewModel.Contactos = db.Contactos.Include(c => c.Departamento).Include(c => c.Estado).OrderByDescending(c => c.Nombres);


            if (Request.IsAjaxRequest())
            {
                if (id != null)
                {
                    ViewBag.ContactoID = id.Value;
                    viewModel.Telefonos = viewModel.Contactos.Where(i => i.ContactoID == id.Value).Single().Telefonos;
                    return PartialView("_TelefonosContacto", viewModel);

                }
            }

            return View(viewModel);

            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    contactos = contactos.Where(c => c.Nombres.ToLower().Contains(SearchString.ToLower()) || (c.Nombres + " " + c.Apellidos).ToLower().Contains(SearchString.ToLower()) || c.Apellidos.ToLower().Contains(SearchString.ToLower()));
            //}
            //return View(contactos.ToList());
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
            ViewBag.DepartamentoID = new SelectList(db.Departamentos.ToList(), "DepartamentoID", "NombreDepartameto");
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado");
            ViewData["Titulo"] = "Create";
            return View();
        }

        // POST: Contacto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "ContactoID,Nombres,Apellidos,Email,DepartamentoID")]*/CreateEditContacto contacto)
        {
            if (ModelState.IsValid)
            {
                bool band = false;
                string num = "";
                
                foreach (var tel in contacto.Telefonos)
                {
                    if (tel.NumeroTel.ToString().Substring(0, 4) != "+505")
                    {
                        tel.NumeroTel = "+505" + tel.NumeroTel;
                    }

                    if (numberExist(tel.NumeroTel))
                    {
                        num = tel.NumeroTel;
                        Danger(String.Format("El Número <b>{0}</b> ya se encuentra registrado. Por favor provea un número diferente", num), true);
                        ViewBag.DepartamentoID = new SelectList(db.Departamentos.ToList(), "DepartamentoID", "NombreDepartameto");
                        return View(contacto); 
                    }
                    else
                    {
                        band = true;
                    }
                }

                if (band)
                {
                    contacto.Contactos.EstadoID = 1;
                  //  contacto.Contactos.DepartamentoID = contacto.Contactos.Departamento.DepartamentoID;
                    db.Contactos.Add(contacto.Contactos);
                    db.SaveChanges();

                    foreach (var tel in contacto.Telefonos)
                    {
                        var id = contacto.Contactos.ContactoID;
                        tel.ContactoID = id;
                        db.Telefonos.Add(tel);
                        db.SaveChanges();
                    }

                    Success(String.Format("El Contacto <b>{0}</b> fué agregado correctamente.", contacto.Contactos.Nombres), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(String.Format("El Número <b>{0}</b> ya se encuentra registrado. Por favor provea un número diferente", num), true);
                    ViewBag.DepartamentoID = new SelectList(db.Departamentos.ToList(), "DepartamentoID", "NombreDepartameto");
                    ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.Contactos.EstadoID);
                    return View(contacto); 
                }

            }
            Danger("Error de Validación, verifica los datos e intenta nuevamente");
            ViewBag.DepartamentoID = new SelectList(db.Departamentos.ToList(), "DepartamentoID", "NombreDepartameto");
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.Contactos.EstadoID);
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
            ViewData["Titulo"] = "Edit";
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartameto", contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.EstadoID);
            return View(contacto);
        }

        // POST: Contacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactoID,Celular,Nombres,Apellidos,Email,EstadoID,DepartamentoID")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacto).State = EntityState.Modified;
                db.SaveChanges();
                Success("El Contacto ha sido Actualizado Correctamente", true);
                return RedirectToAction("Index");
            }
            Danger("Algo está mal! Verifica los campos que son requeridos e intenta guardar nuevamente", true);
            ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartameto", contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.EstadoID);
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
            Success("El Contacto ha sido Desactivado Correctamente", true);
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
            Success("El Contacto ha sido Activado Correctamente", true);
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
