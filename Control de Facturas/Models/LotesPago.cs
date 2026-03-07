
namespace Control_de_Facturas.Models
{
    internal class LotesPago
    {

        public LotesPago(string pObservacion = "", decimal pImporte = 0, Factura pPrimerFactura = null) 
        {
            Observacion = pObservacion;
            Importe = pImporte;
            PrimerFactura = pPrimerFactura;
        }

        public string Observacion { get; set; }
        public decimal Importe { get; set; }
        public Factura PrimerFactura { get; set; }
    }
}
