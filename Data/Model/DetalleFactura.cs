using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Model
{
    public class DetalleFactura
    {
        //IdFactura, IdProducto, ValorProducto, CantidadProducto
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public float ValorProducto { get; set; }
        public float CantidadProducto { get; set; }

        public FacturaProducto facturaProducto;

        public FacturaProducto GetFacturaProducto() 
        {
            facturaProducto = new FacturaProducto();
            facturaProducto.IdFactura = IdFactura;
            facturaProducto.IdProducto = IdProducto;
            facturaProducto.ValorProducto = ValorProducto;
            facturaProducto.CantidadProducto = CantidadProducto;
            return facturaProducto;
        }
    }
}
