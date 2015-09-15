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


        private bool numberExist(String number)
        {
            Boolean flag = false;

            var result = db.Telefonos.Where(c => c.NumeroTel == number).Select(c => c.NumeroTel).Count();

            if (result >= 1)
            {
                flag = true;
            }

            return flag;
        }


        private bool numberExist(string number, int contactoID)
        {

            bool flag = false;

            var result = from t in db.Telefonos
                         where (t.NumeroTel == number && t.ContactoID != contactoID)
                         select t.NumeroTel;

            if (result.Count() >= 1)
            {
                flag = true;
            }
            return flag;
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
        [Authorize]
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
        }


        // GET: Contacto/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contacto contacto = db.Contactos.Find(id);


        //    if (contacto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.id = id;
        //    return PartialView("_DetalleContacto", contacto);
        //}

        // GET: Contacto/Create
        [NoCache]
        [Authorize]
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
        [Authorize]
        [NoCache]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "ContactoID,Nombres,Apellidos,Email,DepartamentoID")]*/CreateEditContacto contacto)
        {
            if (ModelState.IsValid)
            {
                bool band = false;
                string num = "";

                //recorremos la lista de números
                foreach (var tel in contacto.Telefonos)
                {
                    //Si el número no viene con el prefijo +505 entonces se lo agregamos
                    if (tel.NumeroTel.ToString().Substring(0, 4) != "+505")
                    {
                        tel.NumeroTel = "+505" + tel.NumeroTel;
                    }

                    // Verificamos que ninguno de los numeros se encuentre registrado en la BD.
                    // Si alguno está registrado se cancela todo y se muestra una alerta
                    if (numberExist(tel.NumeroTel))
                    {
                        num = tel.NumeroTel;
                        Danger(String.Format("El Número <b>{0}</b> ya se encuentra registrado. Por favor provea un número diferente", num), true);
                        ViewBag.DepartamentoID = new SelectList(db.Departamentos.ToList(), "DepartamentoID", "NombreDepartameto");
                        return View(contacto);
                    }
                    else
                    {
                        // una vez que verificamos ningún número de la lista se encuentra registrado
                        // la bandera la hacemos verdadera.
                        band = true;
                    }
                }

                // Si la bandera es verdadera podemos proceder a guardar el contacto y sus números
                if (band)
                {
                    contacto.Contactos.EstadoID = 1; // Activo
                    db.Contactos.Add(contacto.Contactos);
                    db.SaveChanges();

                    // una vez que guardamos el contacto procedemos a guardar los teléfonos asociados a este.
                    foreach (var tel in contacto.Telefonos)
                    {
                        var id = contacto.Contactos.ContactoID;
                        tel.ContactoID = id;
                        db.Telefonos.Add(tel);
                        db.SaveChanges();
                    }

                    //Mostramos una alerta de éxito.
                    Information(String.Format("El Contacto <b>{0}</b> fué agregado correctamente.", contacto.Contactos.Nombres), true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Danger(String.Format("El Número <b>{0}</b> ya se encuentra registrado. Por favor provea un número diferente", num), true);
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
        [Authorize]
        [NoCache]
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
            //ViewBag.ContactoID = new SelectList(db.Contactos.Where(c => c.EstadoID == 1), "ContactoID", "NombreCompeto");
            //ViewBag.DepartamentoID = new SelectList(db.Departamentos, "DepartamentoID", "NombreDepartameto");

            //populateTelefonosDropDownList(contacto, contacto.Telefonos);
            PopulateDepartmentsDropDownList(contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.EstadoID);
            return View(contacto);
        }

        // POST: Contacto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [NoCache]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ContactoID,Nombres,Apellidos,Email,DepartamentoID")] Contacto contacto, List<Telefono> Tels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool band = false;
                    string num = "";
                    //recorremos la lista de números para verificar que ninguno de los números de la lista entrante se encuentre registrados
                    //y que pertenezcan a otro contacto.
                    foreach (var tel in Tels)
                    {
                        //Si el número no viene con el prefijo +505 entonces se lo agregamos
                        if (tel.NumeroTel.ToString().Substring(0, 4) != "+505")
                        {
                            tel.NumeroTel = "+505" + tel.NumeroTel;
                        }
                        // verificamos que los números ingresados no existan en la BD relacionados con otro contacto
                        // si algunno de los números ingresados ya pertenece a otro contacto se muestra una alerta y no se guardan los cambios.
                        if (numberExist(tel.NumeroTel, contacto.ContactoID))
                        {
                            num = tel.NumeroTel;
                            band = false;
                            PopulateDepartmentsDropDownList(contacto.DepartamentoID);
                            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.EstadoID);
                            Danger(String.Format("El Número <b>{0}</b> ya se encuentra asociado a otro contacto. Los cambios no se guardaron, por favor provea un número diferente", num), true);
                            return RedirectToAction("Edit", contacto.ContactoID);
                        }
                        else
                        {
                            // una vez que verificamos que ningún número de la lista se encuentra registrado
                            // la bandera la hacemos verdadera.
                            band = true;
                        }
                    }

                    if (band) // si ninguno de los números ingresados pertenece a otro contacto
                    {

                        contacto.EstadoID = 1;
                        db.Entry(contacto).State = EntityState.Modified;
                        db.SaveChanges(); // Guardamos el Contacto Modificado

                        //obtenemos la lista de TelefonoID que el contacto tiene actualmente registrados 
                        var result = GetRegisteredPhonesID(contacto.ContactoID);

                        List<int> IncomingTelID = new List<int>(); // Declaramos una Lista que se llenará con los teléfonos de la lista entrante.

                        foreach (var item in Tels) //Recorremos la lista de teléfonos entrantes.
                        {
                            var TelID = getTelephonesID(item.NumeroTel, contacto.ContactoID); //obtenemos el ID de cada teléfono de la lista entrante
                            IncomingTelID.Add(TelID); //Agregamos los ID de los teléfonos que vienen en la lista entrante
                        }

                        //Obtenemos la lista de teléfonos a eliminar
                        //Es decir, los que están en la lista de teléfonos registrados pero no en la lista de teléfonos entrante.
                        var phoneIDToDelete = result.Except(IncomingTelID).ToList();

                        if (phoneIDToDelete.Count > 0) //Si hay al menos un teléfono a eliminar
                        {
                            foreach (var TelToDelete in phoneIDToDelete) //recorremos la lista y lo eliminamos
                            {
                                Telefono tel = db.Telefonos.Find(TelToDelete);
                                db.Telefonos.Remove(tel);
                                db.SaveChanges();
                            }
                        }

                        //recorremos la lista de teléfonos
                        foreach (var tel in Tels)
                        {
                            tel.ContactoID = contacto.ContactoID;
                            var TelID = getTelephonesID(tel.NumeroTel, tel.ContactoID); //obtenemos el ID de cada teléfono de la lista entrante

                            if (TelID == null || TelID == 0)  // si no se encuentra el ID del teléfono (significa que no está registrado)
                            {
                                db.Telefonos.Add(tel);
                                db.SaveChanges(); //como no está registrado, gregamos el nuevos teléfono a la lista de teléfonos del contacto
                            }
                            else
                            {
                                tel.TelefonoID = TelID; // si encuentra el ID del teléfono (significa que ya está registrado)
                                db.Entry(tel).State = EntityState.Modified; // como está registrado en vez de insert hacemos un update.
                                db.SaveChanges(); // Actualizamos los que ya están registradoss
                            }
                        }

                        //Mostramos una alerta de éxito.
                        Information(String.Format("El Contacto <b>{0}</b> fué Actualizado correctamente.", contacto.Nombres), true);
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    Danger("Ha ocurrido un error y los cambios no se guardaron, por favor contacta con el administrador del sistema \n "+ex.Message, true);
                    PopulateDepartmentsDropDownList(contacto.DepartamentoID);
                    ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.EstadoID);
                    return RedirectToAction("Edit", contacto.ContactoID);
 
                }
                
            }
            Danger("Error de Validación en los datos, verifica todos los campos e intenta nuevamente", true);
            PopulateDepartmentsDropDownList(contacto.DepartamentoID);
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "NombreEstado", contacto.EstadoID);
            return RedirectToAction("Edit", contacto.ContactoID);
        }

        private List<int> GetRegisteredPhonesID(int ContactoID)
        {
            var CurrentCellphoneList = (from T in db.Telefonos
                                        where (T.ContactoID == ContactoID)
                                        select T.TelefonoID).ToList();

            return CurrentCellphoneList;
        }


        // GET: Contacto/Delete/5
        [Authorize(Roles="Administrador")]
        [NoCache]
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
        [Authorize(Roles = "Administrador")]
        [NoCache]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            contacto.EstadoID = 2; // no lo eliminamos sino que lo desactivamos
            //db.Contactos.Remove(contacto);
            db.SaveChanges();
            Information(String.Format("El Contacto <b>{0}</b> fué desactivado correctamente.", contacto.Nombres), true);
            return RedirectToAction("Index");
        }

        // GET: Contacto/Activar/5
        //public ActionResult Activar(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contacto contacto = db.Contactos.Find(id);
        //    if (contacto == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contacto);
        //}
        [Authorize(Roles = "Administrador")]
        [NoCache]

        [HttpPost, ActionName("Activar")]
        [ValidateAntiForgeryToken]
        public ActionResult ActivarConfirmado(int id)
        {
            Contacto contacto = db.Contactos.Find(id); // Buscamos el contacto según el ID dado
            contacto.EstadoID = 1; // Cambiamos el estado a "Activo"
            db.SaveChanges(); // Guardamos los cambios
            Information(String.Format("El Contacto <b>{0}</b> fué Activado correctamente.", contacto.Nombres), true);
            return RedirectToAction("Index");
        }



        private int getTelephonesID(string number, int contID)
        {
            var telID = (from T in db.Telefonos
                         where (T.NumeroTel == number && T.ContactoID == contID)
                         select T.TelefonoID).FirstOrDefault();
            return telID;
        }

        private void populateTelefonosDropDownList(Contacto c, object selectedTel = null)
        {
            var telefonos = from t in db.Telefonos
                            orderby t.Descripcion
                            select t;

            ViewBag.TipoTelefono = new SelectList(telefonos, "TelefonoID", "Descripcion", selectedTel);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.Departamentos
                                   orderby d.NombreDepartameto
                                   select d;
            ViewBag.DepartamentoID = new SelectList(departmentsQuery, "DepartamentoID", "NombreDepartameto", selectedDepartment);
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
