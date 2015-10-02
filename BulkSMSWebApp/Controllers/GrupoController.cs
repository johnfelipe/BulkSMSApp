using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BulkSMSWebApp.DAL;
using BulkSMSWebApp.Models;
using System.Data.SqlClient;
using BulkSMSWebApp.Helpers;
using BulkSMSWebApp.ViewModels;

namespace BulkSMSWebApp.Controllers
{
    public class GrupoController : BaseController
    {
        private BulkSMSContext db = new BulkSMSContext();


       //GET
        public ActionResult ContactosNotInGroup(int? id)
        {
            if (id == null)
            {
                Danger("Error, no se ha encontrado el registro seleccionado", true);
                return View("Index");
            }

            var query = "SELECT T.TelefonoID, C.Nombres+' '+C.Apellidos [Nombres],"
                        + " D.NombreDepartameto, T.NumeroTel, T.Descripcion"
                        + " FROM Contacto C, Telefono T, Departamento D"
                        + " WHERE (C.ContactoID=T.ContactoID and"
                        + " D.DepartamentoID=C.DepartamentoID and T.TelefonoID"
                        + " not in (select T.TelefonoID from GrupoContacto T where T.GrupoID={0}))";

            var contactos = db.Database.SqlQuery<GrupoContacto>(query, id).ToList();
            ViewData["CodGrupo"] = id.Value;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AgregarContactos", contactos);
            }

            return View();
        }


        //POST
        [Authorize]
        public ActionResult UpdateGrupo(List<int> TelefonoID, int CodGrupo)
        {
            if (TelefonoID == null)
            {
                Danger("Error, no se ha encontrado el registro seleccionado", true);
                return View("Index");
            }
            else
            {
                try
                {
                    foreach (var tel in TelefonoID)
                    {
                        var query = db.Database.ExecuteSqlCommand("INSERT INTO GrupoContacto(GrupoID, TelefonoID) VALUES ({0},{1})", CodGrupo, tel);
                    }

                    var select = "SELECT T.TelefonoID, GR.GrupoID, C.Nombres+' '+C.Apellidos [Nombres],"
                                  + " D.NombreDepartameto, T.NumeroTel, T.Descripcion"
                                  + " FROM Contacto C, Telefono T, GrupoContacto G, Grupo GR , Departamento D"
                                  + " WHERE (C.ContactoID=T.ContactoID and D.DepartamentoID=C.DepartamentoID"
                                  + " and G.TelefonoID=T.TelefonoID and G.GrupoID=GR.GrupoID and C.EstadoID=1 and G.GrupoID={0})";

                    var contactos = db.Database.SqlQuery<GrupoContacto>(select, CodGrupo).ToList();

                    if (Request.IsAjaxRequest())
                    {
                        System.Threading.Thread.Sleep(1500);
                        return PartialView("_contactosGrupos", contactos);
                    }
                }

                catch (Exception ex)
                {
                    Danger("Ha ocurrido un error, por favor contacta al Administrador del Sistema: \n Detalle del error: " + ex.Message);
                    return RedirectToAction("Index");
                }   
            }

            return RedirectToAction("Index");
        }

        //GET
        public ActionResult ContactosGrupo(int? id)
        {

            if (id == null)
            {
                Danger("Error, no se ha encontrado el registro seleccionado", true);
                return View("Index");
            }

            var query = "SELECT T.TelefonoID, GR.GrupoID, C.Nombres+' '+C.Apellidos [Nombres],"
                                  + " D.NombreDepartameto, T.NumeroTel, T.Descripcion"
                                  + " FROM Contacto C, Telefono T, GrupoContacto G, Grupo GR , Departamento D"
                                  + " WHERE (C.ContactoID=T.ContactoID and D.DepartamentoID=C.DepartamentoID"
                                  + " and G.TelefonoID=T.TelefonoID and G.GrupoID=GR.GrupoID and C.EstadoID=1 and G.GrupoID={0})";

            var contactos = db.Database.SqlQuery<GrupoContacto>(query, id).ToList();

            if (Request.IsAjaxRequest())
            {
                System.Threading.Thread.Sleep(2000);
                return PartialView("_contactosGrupos", contactos);
            }

            return View("Index");
        }




