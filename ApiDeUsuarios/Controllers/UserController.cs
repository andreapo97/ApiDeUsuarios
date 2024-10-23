using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ApiDeUsuarios.Models;
using Microsoft.AspNetCore.Cors;

namespace ApiDeUsuarios.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string cadenaSQL;

        public UserController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
        }

        [HttpGet]
        [Route("lista")]

        public IActionResult Lista()
        {
            List<User> users = new List<User>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var query = "select  * from users u inner join SEXB s on u.idSex = s.id;";
                    var cmd = new SqlCommand(query, conexion);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Direccion = reader["direccion"].ToString(),
                                Telefono = Convert.ToInt64(reader["telefono"]),
                                IdSex = Convert.ToInt32(reader["idSex"]),
                                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fechaNacimiento")), // Conversión a DateTime
                                SexName = reader["tipo"].ToString()

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = users });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = users });
            }
        }


        [HttpGet]
        [Route("Obtener/{id:int}")]

        public IActionResult Obtener(int id)
        {
            List<User> users = new List<User>();
            User usuario = new User();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var query = "select  * from users u inner join SEXB s on u.idSex = s.id;";
                    var cmd = new SqlCommand(query, conexion);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Direccion = reader["direccion"].ToString(),
                                Telefono = Convert.ToInt64(reader["telefono"]),
                                IdSex = Convert.ToInt32(reader["idSex"]),
                                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fechaNacimiento")), // Conversión a DateTime
                                SexName = reader["tipo"].ToString()

                            });
                        }
                    }
                }

                usuario = users.Where(item => item.Id == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = usuario });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = usuario });
            }
        }


        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] User usuario)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var query = "INSERT INTO USERS (nombre, apellido, fechaNacimiento, correo, direccion, telefono, idSex) " +
                                "VALUES (@nombre,  @apellido, @fechaNacimiento, @correo, @direccion, @telefono, @idSex)";
                    var cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("fechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("idSex", usuario.IdSex);

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario creado" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] User usuario)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var query = "UPDATE USERS SET nombre = @nombre, apellido = @apellido, fechaNacimiento = @fechaNacimiento," +
                        "correo = @correo, direccion = @direccion, telefono = @telefono, idSex = @idSex WHERE id = @id";
                    var cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("fechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("idSex", usuario.IdSex);
                    cmd.Parameters.AddWithValue("id", usuario.Id);

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario Editado" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public IActionResult Eliminar(int id)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var query = "delete USERS where id = @id;";
                    var cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario Eliminado" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }


}
