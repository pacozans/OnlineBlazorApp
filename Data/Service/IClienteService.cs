using OnlineBlazorApp.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Service
{
    public interface IClienteService
    {
        Task<bool> ClienteInsert(Cliente cliente);

        Task<IEnumerable<Cliente>> GetAllClientes();

        //Task<Cliente> GetCliente(int id);
        //Task<bool> DeleteCliente(int id);

    }
}