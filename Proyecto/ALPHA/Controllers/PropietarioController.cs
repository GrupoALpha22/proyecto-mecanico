using ALPHA.Data;
using ALPHA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALPHA.Controllers
{
    public class MecanicoController : Controller
    {
        MecanicoData _MecanicoDatos = new MecanicoData();
        public IActionResult Listar()
        {
            //listar contactos
            var oLista = _MecanicoDatos.Listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            //devuelve la vista

            return View();
        }

        [HttpPost]
        public IActionResult Guardar(MecanicoModel oMecanico)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return RedirectToAction("Listar");
            //recibe un objeto y guarda en la base de datos
            var resouesta = _MecanicoDatos.Guardar(oMecanico);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(int Idpersona)
        {
            //devuelve la vista
            var oMecanico = _MecanicoDatos.Obtener(Idpersona);
            return View(oMecanico);
        }

        [HttpPost]
        public IActionResult Editar(MecanicoModel oMecanico)
        {
            //validacion de campos
            if (!ModelState.IsValid)
                return View();
            //recibe un objeto y guarda en la base de datos
            var resouesta = _MecanicoDatos.Editar(oMecanico);
            if (resouesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int Idpersona)
        {
            //devuelve la vista
            var oMecanico = _MecanicoDatos.Obtener(Idpersona);
            return View(oMecanico);
        }

        [HttpPost]
        public IActionResult Eliminar(MecanicoModel oMecanico)
        {

            //recibe un objeto y guarda en la base de datos
            var respuesta = _MecanicoDatos.Eliminar(oMecanico.Idpersona);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

    }
}
