using OnlineBlazorApp.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Service
{
    public interface ITiendaService
    {
        Task<bool> TiendaInsert(Tienda tienda);
        Task<IEnumerable<Tienda>> GetAllTiendas();
        Task<IEnumerable<Tienda>> GetTiendas(string nombreTienda, string bannerTienda);
        Task<Tienda> TiendaSelect(int id);
        Task<bool> TiendaUpdate(Tienda tienda);
        Task<bool> TiendaDelete(int id);
    }
}