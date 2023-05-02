using System;
using System.Collections.Generic;

namespace DL;

public partial class VentaMedicamento
{
    public int IdVentaMedicamento { get; set; }

    public int? Cantidad { get; set; }

    public int? IdMedicamento { get; set; }

    public int? IdPersona { get; set; }

    public virtual Medicamento? IdMedicamentoNavigation { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }
}
