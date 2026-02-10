using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Models
{
    public class Empresas
    {
        public Empresas(int pNumeroBeneficiario = 0, string pDenominacion = "", string pTipoDocumento = "", string pNumeroDocumento ="", string pCodAuth = "")
        {
            NumeroBeneficiario = pNumeroBeneficiario;
            Denominacion = pDenominacion;
            TipoDocumento = pTipoDocumento;
            NumeroDocumento = pNumeroDocumento;
            CodAuth = pCodAuth;
        }

        public int NumeroBeneficiario { get; set; }
        public string Denominacion { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodAuth { get; set; }

    }
}
