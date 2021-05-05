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
    public class FacturaProductoService : IFacturaProductoService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public FacturaProductoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FacturaProductoService()
        {
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

                const string query = @"INSERT INTO Factura_Producto (IdFactura, IdProducto, ValorProducto, CantidadProducto) VALUES (@IdFactura, @IdProducto, @ValorProducto, @CantidadProducto)";
                await conn.ExecuteAsync(query, new { facturaProducto.IdFactura, facturaProducto.IdProducto, facturaProducto.ValorProducto, facturaProducto.CantidadProducto }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<bool> FacturaProductoUpdate(FacturaProducto facturaProducto)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdFactura", facturaProducto.IdFactura, DbType.Int32);
                parameters.Add("IdProducto", facturaProducto.IdProducto, DbType.Int32);
                parameters.Add("ValorProducto", facturaProducto.ValorProducto, DbType.Decimal);
                parameters.Add("CantidadProducto", facturaProducto.CantidadProducto, DbType.Decimal);

                const string query = @"UPDATE Factura_Producto 
                    SET ValorProducto = @ValorProducto, 
                        CantidadProducto = @CantidadProducto
                    WHERE IdFactura=@IdFactura AND IdProducto = @IdProducto";
                await conn.ExecuteAsync(query, new { facturaProducto.IdFactura, facturaProducto.IdProducto, facturaProducto.ValorProducto, facturaProducto.CantidadProducto }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<bool> FacturaProductoDelete(FacturaProducto facturaProducto)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdFactura", facturaProducto.IdFactura, DbType.Int32);
                parameters.Add("IdProducto", facturaProducto.IdProducto, DbType.Int32);

                var query = @"DELETE FROM Factura_Producto
                            WHERE IdFactura=@IdFactura AND IdProducto=@IdProducto";
                await conn.ExecuteAsync(query.ToString(), new { facturaProducto.IdFactura, facturaProducto.IdProducto }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<IEnumerable<DetalleFactura>> GetDetalleFactura(int id)
        {
            IEnumerable<DetalleFactura> detalle;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT a.IdFactura, a.IdProducto, b.NombreProducto, a.ValorProducto, a.CantidadProducto 
                                        FROM Factura_Producto a, Producto b 
                                        WHERE a.IdProducto=b.IdProducto AND a.IdFactura=@IdFactura";
                detalle = await conn.QueryAsync<DetalleFactura>(query, new { IdFactura = id }, commandType: CommandType.Text);
            }

            return detalle;
        }
    }
}
