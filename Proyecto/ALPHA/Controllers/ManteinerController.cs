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
