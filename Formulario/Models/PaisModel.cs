using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Formulario.Models.DataAccess;

namespace Formulario.Models
{
    public class PaisModel
    {
        [Display (Name = "Nombre del país")]
        [StringLength (maximumLength: 50)]
        public string Nombre { get; set; }
        [Display (Name = "Código del país")]
        [StringLength (maximumLength: 3, MinimumLength = 3)]
        public string Codigo { get; set; }

        public List<PaisModel> Lista { get; set; }

        public List<PaisModel> ObtenerPaises()
        {
            //1. Instanciar el dao
            PaisDao pdao = new PaisDao();
            //1.1. Incluir validaciones
            //2. Retornar el listado de usuarios desde el dao
            return pdao.Consultar();
        }

        public PaisModel ObtenerPais(string codigo)
        {
            PaisDao pdao = new PaisDao();
            return pdao.ConsultarUno(codigo);
        }

        public void GuardarPais()
        {
            //1. Instanciar el dao
            PaisDao padao = new PaisDao();
            //1.1. Incluir validaciones
            //2. Invocar al método crear
            padao.Crear(this);
        }

        public bool EliminarPais(string codigo)
        {
            PaisDao pdao = new PaisDao();
            return pdao.Eliminar(codigo);
        }

        public void ActualizarPais(string precodigo)
        {
            PaisDao pdao = new PaisDao();
            pdao.Actualizar(this, precodigo);
        }
    }

}