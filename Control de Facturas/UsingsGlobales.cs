/* 
  Pseudocódigo / Plan (documentación en español, detallada):

  1. Propósito del archivo:
     - Centralizar las directivas `global using` que se usan en todo el proyecto
       para reducir duplicación y mejorar la legibilidad de los archivos fuente.

  2. Estructura y grupos de usings:
     - Agrupar por categorías lógicas:
       a) Núcleo de .NET (System, Collections, Linq, Text, Regex)
       b) UI (Windows Forms)
       c) Espacio de nombres del proyecto (Models, Processors, Servicios)
       d) Bibliotecas de terceros (PdfPig, ClosedXML, NPOI)
     - Mantener cada `global using` en una línea para facilitar el mantenimiento.

  3. Buenas prácticas:
     - Documentar por qué se incluye cada grupo, para que un desarrollador sepa
       si puede eliminar una dependencia si deja de usarse.
     - Evitar `global using` innecesarios: si una librería solo se usa en un
       módulo concreto, preferir `using` local en ese archivo.
     - Conservar compatibilidad con C# 14 / .NET 10 (no afectan los `global using`).

  4. Cómo extender o modificar:
     - Para añadir una dependencia global, colocarla en la categoría adecuada.
     - Para remover, buscar referencias en el proyecto; si no hay usos, eliminar.
     - Mantener comentarios cortos junto a usings no triviales para justificar su presencia.

  5. Resultado esperado:
     - Archivo compacto y descriptivo que agrupa todas las directivas globales
       necesarias para compilar y desarrollar el proyecto "Control de Facturas".
*/

global using System;                            // Tipos base y excepciones
global using System.Collections.Generic;        // Colecciones genéricas (List, Dictionary, ...)
global using System.Linq;                       // Operadores LINQ
global using System.Text;                       // Encodings y StringBuilder
global using System.Windows.Forms;              // WinForms: controles y formularios
global using System.IO;                         // IO: archivos y directorios
global using Control_de_Facturas;               // Espacio de nombres raíz del proyecto
global using Control_de_Facturas.Models;        // Modelos de dominio del proyecto
global using Control_de_Facturas.Processors;    // Lógica de procesamiento central
global using Control_de_Facturas.Servicios;     // Servicios (persistencia, IO, etc.)
global using UglyToad.PdfPig;                   // Lectura y extracción de texto de PDF
global using UglyToad.PdfPig.Content;           // Contenido de PDF (páginas, texto)
global using System.Text.RegularExpressions;    // Expresiones regulares (Regex)
global using ClosedXML.Excel;                   // Lectura/escritura de Excel .xlsx
global using NPOI.HSSF.UserModel;               // NPOI: soporte para .xls (HSSF)
global using NPOI.XSSF.UserModel;               // NPOI: soporte para .xlsx (XSSF)
global using NPOI.SS.UserModel;                 // Interfaces comunes de NPOI (IWorkbook, ISheet)
global using System.Globalization;              // Cultura e internacionalización
