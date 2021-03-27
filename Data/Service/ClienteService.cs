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
    public class ClienteService : IClienteService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public ClienteService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> ClienteInsert(Cliente cliente)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCliente", cliente.IdCliente, DbType.Int32);
                parameters.Add("NombreCliente", cliente.NombreCliente, DbType.String);
                parameters.Add("ApellidoCliente", cliente.ApellidoCliente, DbType.String);
                parameters.Add("EmailCliente", cliente.EmailCliente, DbType.String);

                const string query = @"INSERT INTO Cliente (IdCliente, NombreCliente, ApellidoCliente, EmailCliente) VALUES (@IdCliente, @NombreCliente, @ApellidoCliente, @EmailCliente)";
                await conn.ExecuteAsync(query, new { cliente.IdCliente, cliente.NombreCliente, cliente.ApellidoCliente, cliente.EmailCliente }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            IEnumerable<Cliente> clientes;
            
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdCliente, NombreCliente, ApellidoCliente FROM Cliente";
                clientes = await conn.QueryAsync<Cliente>(query, commandType: CommandType.Text);
            }

            return clientes;
        }
    }
}
