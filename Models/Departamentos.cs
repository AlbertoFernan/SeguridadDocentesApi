using System;
using System.Collections.Generic;

namespace SeguridadDocentesApi.Models;

public partial class Departamentos
{
    public int Id { get; set; }

    public string? Clave { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Contraseña { get; set; }

    public int? IdSuperior { get; set; }

    public string? Correo { get; set; }

    public ulong Eliminado { get; set; }

    public virtual Departamentos? IdSuperiorNavigation { get; set; }

    public virtual ICollection<Departamentos> InverseIdSuperiorNavigation { get; set; } = new List<Departamentos>();

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
