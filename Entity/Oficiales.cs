﻿using System;

namespace GuanajuatoAdminUsuarios.Entity;

public partial class Oficiales
{
    public int IdOficial { get; set; }

    public string Rango { get; set; }

    public string Nombre { get; set; }

    public string ApellidoPaterno { get; set; }

    public string ApellidoMaterno { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int? ActualizadoPor { get; set; }

    public int? Estatus { get; set; }

    public int IdDelegacion { get; set; }

}