        // GET: Grupo
        [NoCache]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Grupos.ToListAsync());
        }

        // GET: Grupo/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = await db.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // GET: Grupo/Create
        [Authorize]
        [NoCache]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grupo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GrupoID,NombreGrupo,Descripcion")] Grupo grupo, List<int> check)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    grupo.FechaCreacion = DateTime.Now;
                    grupo.EstadoID = 1;
                    db.Grupos.Add(grupo);
                    await db.SaveChangesAsync();

                    foreach (var tel in check)
                    {

                        var query = db.Database.ExecuteSqlCommand("INSERT INTO GrupoContacto(GrupoID, TelefonoID) VALUES ({0},{1})", grupo.GrupoID, tel);
                    }

                    Information(String.Format("El Grupo <b>{0}</b> ha sido registrado correctamente.", grupo.NombreGrupo), true);
                    return RedirectToAction("Create");

                }
                catch (Exception ex)
                {
                    Danger("Ha ocurrido un error en la operación, no se guardaron los cambios!! Por favor contacta al Administrador del Sistema \n Detalles del error: " + ex.Message);
                    return View(grupo);
                }
            }

            return View(grupo);
        }

        // GET: Grupo/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = await db.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
     
            System.Threading.Thread.Sleep(1500);
            return PartialView("_EditGrupo", grupo);
        }

        // POST: Grupo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GrupoID,NombreGrupo,Descripcion,FechaCreacion")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    grupo.EstadoID = 1;
                    db.Entry(grupo).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    var grupos = db.Grupos.ToList();
                    System.Threading.Thread.Sleep(1000);
                    return PartialView("_IndexPartialView", grupos);
                }
                catch (Exception ex)
                {
 
                }
                
            }
            return View(grupo);
        }

        // GET: Grupo/Delete/5
        //[Authorize]
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Grupo grupo = await db.Grupos.FindAsync(id);
        //    if (grupo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(grupo);
        //}

        // POST: Grupo/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    Grupo grupo = await db.Grupos.FindAsync(id);
        //    db.Grupos.Remove(grupo);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        public ActionResult BorrarContactos(List<int> TelefonoID, int CodGrupo)
        {
            if (TelefonoID == null)
            {
                Danger("Error, no se ha encontrado el registro seleccionado", true);
                return View("Index");
            }
            else
            {
                try
                {
                    foreach (var tel in TelefonoID)
                    {
                        var query = db.Database.ExecuteSqlCommand("DELETE FROM GrupoContacto WHERE GrupoID = {0} AND TelefonoID = {1}", CodGrupo, tel);
                    }

                    var select = "SELECT GR.GrupoID, C.Nombres+' '+C.Apellidos [Nombres],"
                                  + " D.NombreDepartameto, T.NumeroTel, T.Descripcion"
                                  + " FROM Contacto C, Telefono T, GrupoContacto G, Grupo GR , Departamento D"
                                  + " WHERE (C.ContactoID=T.ContactoID and D.DepartamentoID=C.DepartamentoID"
                                  + " and G.TelefonoID=T.TelefonoID and G.GrupoID=GR.GrupoID and C.EstadoID=1 and G.GrupoID={0})";

                    var contactos = db.Database.SqlQuery<GrupoContacto>(select, CodGrupo).ToList();

                    if (Request.IsAjaxRequest())
                    {
                        System.Threading.Thread.Sleep(1500);
                        return PartialView("_contactosGrupos", contactos);
                    }
                }

                catch (Exception ex)
                {
                    Danger("Ha ocurrido un error, por favor contacta al Administrador del Sistema: \n Detalle del error: " + ex.Message);
                    return RedirectToAction("Index");
                }
            }
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
