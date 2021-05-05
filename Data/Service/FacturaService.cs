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
    public class FacturaService : IFacturaService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public FacturaService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FacturaService()
        {
        }

        public async Task<bool> FacturaInsert(Factura factura)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdFactura", factura.IdFactura, DbType.Int32);
                parameters.Add("FechaFactura", factura.FechaFactura, DbType.DateTime);
                parameters.Add("ClienteFactura", factura.ClienteFactura, DbType.Int32);

                const string query = @"INSERT INTO Factura (IdFactura, FechaFactura, ClienteFactura) VALUES (@IdFactura, @FechaFactura, @ClienteFactura)";
                await conn.ExecuteAsync(query, new { factura.IdFactura, factura.FechaFactura, factura.ClienteFactura }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<Factura> FacturaSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdFactura, FechaFactura, ClienteFactura
                            FROM Factura
                            WHERE IdFactura = @IdFactura";
                return await conn.QueryFirstOrDefaultAsync<Factura>(query.ToString(), new { IdFactura = id }, commandType: CommandType.Text);
            }
        }

        public async Task<int> NumeroFactura()
        {
            var max = 0;
            int cantidad=0;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = @"SELECT MAX(IdFactura) Numero FROM Factura";
                max = await conn.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
               
            }

            cantidad = Convert.ToInt32(max.ToString());
            cantidad++;
            return cantidad;
        }

        public async Task<IEnumerable<Factura>> ListaFacturas()
        {
            IEnumerable<Factura> facturas;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                string query = "SELECT IdFactura, FechaFactura, ClienteFactura FROM Factura";
                facturas = await conn.QueryAsync<Factura>(query, commandType: CommandType.Text);
            }

            return facturas;
        }

    }
}
