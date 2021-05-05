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
    public class ProductoService : IProductoService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public ProductoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> ProductoInsert(Producto producto)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdProducto", producto.IdProducto, DbType.Int32);
                parameters.Add("NombreProducto", producto.NombreProducto, DbType.String);
                parameters.Add("PrecioProducto", producto.PrecioProducto, DbType.Decimal);
                parameters.Add("ImagenProducto", producto.ImagenProducto, DbType.String);
                parameters.Add("IdEstadoProducto", producto.IdEstadoProducto, DbType.Int16);

                const string query = @"INSERT INTO Producto (IdProducto, NombreProducto, PrecioProducto, ImagenProducto, IdEstadoProducto) VALUES (@IdProducto, @NombreProducto, @PrecioProducto,@ImagenProducto, @IdEstadoProducto)";
                await conn.ExecuteAsync(query, new { producto.IdProducto, producto.NombreProducto, producto.PrecioProducto, producto.ImagenProducto, producto.IdEstadoProducto }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<Producto> ProductoSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdProducto, NombreProducto, PrecioProducto, ImagenProducto, IdEstadoProducto
                            FROM Producto
                            WHERE IdProducto = @IdProducto";
                return await conn.QueryFirstOrDefaultAsync<Producto>(query.ToString(), new { IdProducto = id }, commandType: CommandType.Text);
            }
        }

        public async Task<bool> ProductoUpdate(Producto producto)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdProducto", producto.IdProducto, DbType.Int32);
                parameters.Add("NombreProducto", producto.NombreProducto, DbType.String);
                parameters.Add("PrecioProducto", producto.PrecioProducto, DbType.Decimal);
                parameters.Add("ImagenProducto", producto.ImagenProducto, DbType.String);
                parameters.Add("IdEstadoProducto", producto.IdEstadoProducto, DbType.Int16);

                const string query = @"UPDATE Producto 
                    SET NombreProducto = @NombreProducto, 
                        PrecioProducto = @PrecioProducto,
                        ImagenProducto = @ImagenProducto,
                        IdEstadoProducto = @IdEstadoProducto
                    WHERE IdTienda = @IdTienda";
                await conn.ExecuteAsync(query, new { producto.IdProducto, producto.NombreProducto, producto.PrecioProducto, producto.ImagenProducto, producto.IdEstadoProducto }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<bool> ProductoDelete(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"DELETE FROM Producto
                            WHERE IdProducto = @IdProducto";
                await conn.ExecuteAsync(query.ToString(), new { IdProducto = id }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<IEnumerable<Producto>> GetProductosActivos(int estado)
        {
            IEnumerable<Producto> productos;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT IdProducto, NombreProducto, PrecioProducto, ImagenProducto FROM Producto WHERE IdEstadoProducto=@IdEstadoProducto";
                productos = await conn.QueryAsync<Producto>(query, new { IdEstadoProducto = estado }, commandType: CommandType.Text);
            }

            return productos;
        }

    }
}
