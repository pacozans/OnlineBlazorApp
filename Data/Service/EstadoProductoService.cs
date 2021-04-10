using Dapper;
using Microsoft.Data.SqlClient;
using OnlineBlazorApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Service
{
    public class EstadoProductoService : IEstadoProductoService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public EstadoProductoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<EstadoProducto>> GetAllEstadoProducto()
        {
            IEnumerable<EstadoProducto> estadoProducto;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdEstado, NombreEstado FROM EstadoProducto";
                estadoProducto = await conn.QueryAsync<EstadoProducto>(query, commandType: CommandType.Text);
            }
            return estadoProducto;
        }
    }
}
