using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Models
{
    internal class DatosFactura
    {
        public DatosFactura
            (
            string pDenominacion = "",
            string pCliente = "",
            string pTipoFactura = "",
            string pPuntoVenta = "",
            string pNumeroFactura = "",
            DateTime? pFechaEmision = null,
            DateTime? pFechaVencimientoFC = null,
            string pPeriodo = "",
            decimal pImporte = 0,
            long pCUIT = 0,
            string pTipoCodAut = "",
            string pCodigoAut = "",
            DateTime? pVencimientoCodAut = null,
            string pTipoServicio = ""
            )
        {
            Denominacion = pDenominacion;
            Cliente = pCliente;
            TipoFactura = pTipoFactura;
            PuntoVenta = pPuntoVenta;
            NumeroFactura = pNumeroFactura;
            FechaEmision = pFechaEmision ?? DateTime.MinValue;
            FechaVencimientoFC = pFechaVencimientoFC ?? DateTime.MinValue;
            Periodo = pPeriodo;
            Importe = pImporte;
            CUIT = pCUIT;
            TipoCodAut = pTipoCodAut;
            CodigoAut = pCodigoAut;
            VencimientoCodAut = pVencimientoCodAut ?? DateTime.MinValue;
            TipoServicio = pTipoServicio;
        }
        public string Denominacion { get; set; }
        public string Cliente { get; set; }
        public string TipoFactura { get; set; }
        public string PuntoVenta { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimientoFC { get; set; }
        public string Periodo { get; set; }
        public decimal Importe { get; set; }
        public long CUIT { get; set; }
        public string TipoCodAut { get; set; }
        public string TipoServicio { get; set; }
        public string CodigoAut { get; set; }
        public DateTime VencimientoCodAut { get; set; }

    }
}
