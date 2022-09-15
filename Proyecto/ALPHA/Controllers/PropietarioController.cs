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
    }
}
