
# PROYECTO

En la siguiente documentación se podrá encontrar paso a paso la información sobre el proyecto FULL STACK con .net, así mismo como sus modificaciones.



## CREAR PROYECTO

En primer lugar, teniendo en cuenta que el lenguaje de desarrollo es .net de microsoft, usaremos VISUAL STUDIO debido a que tiene todo el develpment kit para facilitarnos el desarrollo. 

Se deben seguir los siguientes pasos. 

- Abrir VISUAL STUDIO
- Crear un nuevo proyecto
- Elegir la opción ASP.NET Core aplicación web(controlador de vista de modelo)
- Nombrar el proyecto ALPHA y darle una ubicación al proyecto. 
- CREAR y esperar pacientemente.

## EMPEZAR A PROGRAMAR
- Lo primero que se debe hacer es ir a la carpeta de modelos, darle click derecho y crear nueva clase. A esta clase la llamamos PersonModel.cs y copiamos el siguiente código: 
```
using System;
using System.ComponentModel.DataAnnotations;
namespace ALPHA.Models
{
    public class PersonModel
    {

        public int Idpersona { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? anacimiento { get; set; }

    }
}

```

- Vamos al archivo **appsettings.json**  y se digita el siguiente código: 

```
{
  "ConnectionStrings": {
    "CadenaSQL": "Data Source=localhost; Initial Catalog=ALPHA;Integrated Security=true"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
- Damos click derecho sobre el proyecto y le damos click sobre "ADMINISTRAR PAQUETES NUGET". Buscamos **system.data.sqlClient** e instalamos el paquete. 
- Después de instalar creamos una carpeta con el nombre **Data** y creamos una nueva clase llamada **Connection**. Dentro de esa clase copiamos:

```
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ALPHA.Data
{
    public class Connection

    {
        private string cadenaSQL = string.Empty;
        public Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }

        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}

```
- Creamos una nueva clase dentro de la carpeta Data con el nombre **PersonData** y copiamos el siguiente código:

```
using System.Data.SqlClient;
using ALPHA.Models;
using System.Data;
using System.Collections.Generic;
using System;

namespace ALPHA.Data
{
    public class PersonData
    {

        public List<PersonModel> Listar()
        {
            var olista = new List<PersonModel>();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olista.Add(new PersonModel()
                        {
                            Idpersona = Convert.ToInt32(dr["Idpersona"]),
                            Identificacion = dr["Identificacion"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            anacimiento = dr["anacimiento"].ToString(),
                        });
                    }
                }
            }
            return olista;

        }

        public PersonModel Obtener(int Idpersona)
        {
            var oPersona = new PersonModel();
            var cn = new Connection();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("Idpersona", Idpersona);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oPersona.Idpersona = Convert.ToInt32(dr["Idperona"]);
                        oPersona.Identificacion = dr["Identificacion"].ToString();
                        oPersona.Nombre = dr["Nombre"].ToString();
                        oPersona.Apellido = dr["Apellido"].ToString();
                        oPersona.anacimiento = dr["anacimiento"].ToString();

                    }
                }
            }
            return oPersona;

        }

        public bool Guardar(PersonModel opersona)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("Identificacion", opersona.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", opersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", opersona.Apellido);
                    cmd.Parameters.AddWithValue("anacimiebto", opersona.anacimiento);

                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

            return rpta;
        }
        public bool Editar(PersonModel opersona)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("Idpersona", opersona.Idpersona);
                    cmd.Parameters.AddWithValue("Identificacion", opersona.Identificacion);
                    cmd.Parameters.AddWithValue("Nombre", opersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", opersona.Apellido);
                    cmd.Parameters.AddWithValue("anacimiebto", opersona.anacimiento);

                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

            return rpta;
        }
        public bool Eliminar(int Idpersona)
        {
            bool rpta;
            try
            {
                var cn = new Connection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("Idpersona", Idpersona);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

            return rpta;
        }

    }
}


```
- En la carpeta *Controllers* le damos click derecho y seleccionamos Nuevo scaffolding. seleccionamos Controlador de MVC en blanco, lo nombramos **ManteinerController.cs** y copiamos el siguiente código: 

```
using ALPHA.Data;
using ALPHA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALPHA.Controllers
{
    public class ManteinerController : Controller
    {

        PersonData _PersonaDatos = new PersonData();
        public IActionResult Listar()
        {
            //listar contactos
            var oLista = _PersonaDatos.Listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            //devuelve la vista

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(PersonModel oPersona)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return View();
            //recibe un objeto y guarda en la base de datos
            var resouesta = _PersonaDatos.Guardar(oPersona);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(int Idpersona)
        {
            //devuelve la vista
            var opersona = _PersonaDatos.Obtener(Idpersona);
            return View();
        }

        [HttpPost]
        public IActionResult Editar(PersonModel oPersona)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return View();
            //recibe un objeto y guarda en la base de datos
            var resouesta = _PersonaDatos.Editar(oPersona);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int Idpersona)
        {
            //devuelve la vista
            var opersona = _PersonaDatos.Obtener(Idpersona);
            return View();
        }

        [HttpPost]
        public IActionResult Eliminar(PersonModel oPersona)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return View();
            //recibe un objeto y guarda en la base de datos
            var resouesta = _PersonaDatos.Editar(oPersona);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}

```

- Sobre el archivo que acabamos de crear *ManteinerController*, damos click derecho y le damos crear vista. En el nombre ponemos **Listar**, chuleamos Usar página de diseño y buscamos el archivo *layout* alojado en *Views/Shared/layout.cshtml* 
Esto creará una carpeta llamada **Manteiner* dentro de Views y dentro se encontrará el archivo *Listar.cshtml*. Dentro de ese archivo copiamos: 

```

```