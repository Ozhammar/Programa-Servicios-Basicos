namespace Control_de_Facturas.Models
{
    internal class DatosBasicosExcel
    {
        public DatosBasicosExcel(
            int pSaf,
            string pTipoComprobante,
            int pEjercicio,
            string pTipoDocumento, //CUI
            string pTipoMoneda, //ARP
            int pCotizacion, // 1
            string pMedioPago, //RC
            int pCantidadUnidades,
            int pJurisdiccion,
            int pSubJurisdiccion,
            int pEntidad,
            int pFuenteFinanciamiento,
            int pMoneda,
            int pPex,
            int pBapin,
            string pCodigoGanancias,
            string pCodigoIVA,
            string pCodigoSUSS,
            int pPorcentualIVA
        )
        {
            //CABECERA Y GENERALES
            SAF = pSaf;
            TipoComprobante = pTipoComprobante;
            Ejercicio = pEjercicio;
            TipoDocumento = pTipoDocumento;
            TipoMoneda = pTipoMoneda;
            Cotizacion = pCotizacion;
            MedioPago = pMedioPago;

            //DETALLE COMPROBANTE
            CantidadUnidades = pCantidadUnidades;



            //DETALLE PRESUPUESTARIO
            Jurisdiccion = pJurisdiccion;
            SubJurisdiccion = pSubJurisdiccion;
            Entidad = pEntidad;
            //CODIGO DEPENDENCIA
            //UBICACION GEOGRAFICA
            FuenteFinanciamiento = pFuenteFinanciamiento;
            Moneda = pMoneda;
            PEX = pPex;
            BAPIN = pBapin;
            CodigoGanancias = pCodigoGanancias;
            CodigoIVA = pCodigoIVA;
            CodigoSUSS = pCodigoSUSS;
            PorcentualIVA = pPorcentualIVA;
        }

        // PROPIEDADES
        public int SAF { get; set; }
        public string TipoComprobante { get; set; }
        public int Ejercicio { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoMoneda { get; set; }
        public int Cotizacion { get; set; }
        public string MedioPago { get; set; }
        public int CantidadUnidades { get; set; }
        public int Jurisdiccion { get; set; }
        public int SubJurisdiccion { get; set; }
        public int Entidad { get; set; }
        public int FuenteFinanciamiento { get; set; }
        public int Moneda { get; set; }
        public int PEX { get; set; }
        public int BAPIN { get; set; }
        public string CodigoGanancias { get; set; }
        public string CodigoIVA { get; set; }
        public string CodigoSUSS { get; set; }
        public int PorcentualIVA { get; set; }
    }
}
