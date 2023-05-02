using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaMedicamento
    {
        public int IdVentaMedicamento { get; set; }
        public int Cantidad { get; set; }

        public decimal Total { get; set; }
        public ML.Medicamento Medicamento { get; set; }
        public ML.Persona Persona { get; set; }

        public List<object> Ventas { get; set; }
    }
}
