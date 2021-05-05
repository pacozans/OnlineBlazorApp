using OnlineBlazorApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBlazorApp.Data.Service
{
    public interface IFacturaService
    {
        Task<bool> FacturaInsert(Factura factura);
        Task<Factura> FacturaSelect(int id);
        Task<int> NumeroFactura();
        Task<IEnumerable<Factura>> ListaFacturas();
    }
}