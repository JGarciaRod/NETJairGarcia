using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Examen
    {
        public int IdExamen { get; set; }
        public string Descripcion { get; set; }

        public List<object> Examenes { get; set; }
    }
}
