using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Models
{
    internal class Dependencias
    {
        public Dependencias(string pEmpresa ="", long pCUIT = 0, int pCodigo = 0, int pUG =0) 
        {
            Empresa = pEmpresa;
            CUIT = pCUIT;
            Codigo = pCodigo;
            UGeografica = pUG;
        }
        public string Empresa { get; set; }
        public long CUIT { get; set; }
        public int Codigo { get; set; }
        public int UGeografica { get; set; }

    }
}
