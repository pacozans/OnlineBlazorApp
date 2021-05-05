using Microsoft.AspNetCore.Mvc;
using OnlineBlazorApp.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OnlineBlazorApp.Data.PDF
{
    public interface IFacturaPDF
    {
        FileResult descargarPDF();
        Task GeneraFactura(Factura factura, Cliente cliente, IEnumerable<DetalleFactura> detalle);
    }
}