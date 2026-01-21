using NPOI.SS.Formula.Functions;

namespace Control_de_Facturas.Models
{
    public class Factura
    {
        public Factura(
            string pEmpresa = "",
            string pNumeroCliente = "",
            string pTipoFactura = "",
            string pPuntoVenta = "",
            string pNumeroFactura = "",
            DateTime? pFechaEmision = null,
            DateTime? pFechaVencimiento = null,
            string pPeriodo = "",
            decimal pImportePrimerVencimiento = 0,
            decimal pImporteSaldoAnterior = 0,
            decimal pImporteAbonable = 0,
            long pCUIT = 0,
            string pObjetoGasto = "",
            string pCodigoCatalogo = "",
            string pTipoCodigoAutorizacion = "CESP",
            string pCodigoAutorizacion = "",
            DateTime? pVencimientoCodigoAutorizacion = null,
            //string pArchivo = "",
            string pTipoServicio = "",
            string pTarifa = "")
        {
            Empresa = pEmpresa;
            NumeroCliente = pNumeroCliente;
            TipoFactura = pTipoFactura;
            PuntoVenta = pPuntoVenta;
            NumeroFactura = pNumeroFactura;
            FechaEmision = pFechaEmision ?? DateTime.MinValue;
            FechaVencimiento = pFechaVencimiento ?? DateTime.MinValue;
            Periodo = pPeriodo;
            ImportePrimerVencimiento = pImportePrimerVencimiento;
            ImporteSaldoAnterior = pImporteSaldoAnterior;
            ImporteAbonable = pImporteAbonable;
            CUIT = pCUIT;
            ObjetoGasto = pObjetoGasto;
            CodigoCatalogo = pCodigoCatalogo;
            TipoCodigoAutorizacion = pTipoCodigoAutorizacion;
            CodigoAutorizacion = pCodigoAutorizacion;
            VencimientoCodigoAutorizacion = pVencimientoCodigoAutorizacion ?? DateTime.MinValue;
            //Archivo = pArchivo;
            TipoServicio = pTipoServicio;
            Tarifa = pTarifa;
        }
        public string Empresa { get; set; }
        public string NumeroCliente { get; set; }
        public string TipoFactura { get; set; }
        public string PuntoVenta { get; set; }

        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Periodo { get; set; }
        public decimal ImportePrimerVencimiento { get; set; }
        public decimal ImporteSaldoAnterior { get; set; }
        public decimal ImporteAbonable { get; set; }
        public long CUIT { get; set; }
        public string ObjetoGasto { get; set; }
        public string CodigoCatalogo { get; set; }
        public string TipoCodigoAutorizacion { get; set; }
        public string CodigoAutorizacion { get; set; }
        public DateTime VencimientoCodigoAutorizacion { get; set; }
        //public string Archivo { get; set; }
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
            decimal ImporteAbonable = ImportePrimerVencimiento - Math.Abs(ImporteSaldoAnterior);
            if (ImporteAbonable < 0)
            {
                ImporteAbonable = 0;
            }
            return ImporteAbonable;
        }

        public string obtenerAtributo(string atributo)
        {
            return atributo switch
            {
                "empresa" => Empresa,
                "numerocliente" => NumeroCliente,
                "tipofactura" => TipoFactura,
                "puntoventa" => PuntoVenta,
                "numerofactura" => NumeroFactura,
                "fechaemision" => FechaEmision.ToString("yyyy-MM-dd"),
                "fechavencimiento" => FechaVencimiento.ToString("yyyy-MM-dd"),
                "periodo" => Periodo,
                "importeprimervencimiento" => ImportePrimerVencimiento.ToString("F2"),
                "importesaldoanterior" => ImporteSaldoAnterior.ToString("F2"),
                "importeabonable" => ImporteAbonable.ToString("F2"),
                "cuit" => CUIT.ToString(),
                "objetogasto" => ObjetoGasto,
                "codigocatalogo" => CodigoCatalogo,
                "tipocodigoautorizacion" => TipoCodigoAutorizacion,
                "codigoautorizacion" => CodigoAutorizacion,
                "vencimientocodigoautorizacion" => VencimientoCodigoAutorizacion.ToString("yyyy-MM-dd"),
                //"archivo" => Archivo,
                "tiposervicio" => TipoServicio,
                "tarifa" => Tarifa,
                _ => "",
            };
        }
    }
}
