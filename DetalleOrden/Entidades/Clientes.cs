using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DetalleOrden.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public String Nombre { get; set; }
        public String Cedula { get; set; }
        public String Telefono { get; set; }

        [ForeignKey("ClienteId")]
        public virtual List<Ordenes> Orden { get; set; }

        public Clientes()
        {
            ClienteId = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Telefono = string.Empty;
            Orden = new List<Ordenes>();
        }
    }
}
