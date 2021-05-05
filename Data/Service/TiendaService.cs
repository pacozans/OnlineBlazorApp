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
    public class TiendaService : ITiendaService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public TiendaService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> TiendaInsert(Tienda tienda)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdTienda", tienda.IdTienda, DbType.Int32);
                parameters.Add("NombreTienda", tienda.NombreTienda, DbType.String);
                parameters.Add("FacebookTienda", tienda.FacebookTienda, DbType.String);
                parameters.Add("BannerTienda", tienda.BannerTienda, DbType.String);
                parameters.Add("UsuarioRegistro", tienda.BannerTienda, DbType.String);

                const string query = @"INSERT INTO Tienda (IdTienda, NombreTienda, FacebookTienda,BannerTienda,UsuarioRegistro ) VALUES (@IdTienda, @NombreTienda, @FacebookTienda, @BannerTienda, @UsuarioRegistro)";
                await conn.ExecuteAsync(query, new { tienda.IdTienda, tienda.NombreTienda, tienda.FacebookTienda, tienda.BannerTienda, tienda.UsuarioRegistro }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<Tienda> TiendaSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdTienda, NombreTienda, FacebookTienda, BannerTienda,UsuarioRegistro
                            FROM Tienda
                            WHERE IdTienda = @IdTienda";
                return await conn.QueryFirstOrDefaultAsync<Tienda>(query.ToString(), new { IdTienda = id }, commandType: CommandType.Text);
            }
        }

        public async Task<bool> TiendaUpdate(Tienda tienda)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdTienda", tienda.IdTienda, DbType.Int32);
                parameters.Add("NombreTienda", tienda.NombreTienda, DbType.String);
                parameters.Add("FacebookTienda", tienda.FacebookTienda, DbType.String);
                parameters.Add("BannerTienda", tienda.BannerTienda, DbType.String);
                parameters.Add("UsuarioRegistro", tienda.BannerTienda, DbType.String);

                const string query = @"UPDATE Tienda 
                    SET NombreTienda = @NombreTienda, 
                        FacebookTienda = @FacebookTienda,
                        BannerTienda = @BannerTienda,
                        UsuarioRegistro = @UsuarioRegistro
                    WHERE IdTienda = @IdTienda";
                await conn.ExecuteAsync(query, new { tienda.IdTienda, tienda.NombreTienda, tienda.FacebookTienda, tienda.BannerTienda, tienda.UsuarioRegistro }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<bool> TiendaDelete(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"DELETE FROM Tienda
                            WHERE IdTienda = @IdTienda";
                await conn.ExecuteAsync(query.ToString(), new { IdTienda = id }, commandType: CommandType.Text);
            }

            return true;
        }


        public async Task<IEnumerable<Tienda>> GetAllTiendas()
        {
            IEnumerable<Tienda> tiendas;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdTienda, NombreTienda, BannerTienda FROM Tienda";
                tiendas = await conn.QueryAsync<Tienda>(query, commandType: CommandType.Text);
            }

            return tiendas;
        }

        public async Task<IEnumerable<Tienda>> GetTiendas(string nombreTienda, string bannerTienda)
        {
            IEnumerable<Tienda> tiendas;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (nombreTienda.Length > 0 && bannerTienda.Length > 0)
                {
                    string query = @"SELECT IdTienda, NombreTienda, FacebookTienda,BannerTienda,UsuarioRegistro 
                                            FROM Tienda 
                                            WHERE NombreTienda LIKE '%" + nombreTienda + "%' AND BannerTienda LIKE '%" + bannerTienda + "%'";
                    tiendas = await conn.QueryAsync<Tienda>(query, commandType: CommandType.Text);
                }
                else
                {
                    if (nombreTienda.Length > 0)
                    {
                        string query = @"SELECT IdTienda, NombreTienda, BannerTienda FROM Tienda WHERE NombreTienda LIKE '%" + nombreTienda + "%'";
                        tiendas = await conn.QueryAsync<Tienda>(query, commandType: CommandType.Text);
                    }
                    else
                    {
                        string query = @"SELECT IdTienda, NombreTienda, FacebookTienda,BannerTienda,UsuarioRegistro 
                                            FROM Tienda 
                                            WHERE BannerTienda LIKE '%" + bannerTienda + "%'";
                        tiendas = await conn.QueryAsync<Tienda>(query, commandType: CommandType.Text);
                    }
                }
            }

            return tiendas;
        }



    }
}

