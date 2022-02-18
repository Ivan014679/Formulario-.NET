using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formulario.Models;

namespace Formulario.Controllers
{
    public class UsuarioController : Controller
    {
        static bool FueRegistrado = false;
        static bool FueActualizado = false;
        static bool FueEliminado = false;
        // GET: Usuario
        public ActionResult Index()
        {
            GenerarListas();
            return View();
        }

        [HttpPost]
        public ActionResult Index(UsuarioModel usuarioModel)
        {
            GenerarListas();
            if (ModelState.IsValid)
            {
                FueRegistrado = true;
                usuarioModel.GuardarUsuario();
                ViewBag.Registro = "Exito";                
                if (Session["email"] != null)
                {
                    return RedirectToAction("MostrarUsuarios");
                }
                else
                {
                    return RedirectToAction("Login");
                }                
            }
            else
            {
                return View(usuarioModel);
            }
        }

        public ActionResult Login()
        {
            if (!FueRegistrado)
            {
                ViewBag.Registro = null;
            }
            if (FueRegistrado)
            {
                ViewBag.Registro = "Exito";
                FueRegistrado = false;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuario = usuario.IniciarSesion(usuario.Email, usuario.Contrasena);
                if (usuario != null)
                {
                    Session["id"] = usuario.Id;
                    Session["nombre"] = usuario.Nombre;
                    Session["email"] = usuario.Email;
                    Session["pais"] = usuario.Pais;
                    Session["genero"] = usuario.Genero;
                    return RedirectToAction("MostrarUsuarios");
                }
                ViewBag.Error = "Error";
                return View();
            }
            else
            {
                return View(usuario);
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult MostrarUsuarios()
        {
            if (!FueRegistrado)
            {
                ViewBag.Registro = null;
            }
            if (!FueEliminado)
            {
                ViewBag.Deleted = null;
            }            
            if (!FueActualizado)
            {
                ViewBag.Update = null;
            }
            UsuarioModel usModel = new UsuarioModel();
            usModel.Lista = usModel.ObtenerUsuarios();
            if (FueRegistrado)
            {
                ViewBag.Registro = "Exito";
                FueRegistrado = false;
            }
            if (FueActualizado)
            {
                ViewBag.Update = "Exito";
                FueActualizado = false;
            }
            if (FueEliminado)
            {
                ViewBag.Deleted = "Exito";
                FueEliminado = false;
            }
            return View(usModel);
        }

        [HttpPost]
        public ActionResult ModificarUsuario(decimal id)
        {
            GenerarListas();
            UsuarioModel usModel = new UsuarioModel();
            usModel = usModel.ObtenerUsuario(id);
            return View(usModel);
        }

        [HttpPost]
        public ActionResult GuardarCambios(UsuarioModel usuario)
        {
            usuario.ActualizarUsuario();
            FueActualizado = true;
            return RedirectToAction("MostrarUsuarios");
        }

        [HttpPost]
        public ActionResult EliminarUsuario(decimal id)
        {
            UsuarioModel usModel = new UsuarioModel();
            usModel.EliminarUsuario(id);
            FueEliminado = true;
            return RedirectToAction("MostrarUsuarios");
        }

        public void GenerarListas()
        {
            List<SelectListItem> paisesRenderizar = new List<SelectListItem>();
            List<SelectListItem> generosRenderizar = new List<SelectListItem>();
            GeneroModel generos = new GeneroModel();
            PaisModel paises = new PaisModel();
            var listaGeneros = generos.ObtenerGeneros();
            var listaPaises = paises.ObtenerPaises();
            foreach (var item in listaPaises)
            {
                SelectListItem i = new SelectListItem();
                i.Text = item.Nombre;
                i.Value = item.Codigo;
                paisesRenderizar.Add(i);
            }
            foreach (var item in listaGeneros)
            {
                SelectListItem i = new SelectListItem();
                i.Text = item.Nombre;
                i.Value = item.Id_Genero;
                generosRenderizar.Add(i);
            }
            ViewBag.paises = paisesRenderizar;
            ViewBag.generos = generosRenderizar;
        }
    }
}