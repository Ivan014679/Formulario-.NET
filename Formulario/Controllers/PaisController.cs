using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formulario.Models;

namespace Formulario.Controllers
{
    public class PaisController : Controller
    {
        static bool FueIngresado = false;
        static bool FueActualizado = false;
        static bool NoEliminado = false;
        static bool EliminadoExitoso = false;
        // GET: Pais
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PaisModel paisModel)
        {
            if (ModelState.IsValid)
            {
                paisModel.GuardarPais();
                FueIngresado = true;
                return RedirectToAction("MostrarPaises");
            }
            else
            {
                return View(paisModel);
            }            
        }

        public ActionResult MostrarPaises()
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
            PaisModel pModel = new PaisModel();
            pModel.Lista = pModel.ObtenerPaises();
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
            return View(pModel);
        }

        [HttpPost]
        public ActionResult ModificarPais(string codigo)
        {
            PaisModel pModel = new PaisModel();
            pModel = pModel.ObtenerPais(codigo);
            return View(pModel);
        }

        [HttpPost]
        public ActionResult GuardarCambios(PaisModel pais, string precodigo)
        {
            pais.ActualizarPais(precodigo);
            FueActualizado = true;
            return RedirectToAction("MostrarPaises");
        }

        [HttpPost]
        public ActionResult EliminarPais(string codigo)
        {
            PaisModel pModel = new PaisModel();
            EliminadoExitoso = pModel.EliminarPais(codigo);
            if (!EliminadoExitoso)
            {
                NoEliminado = true;
            }            
            return RedirectToAction("MostrarPaises");
        }
    }
}