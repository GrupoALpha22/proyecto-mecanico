
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
- Después de instalar creamos una carpeta con el nombre **Data** y creamos una nueva clase llamada **Connection.cs**. Dentro de esa clase copiamos:

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
- Creamos una nueva clase dentro de la carpeta Data con el nombre **PersonData.cs** y copiamos el siguiente código:

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

                        oPersona.Idpersona = Convert.ToInt32(dr["Idpersona"]);
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
                    cmd.Parameters.AddWithValue("anacimiento", opersona.anacimiento);
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
                    cmd.Parameters.AddWithValue("anacimiento", opersona.anacimiento);
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
            //string val1 = Resquest.QueryString["variable1"].ToString();
            //devuelve la vista
            var opersona = _PersonaDatos.Obtener(Idpersona);
            return View(opersona);
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

            //recibe un objeto y guarda en la base de datos
            var respuesta = _PersonaDatos.Eliminar(oPersona.Idpersona);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}


```

- Sobre el archivo que acabamos de crear *ManteinerController*, damos click derecho y le damos crear vista. En el nombre ponemos **Listar**, chuleamos Usar página de diseño y buscamos el archivo *layout* alojado en *Views/Shared/layout.cshtml* 
- Esto creará una carpeta llamada **Manteiner** dentro de Views y dentro se encontrará el archivo **Listar.cshtml**.
- Dentro de ese archivo vamos a copiar lo siguiente:
```
@model IEnumerable<ALPHA.Models.PersonModel>

@{
    ViewData["Title"] = "Listar";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1>Listar</h1>

<p>
    <a asp-action="Guardar" class="btn btn-primary">Crear persona</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Idpersona)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Identificacion)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Nombre)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Apellido)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.anacimiento)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.Idpersona)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Identificacion)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Nombre)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Apellido)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.anacimiento)
            </td>
            <td>
                    <a asp-action="Editar" asp-route-Idpersona=@item.Idpersona class="btn btn-success">Editar</a>
                    <a asp-action="Eliminar" class="btn btn-danger">Eliminar</a>
          @*          <button type="submit" class="btn btn-warning">
                        @Html.ActionLink("Editar" , "Editar", new {  Idpersona=item.Idpersona })
                    </button>
                    <button type="submit" class="btn btn-danger">
                    @Html.ActionLink("Eliminar", "Eliminar", new { Idpersona=item.Idpersona })
                    </button>*@
            </td>
        </tr>
}
    </tbody>
</table>


```
- Vamos a repetir los pasos anteriores con la vista Guardar, Editar y Eliminar copiando las sigueintes lineas de código respectivamente: 
- Para **Guardar.cshtml**
```
@model ALPHA.Models.PersonModel

@{
    ViewData["Title"] = "Guardar";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1>Crear</h1>

<div class="card">
    <div class="card-header">
        Crear persona
    </div>
    <div class="card-body">

        <form asp-action="Guardar">
            <div class="form-group">
                <label class="form-label">Identificacion</label>
                <input asp-for="Identificacion" type="text" class="form-control" />
                <span asp-validation-for="Identificacion" class="text-danger"></span>
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <input asp-for="Nombre" type="text" class="form-control" />
                <span asp-validation-for="Nombre" class="text-danger"></span>
            </div>
            <div class="mb-3">

                <label class="form-label">Apellido</label>
                <input asp-for="Apellido" type="text" class="form-control" />
                <span asp-validation-for="Apellido" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label class="form-label">anacimiento</label>
                <input asp-for="anacimiento" type="text" class="form-control" />
                <span asp-validation-for="anacimiento" class="text-danger"></span>
            </div>

            <button type="submit" class="btn btn-primary">Guardar</button>
            <a asp-action="Listar" asp-controller="Manteiner" class="btn btn-warning">Volver a la lista</a>
        </form>
    </div>
</div>


```

- Para **Editar.cshtml**

```
@model ALPHA.Models.PersonModel

@{
    ViewData["Title"] = "Editar";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1>Editar</h1>

<div class="card">
    <div class="card-header">
        Editar Propietario
    </div>
    <div class="card-body">

        <form asp-action="Editar" asp-controller="Manteiner" method="post">
            <input asp-for="Idpersona" type="hidden" class="form-control" />
            <div class="mb-3">
                <label class="form-label">Identificacion</label>
                <input asp-for="Identificacion" type="text" class="form-control" />
                <span asp-validation-for="Identificacion" class="text-danger"></span>
            </div>
            <div class="mb-3">
                <h1>@ViewData["Nombre"]</h1>
                
                <label class="form-label">Nombre</label>
                <input asp-for="Nombre" type="text" class="form-control" />
                <span asp-validation-for="Nombre" class="text-danger"></span>
            </div>
            <div class="mb-3">
                
                <label class="form-label">Apellido</label>
                <input asp-for="Apellido" type="text" class="form-control" />
                <span asp-validation-for="Apellido" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label class="form-label">anacimiento</label>
                <input asp-for="anacimiento" type="text" class="form-control" />
                <span asp-validation-for="anacimiento" class="text-danger"></span>
            </div>

            <button type="submit" class="btn btn-primary">Guardar</button>
            <a asp-action="Listar" asp-controller="Manteiner" class="btn btn-warning">Volver a la lista</a>
        </form>
    </div>
</div>

```
- Para **Eliminar.cshtml**

```
@model ALPHA.Models.PersonModel

@{
    ViewData["Title"] = "Eliminar";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1>Eliminar</h1>

<h3>Are you sure you want to delete this?</h3>

<div class="card">
    <div class="card-header">
        Eliminar Propietario
    </div>
    <div class="card-body">

        <form asp-action="Eliminar" asp-controller="Manteiner" method="post">

            <input asp-for="Idpersona" type="hidden" class="form-control">

            <div class="alert alert-danger" role="alert">
                ¿Desea eliminar el contacto : @Html.DisplayTextFor(m => m.Nombre) ?
            </div>



            <button type="submit" class="btn btn-danger">Eliminar</button>
            <a asp-action="Listar" asp-controller="Manteiner" class="btn btn-warning">Voler a la lista</a>
        </form>


    </div>
</div>
```

- Para poder visualizar esta vista debemos cambiar el archivo **_Layout.cshtml** que se encuentra en **Views/Shared** de la siguiente manera :
```
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - ALPHA</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/ALPHA.styles.css" asp-append-version="true" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container-fluid">
                <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">ALPHA</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Manteiner" asp-action="Listar">Personas</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2022 - ALPHA - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </div>
    </footer>
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>


```
- Finalmente cambiamos el archivo **Index.cshtml** que se encuentra en **Views/Home** de la siguiente manera: 
```
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Bienvenidos</h1>
    <hr />
    <div id="demo" class="carousel slide mw-80 pt-2" data-bs-ride="carousel">

        <!-- Indicators/dots -->
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#demo" data-bs-slide-to="0" class="active"></button>
        </div>

        <!-- The slideshow/carousel -->
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="https://c.tenor.com/GVk4jB2u_i8AAAAC/coding.gif" alt="Los Angeles" class="d-block w-100">
            </div>
        </div>

        <!-- Left and right controls/icons -->
    </div>
    <hr />
    <p>Somos grupo ALPHA! Para ver nuestro repositorio haga <a href="https://docs.microsoft.com/aspnet/core">click aquí</a>.</p>
</div>

```
