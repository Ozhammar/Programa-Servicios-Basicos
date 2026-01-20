using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{
    internal class ConfiguracionExcel
    {
        public static DatosBasicosExcel CrearPorDefecto()
        {
            return new DatosBasicosExcel(
                pSaf: 326,
                pTipoComprobante: "FACGS",
                pEjercicio: DateTime.Now.Year,
                pTipoDocumento: "CUI",
                pTipoMoneda: "ARP",
                pCotizacion: 1,
                pMedioPago: "RC",
                pCantidadUnidades: 1,
                pJurisdiccion: 41,
                pSubJurisdiccion: 4,
                pEntidad: 0,
                pFuenteFinanciamiento: 0,
                pMoneda: 0,
                pPex: 0,
                pBapin: 0,
                pCodigoGanancias: "EXE",
                pCodigoIVA: "EXE",
                pCodigoSUSS: "EXE",
                pPorcentualIVA: 0

            );
        }
    }
}