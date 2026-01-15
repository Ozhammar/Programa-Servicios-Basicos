namespace Control_de_Facturas.Models
{
    public class Factura
    {
        public Factura(
            string pEmpresa = "",
            string pNumeroCliente = "",
            string pNumeroFactura = "",
            DateTime? pFechaEmision = null,
            DateTime? pFechaVencimiento = null,
            decimal pImportePrimerVencimiento = 0,
            decimal pImporteSaldoAnterior = 0,
            decimal pImporteAbonable = 0,
            long pCUIT = 0,
            string pObjetoGasto = "",
            string pCodigoCatalogo = "",
            string pTipoCodigoAutorizacion = "CESP",
            string pCodigoAutorizacion = "",
            DateTime? pVencimientoCodigoAutorizacion = null,
            string pArchivo = "",
            string pTipoServicio = "",
            string pTarifa = "")
        {
            Empresa = pEmpresa;
            NumeroCliente = pNumeroCliente;
            NumeroFactura = pNumeroFactura;
            FechaEmision = pFechaEmision ?? DateTime.MinValue;
            FechaVencimiento = pFechaVencimiento ?? DateTime.MinValue;
            ImportePrimerVencimiento = pImportePrimerVencimiento;
            ImporteSaldoAnterior = pImporteSaldoAnterior;
            ImporteAbonable = pImporteAbonable;
            CUIT = pCUIT;
            ObjetoGasto = pObjetoGasto;
            CodigoCatalogo = pCodigoCatalogo;
            TipoCodigoAutorizacion = pTipoCodigoAutorizacion;
            CodigoAutorizacion = pCodigoAutorizacion;
            VencimientoCodigoAutorizacion = pVencimientoCodigoAutorizacion ?? DateTime.MinValue;
            Archivo = pArchivo;
            TipoServicio = pTipoServicio;
            Tarifa = pTarifa;
        }
        public string Empresa { get; set; }
        public string NumeroCliente { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal ImportePrimerVencimiento { get; set; }
        public decimal ImporteSaldoAnterior { get; set; }
        public decimal ImporteAbonable { get; set; }
        public long CUIT { get; set; }
        public string ObjetoGasto { get; set; }
        public string CodigoCatalogo { get; set; }
        public string TipoCodigoAutorizacion { get; set; }
        public string CodigoAutorizacion { get; set; }
        public DateTime VencimientoCodigoAutorizacion { get; set; }
        public string Archivo { get; set; }
        public string TipoServicio { get; set; }
        public string Tarifa { get; set; } // Solo para Edesur

        // Método de validación
        public bool EsValida()
        {
            return !string.IsNullOrEmpty(NumeroFactura)
                && ImportePrimerVencimiento > 0
                && !string.IsNullOrEmpty(NumeroCliente)
                && FechaVencimiento != DateTime.MinValue; // Validar que tenga fecha
        }

        // Método adicional útil para calcular importe abonable
        public decimal CalcularImporteAbonable()
        {
            decimal ImporteAbonable = ImportePrimerVencimiento - ImporteSaldoAnterior;
            if (ImporteAbonable < 0)
            {
                ImporteAbonable = 0;
            }

            return ImporteAbonable;

        }
    }
}
