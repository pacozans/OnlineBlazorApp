using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Model
{
    public class FacturaProducto
    {
        //IdFactura, IdProducto, ValorProducto, CantidadProducto
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public float ValorProducto { get; set; }
        public float CantidadProducto { get; set; }
    }
}
