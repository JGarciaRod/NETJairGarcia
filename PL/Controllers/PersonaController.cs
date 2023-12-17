using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        [HttpGet]
        public ActionResult GetAll()
        {
            BL.Persona persona = new BL.Persona();
            BL.Result result = BL.Persona.GetAll();
            if (result.Correct)
            {
                persona.Personas = result.Objects;
            }
            else
            {
                persona.Personas = null;
            }

            return View(persona);
        }
    }
}