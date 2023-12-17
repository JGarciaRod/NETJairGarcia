using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_Web.Controllers
{
    [RoutePrefix("api/EvalXPersona")]
    public class EvalXPersonaController : ApiController
    {
        [HttpGet]
        [Route("GetBy/{Correo?}")]
        public IHttpActionResult GetByCorreo(string Correo)
        {
            BL.Result result = BL.EvalXPersona.GetByCorreo(Correo);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
