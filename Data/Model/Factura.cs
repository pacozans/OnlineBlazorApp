using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Model
{
    public class Factura
    {
        //IdFactura, FechaFactura, ClienteFactura
        public int IdFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public int ClienteFactura { get; set; }
    }
}
