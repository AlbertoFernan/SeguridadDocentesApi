using System;
using System.Collections.Generic;

namespace SeguridadDocentesApi.Models;

public partial class Usuarios
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string NumEmpleado { get; set; } = null!;

    public string? Correo { get; set; }

    public int IdDepartamento { get; set; }

    public ulong Eliminado { get; set; }

    public virtual Departamentos IdDepartamentoNavigation { get; set; } = null!;
}
