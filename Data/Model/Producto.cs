using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Model
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public float PrecioProducto { get; set; }
        public string ImagenProducto { get; set; }
        public int IdEstadoProducto { get; set; }
    }
}
