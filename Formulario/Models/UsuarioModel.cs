using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Formulario.Models.DataAccess;

namespace Formulario.Models
{
    public class UsuarioModel
    {
        public decimal Id { get; set; }
        [Display(Name = "Nombre completo")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Nombre { get; set; }
        [Display(Name = "Correo electrónico")]
        [StringLength(maximumLength: 60, MinimumLength = 10)]
        [DataType(dataType: DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Contraseña")]
        [StringLength(maximumLength: 60, MinimumLength = 5)]
        [DataType(dataType: DataType.Password)]
        public string Contrasena { get; set; }
        [Display(Name = "País de origen")]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Pais { get; set; }
        [Display(Name = "Sexo")]
        public string Genero { get; set; }

        public List<UsuarioModel> Lista { get; set; }

        /// <summary>
        /// Esta operación permitirá guardar una operación en una base de datos.
        /// </summary>
        public void GuardarUsuario()
        {
            //1. Instanciar el dao
            UsuarioDao usdao = new UsuarioDao();
            //1.1. Incluir validaciones
            //2. Invocar al método crear
            usdao.Crear(this);
        }

        public List<UsuarioModel> ObtenerUsuarios()
        {
            //1. Instanciar el dao
            UsuarioDao usdao = new UsuarioDao();
            //1.1. Incluir validaciones
            //2. Retornar el listado de usuarios desde el dao
            return usdao.Consultar();
        }

        public UsuarioModel ObtenerUsuario(decimal id)
        {
            UsuarioDao usdao = new UsuarioDao();
            return usdao.ConsultarUno(id);
        }

        public UsuarioModel IniciarSesion(string email, string contrasena)
        {
            UsuarioDao usdao = new UsuarioDao();
            return usdao.InicioSesion(email, contrasena);
        }

        public void EliminarUsuario(decimal id)
        {
            UsuarioDao usdao = new UsuarioDao();
            usdao.Eliminar(id);
        }

        public void ActualizarUsuario()
        {
            UsuarioDao usdao = new UsuarioDao();
            usdao.Actualizar(this);
        }

        public void ActualizarPaisUsuarios(string precodigo, string postcodigo)
        {
            UsuarioDao usdao = new UsuarioDao();
            usdao.ModificarPaisUsuarios(precodigo, postcodigo);
        }

        public void ActualizarGeneroUsuarios(string preid, string postid)
        {
            UsuarioDao usdao = new UsuarioDao();
            usdao.ModificarGeneroUsuarios(preid, postid);
        }
    }
}