using OnlineBlazorApp.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Service
{
    public interface IFacturaProductoService
    {
        Task<bool> FacturaProductoDelete(FacturaProducto facturaProducto);
        Task<bool> FacturaProductoInsert(FacturaProducto facturaProducto);
        Task<bool> FacturaProductoUpdate(FacturaProducto facturaProducto);
        Task<IEnumerable<DetalleFactura>> GetDetalleFactura(int id);
    }
}