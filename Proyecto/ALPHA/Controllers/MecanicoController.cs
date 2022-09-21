using ALPHA.Data;
using ALPHA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALPHA.Controllers
{
    public class PropietarioController : Controller
    {
        PropietarioData _PropietarioDatos = new PropietarioData();
        public IActionResult Listar()
        {
            //listar contactos
            var oLista = _PropietarioDatos.Listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            //devuelve la vista

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(PropietarioModel oPropietario)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return RedirectToAction("Listar");
            //recibe un objeto y guarda en la base de datos
            var resouesta = _PropietarioDatos.Guardar(oPropietario);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(int Idpersona)
        {
            //devuelve la vista
            var oPropietario = _PropietarioDatos.Obtener(Idpersona);
            return View(oPropietario);
        }

        [HttpPost]
        public IActionResult Editar(PropietarioModel oPropietario)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return View();
            //recibe un objeto y guarda en la base de datos
            var resouesta = _PropietarioDatos.Editar(oPropietario);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int Idpersona)
        {
            //devuelve la vista
            var oPropietario = _PropietarioDatos.Obtener(Idpersona);
            return View(oPropietario);
        }

        [HttpPost]
        public IActionResult Eliminar(PropietarioModel oPropietario)
        {

            //recibe un objeto y guarda en la base de datos
            var respuesta = _PropietarioDatos.Eliminar(oPropietario.Idpersona);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}
