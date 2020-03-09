using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DetalleOrden.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public String NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoTotal { get; set; }

        [ForeignKey("OrdenId")]
        public virtual List<OrdenDetalle> ordenDetalle { get; set; }

        public Ordenes()
        {
            OrdenId = 0;
            ClienteId = 0;
            NombreCliente = string.Empty;
            Fecha = DateTime.Now;
            MontoTotal = 0;
            ordenDetalle = new List<OrdenDetalle>();
        }

    }
}
