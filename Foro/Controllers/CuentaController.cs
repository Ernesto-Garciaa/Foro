using Foro.Data;
using Microsoft.AspNetCore.Mvc;
using Foro.Models;
using System.Data.SqlClient;
using System.Data;

namespace Foro.Controllers
{
    public class CuentaController : Controller
    {
        private readonly DbContext _context;

        public CuentaController(DbContext context)
        {
            _context = context;
        }

        //Crear las vistas de registro
        public ActionResult Registrar()
        {
            return View("Registrar");
        }

        [HttpPost]
        public ActionResult Registrar(UsuarioModel u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new(_context.Valor))
                    {
                        using (SqlCommand cmd = new("sp_registrar", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@tipoUsuario", SqlDbType.Int).Value = u.idTipoUsuario;
                            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = u.nombre;
                            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = u.email;
                            cmd.Parameters.Add("@contrasenia", SqlDbType.VarChar).Value = u.contrasenia;
                            SqlParameter outputParam = cmd.Parameters.Add("@result", SqlDbType.Int);
                            outputParam.Direction = ParameterDirection.Output; // establecer el parametro como salida
                            con.Open();
                            cmd.ExecuteNonQuery();
                            int result = (int)outputParam.Value; //obtener el valor de salida
                            con.Close();
                            if (result == -1)
                            {
                                ViewData["Error"] = "Ya existe un usuario con ese nombre, prueba con otro";
                                return View("Registrar");
                            }
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "No se pudo registrar al usuario. Error: " + ex.Message;
                return View("Registrar");
            }
            ViewData["error"] = "Error de credenciales";
            return View("Registrar");
        }




        public ActionResult Login()
        {
            return View("Login");
        }




        [HttpPost]
        public ActionResult Login(LoginModel l)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new(_context.Valor))
                    {
                        using (SqlCommand cmd = new("sp_consultar", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = l.nombre;
                            cmd.Parameters.Add("@contrasenia", SqlDbType.VarChar).Value = l.contrasenia;
                            con.Open();

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                Response.Cookies.Append("user", "Bienvenido " + l.nombre);
                                var model = new LoginModel { nombre = l.nombre, contrasenia = l.contrasenia };
                                return RedirectToAction("mostrarPreguntas", "Preguntas");
                            }
                            else
                            {
                                ViewData["error"] = "Error de credenciales";
                            }

                            con.Close();
                        }

                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {

                return View("Login");
            }
            return View("Login");
        }


        //Eliminar la coockie


        public ActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }

       

        public IActionResult Index()
        {
            return View();
            
        }

       
        
    }

}

