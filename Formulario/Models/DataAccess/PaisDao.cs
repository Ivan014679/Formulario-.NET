using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Formulario.Models.DataModel;

namespace Formulario.Models.DataAccess
{
    public class PaisDao
    {
        public void Crear(PaisModel p)
        {
            using (var contexto = new UsuariosEntities())
            {
                Pais pa = new Pais();
                pa.Id_Pais = p.Codigo;
                pa.Nom_Pais = p.Nombre;

                contexto.Pais.Add(pa);
                contexto.SaveChanges();
            }
        }

        public List<PaisModel> Consultar()
        {
            List<PaisModel> listapaises = new List<PaisModel>();
            using (var contexto = new UsuariosEntities())
            {
                var consulta = (from d in contexto.Pais select d).ToList();
                foreach (var item in consulta)
                {
                    //dominio modem     //data model
                    PaisModel x = new PaisModel();
                    x.Codigo = item.Id_Pais;
                    x.Nombre = item.Nom_Pais;
                    //van desde la dominio a la base de datos

                    listapaises.Add(x);
                }
            }
            return listapaises;
        }

        public PaisModel ConsultarUno(string codigo)
        {
            using (var contexto = new UsuariosEntities())
            {
                PaisModel pais = new PaisModel();
                var registro = (from d in contexto.Pais select d).Where(d => d.Id_Pais.Equals(codigo)).FirstOrDefault();
                pais.Codigo = registro.Id_Pais;
                pais.Nombre = registro.Nom_Pais;
                return pais;
            }
        }

        public bool Eliminar(string codigo)
        {
            try
            {
                using (var contexto = new UsuariosEntities())
                {
                    var registro = (from d in contexto.Pais select d).Where(d => d.Id_Pais.Equals(codigo)).FirstOrDefault();
                    contexto.Pais.Remove(registro);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return false;
            }
        }

        public void Actualizar(PaisModel p, string precodigo)
        {
            if (p.Codigo != precodigo) { 
                Crear(p);
                new UsuarioModel().ActualizarPaisUsuarios(precodigo, p.Codigo);
                Eliminar(precodigo);
            }
            else
            {
                using (var contexto = new UsuariosEntities())
                {
                    var consulta = (from d in contexto.Pais select d).Where(d => d.Id_Pais.Equals(p.Codigo)).FirstOrDefault();
                    consulta.Nom_Pais = p.Nombre;
                    consulta.Id_Pais = p.Codigo;
                    contexto.SaveChanges();
                }
            }            
        }
    }
}