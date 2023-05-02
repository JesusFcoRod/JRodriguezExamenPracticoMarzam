using System;
using System.Collections.Generic;

namespace DL;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public virtual ICollection<VentaMedicamento> VentaMedicamentos { get; set; } = new List<VentaMedicamento>();
}
