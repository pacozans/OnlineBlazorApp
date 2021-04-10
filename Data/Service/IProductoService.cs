using OnlineBlazorApp.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Service
{
    public interface IProductoService
    {
        Task<bool> ProductoDelete(int id);
        Task<bool> ProductoInsert(Producto producto);
        Task<Producto> ProductoSelect(int id);
        Task<bool> ProductoUpdate(Producto producto);
        Task<IEnumerable<Producto>> GetProductosActivos(int estado);
    }
}