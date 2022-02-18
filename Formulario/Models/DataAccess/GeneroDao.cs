using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Formulario.Models.DataModel;

namespace Formulario.Models.DataAccess
{
    public class GeneroDao
    {
        public void Crear(GeneroModel g)
        {
            using (var contexto = new UsuariosEntities())
            {
                Genero ge = new Genero();
                ge.Id_Genero = g.Id_Genero;
                ge.Nombre = g.Nombre;

                contexto.Genero.Add(ge);
                contexto.SaveChanges();
            }
        }

        public List<GeneroModel> Consultar()
        {
            List<GeneroModel> listasexos = new List<GeneroModel>();
            using (var contexto = new UsuariosEntities())
            {
                var consulta = (from d in contexto.Genero select d).ToList();
                foreach (var item in consulta)
                {
                    //dominio modem     //data model
                    GeneroModel x = new GeneroModel();
                    x.Id_Genero = item.Id_Genero;
                    x.Nombre = item.Nombre;
                    //van desde la dominio a la base de datos

                    listasexos.Add(x);
                }
            }
            return listasexos;
        }

        public GeneroModel ConsultarUno(string id)
        {
            using (var contexto = new UsuariosEntities())
            {
                GeneroModel genero = new GeneroModel();
                var registro = (from d in contexto.Genero select d).Where(d => d.Id_Genero.Equals(id)).FirstOrDefault();
                genero.Id_Genero = registro.Id_Genero;
                genero.Nombre = registro.Nombre;
                return genero;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                using (var contexto = new UsuariosEntities())
                {
                    var registro = (from d in contexto.Genero select d).Where(d => d.Id_Genero.Equals(id)).FirstOrDefault();
                    contexto.Genero.Remove(registro);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return false;
            }            
        }

        public void Actualizar(GeneroModel g, string preid)
        {
            if (g.Id_Genero != preid)
            {
                Crear(g);
                new UsuarioModel().ActualizarGeneroUsuarios(preid, g.Id_Genero);
                Eliminar(preid);
            }
            else
            {
                using (var contexto = new UsuariosEntities())
                {
                    var consulta = (from d in contexto.Genero select d).Where(d => d.Id_Genero.Equals(g.Id_Genero)).FirstOrDefault();
                    consulta.Nombre = g.Nombre;
                    consulta.Id_Genero = g.Id_Genero;
                    contexto.SaveChanges();
                }
            }            
        }
    }
}