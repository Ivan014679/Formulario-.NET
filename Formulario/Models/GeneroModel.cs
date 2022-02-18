using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Formulario.Models.DataAccess;

namespace Formulario.Models
{
    public class GeneroModel
    {
        [Display(Name = "Sexo")]
        [StringLength(maximumLength: 10)]
        public string Nombre { get; set; }
        [Display(Name = "Id del género")]
        [StringLength(maximumLength: 1, MinimumLength = 1)]
        public string Id_Genero { get; set; }

        public List<GeneroModel> Lista { get; set; }

        public void GuardarGenero()
        {
            //1. Instanciar el dao
            GeneroDao gdao = new GeneroDao();
            //1.1. Incluir validaciones
            //2. Invocar al método crear
            gdao.Crear(this);
        }

        public List<GeneroModel> ObtenerGeneros()
        {
            //1. Instanciar el dao
            GeneroDao gdao = new GeneroDao();
            //1.1. Incluir validaciones
            //2. Retornar el listado de usuarios desde el dao
            return gdao.Consultar();
        }

        public GeneroModel ObtenerGenero(string id)
        {
            GeneroDao gdao = new GeneroDao();
            return gdao.ConsultarUno(id);
        }

        public bool EliminarGenero(string id)
        {
            GeneroDao gdao = new GeneroDao();
            return gdao.Eliminar(id);
        }

        public void ActualizarGenero(string preid)
        {
            GeneroDao gdao = new GeneroDao();
            gdao.Actualizar(this, preid);
        }
    }
}