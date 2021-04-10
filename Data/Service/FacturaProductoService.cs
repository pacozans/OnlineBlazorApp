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
    public class FacturaProductoService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public FacturaProductoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> FacturaProductoInsert(FacturaProducto facturaProducto)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdFactura", facturaProducto.IdFactura, DbType.Int32);
                parameters.Add("IdProducto", facturaProducto.IdProducto, DbType.Int32);
                parameters.Add("ValorProducto", facturaProducto.ValorProducto, DbType.Decimal);
                parameters.Add("CantidadProducto", facturaProducto.CantidadProducto, DbType.Decimal);

                const string query = @"INSERT INTO FacturaProducto (IdFactura, IdProducto, ValorProducto, CantidadProducto) VALUES (@IdFactura, @IdProducto, @ValorProducto, @CantidadProducto)";
                await conn.ExecuteAsync(query, new { facturaProducto.IdFactura, facturaProducto.IdProducto, facturaProducto.ValorProducto, facturaProducto.CantidadProducto}, commandType: CommandType.Text);
            }
            return true;
        }
    }
}
