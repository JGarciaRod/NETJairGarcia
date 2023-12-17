using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EvalXPersonaController : Controller
    {
        // GET: EvalXPersona
        public ActionResult GetExamenAsignado(int IdPersona)
        {
            BL.EvalXPersona evalXPersona = new BL.EvalXPersona();
            evalXPersona.Persona = new BL.Persona();
            evalXPersona.Examen = new BL.Examen();

            BL.Result result = BL.EvalXPersona.GetExamenAsignado(IdPersona);
            if (result.Correct)
            {
                evalXPersona.EvalxPersonas = result.Objects;
                evalXPersona.Persona.IdPersona = IdPersona;
            }
            else
            {
                evalXPersona.EvalxPersonas = null;
            }

            return View(evalXPersona);
        }

        public ActionResult GetExamenNoAsignado(int IdPersona)
        {
            BL.EvalXPersona evalXPersona = new BL.EvalXPersona();
            evalXPersona.Persona = new BL.Persona();
            evalXPersona.Examen = new BL.Examen();

            BL.Result result = BL.EvalXPersona.GetExamenNoAsignado(IdPersona);

            if (result.Correct)
            {
                evalXPersona.Examen.Examenes = result.Objects;
                evalXPersona.Persona.IdPersona = IdPersona;
            }
            else
            {
                evalXPersona.Examen.Examenes = null;
            }
            return View(evalXPersona);
        }

        public ActionResult Add(BL.Persona persona, int[] IdExamen)
        {
            foreach (var item in IdExamen)
            {
                BL.EvalXPersona.Add(persona.IdPersona, item);
            }
            return PartialView("Modal");
        }

        public ActionResult Delete(int IdEvalxPersona)
        {
            BL.Result result = BL.EvalXPersona.Delete(IdEvalxPersona);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamnete";
            }
            else
            {
                ViewBag.Error = "Error al eliminar";
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult GetCorreo(string Correo)
        {
            BL.EvalXPersona evalXPersona = new BL.EvalXPersona();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57115/api/EvalXPersona/");

                var responseTask = client.GetAsync("GetBy?Correo=" + Correo);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BL.Result>();
                    readTask.Wait();

                    BL.EvalXPersona readItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.EvalXPersona>(readTask.Result.Object.ToString());
                    evalXPersona = readItemList;
                }
            }
            return View(evalXPersona);
        }
    }
}