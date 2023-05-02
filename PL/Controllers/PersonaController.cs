using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PersonaController : Controller
    {
        [HttpGet]
        public ActionResult LoginPersona()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPersona(ML.Persona persona)
        {
            ML.Result result = BL.Persona.GetByUserName(persona);

            if (result.Correct)
            {
                ML.Persona personaUn = (ML.Persona)result.Object;
                if (persona.Password == personaUn.Password)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Message = "Credenciales Ingresadas Invalidas";
                    return PartialView("Modal");
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "La persona no se encuantra dada de alta en la base de datos";
                return PartialView("Modal");
            }
        }
    }
}
