using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formulario.Models;

namespace Formulario.Controllers
{
    public class GeneroController : Controller
    {
        static bool FueIngresado = false;
        static bool FueActualizado = false;
        static bool NoEliminado = false;
        static bool EliminadoExitoso = false;
        // GET: Genero
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(GeneroModel generoModel)
        {
            if (ModelState.IsValid)
            {
                generoModel.GuardarGenero();
                FueIngresado = true;
                return RedirectToAction("MostrarGeneros");
            }
            else
            {
                return View(generoModel);
            }
        }

        public ActionResult MostrarGeneros()
        {
            if (!NoEliminado && !EliminadoExitoso)
            {
                ViewBag.Deleted = " ";
            }
            else if (EliminadoExitoso)
            {
                ViewBag.Deleted = "Exito";
                EliminadoExitoso = false;
            }
            else if (NoEliminado)
            {
                ViewBag.Deleted = null;
                NoEliminado = false;
            }
            if (!FueIngresado)
            {
                ViewBag.Ingreso = null;
            }
            if (!FueActualizado)
            {
                ViewBag.Update = null;
            }
            GeneroModel gModel = new GeneroModel();
            gModel.Lista = gModel.ObtenerGeneros();
            if (FueIngresado)
            {
                ViewBag.Ingreso = "Exito";
                FueIngresado = false;
            }
            if (FueActualizado)
            {
                ViewBag.Update = "Exito";
                FueActualizado = false;
            }
            return View(gModel);
        }

        [HttpPost]
        public ActionResult ModificarGenero(string id)
        {
            GeneroModel gModel = new GeneroModel();
            gModel = gModel.ObtenerGenero(id);
            return View(gModel);
        }

        [HttpPost]
        public ActionResult GuardarCambios(GeneroModel genero, string preid)
        {
            genero.ActualizarGenero(preid);
            FueActualizado = true;
            return RedirectToAction("MostrarGeneros");
        }

        [HttpPost]
        public ActionResult EliminarGenero(string id)
        {
            GeneroModel gModel = new GeneroModel();
            EliminadoExitoso = gModel.EliminarGenero(id);
            if (!EliminadoExitoso)
            {
                NoEliminado = true;
            }
            return RedirectToAction("MostrarGeneros");
        }
    }
}