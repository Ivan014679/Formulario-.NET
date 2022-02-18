using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Formulario.Models.DataModel;

namespace Formulario.Models.DataAccess
{
    public class UsuarioDao
    {
        public void Crear(UsuarioModel u)
        {
            using (var contexto = new UsuariosEntities())
            {
                Usuario us = new Usuario();
                us.Nombre = u.Nombre;
                us.Pais = u.Pais;
                us.Email = u.Email;
                us.Contrasena = u.Contrasena;
                us.Genero = u.Genero;

                contexto.Usuario.Add(us);
                contexto.SaveChanges();
            }
        }

        public List<UsuarioModel> Consultar()
        {
            List<UsuarioModel> listausuarios = new List<UsuarioModel>();
            using (var contexto = new UsuariosEntities())
            {
                var consulta = (from d in contexto.Usuario select d).ToList();
                foreach (var item in consulta)
                {
                    //dominio modem     //data model
                    UsuarioModel x = new UsuarioModel();
                    x.Id = item.Id;
                    x.Nombre = item.Nombre;
                    x.Pais = item.Pais1.Nom_Pais;
                    x.Email = item.Email;
                    x.Genero = item.Genero1.Nombre;
                    //van desde la dominio a la base de datos

                    listausuarios.Add(x);
                }
            }
            return listausuarios;
        }

        public UsuarioModel ConsultarUno(decimal id)
        {
            using (var contexto = new UsuariosEntities())
            {
                UsuarioModel usuario = new UsuarioModel();
                var registro = (from d in contexto.Usuario select d).Where(d => d.Id.Equals(id)).FirstOrDefault();
                usuario.Id = registro.Id;
                usuario.Nombre = registro.Nombre;
                usuario.Email = registro.Email;
                usuario.Pais = registro.Pais;
                usuario.Genero = registro.Genero;
                return usuario;
            }
        }

        public UsuarioModel InicioSesion(string email, string contrasena)
        {
            try
            {
                using (var contexto = new UsuariosEntities())
                {
                    UsuarioModel usuario = new UsuarioModel();
                    var registro = (from d in contexto.Usuario select d).Where(d => d.Email.Equals(email)).Where(d => d.Contrasena.Equals(contrasena)).FirstOrDefault();
                    usuario.Id = registro.Id;
                    usuario.Nombre = registro.Nombre;
                    usuario.Email = registro.Email;
                    usuario.Pais = registro.Pais;
                    usuario.Genero = registro.Genero;
                    return usuario;
                }
            }
            catch(System.NullReferenceException)
            {
                return null;
            }
        }

        public void Eliminar(decimal id)
        {
            using (var contexto = new UsuariosEntities())
            {
                var registro = (from d in contexto.Usuario select d).Where(d => d.Id.Equals(id)).FirstOrDefault();
                contexto.Usuario.Remove(registro);
                contexto.SaveChanges();
            }
        }

        public void Actualizar(UsuarioModel u)
        {
            using (var contexto = new UsuariosEntities())
            {
                var consulta = (from d in contexto.Usuario select d).Where(d => d.Id.Equals(u.Id)).FirstOrDefault();
                consulta.Nombre = u.Nombre;
                consulta.Email = u.Email;
                consulta.Contrasena = u.Contrasena;
                consulta.Pais = u.Pais;
                consulta.Genero = u.Genero;
                contexto.SaveChanges();
            }
        }

        public void ModificarPaisUsuarios(string precodigo, string postcodigo)
        {
            using (var contexto = new UsuariosEntities())
            {
                var consulta = (from d in contexto.Usuario select d).Where(d => d.Pais.Equals(precodigo)).ToList();
                foreach (var item in consulta)
                {
                    item.Pais = postcodigo;
                    contexto.SaveChanges();
                }
            }
        }

        public void ModificarGeneroUsuarios(string preid, string postid)
        {
            using (var contexto = new UsuariosEntities())
            {
                var consulta = (from d in contexto.Usuario select d).Where(d => d.Genero.Equals(preid)).ToList();
                foreach (var item in consulta)
                {
                    item.Genero = postid;
                    contexto.SaveChanges();
                }
            }
        }
    }
}