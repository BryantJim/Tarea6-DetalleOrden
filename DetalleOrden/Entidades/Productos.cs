using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DetalleOrden.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public String NombreProducto { get; set; }
        public decimal Inventario { get; set; }
        public decimal Precio { get; set; }

        [ForeignKey("ProductoId")]
        public virtual List<OrdenDetalle> OrdenDetalle { get; set; }

        public Productos()
        {
            ProductoId = 0;
            NombreProducto = string.Empty;
            Inventario = 0;
            Precio = 0;
        }
    }
}
