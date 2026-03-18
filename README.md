# Control de Facturas

**Sistema de Procesamiento Automático de Facturas de Servicios Públicos**

Versión 1.0 · .NET 8 / C# · Windows Forms

---

## Índice

1. [Descripción general](#1-descripción-general)
2. [Propósito y contexto de uso](#2-propósito-y-contexto-de-uso)
3. [Tecnologías y dependencias](#3-tecnologías-y-dependencias)
4. [Arquitectura del sistema](#4-arquitectura-del-sistema)
5. [Estructura de archivos del proyecto](#5-estructura-de-archivos-del-proyecto)
6. [Flujo de procesamiento de PDFs](#6-flujo-de-procesamiento-de-pdfs)
7. [Empresas y proveedores soportados](#7-empresas-y-proveedores-soportados)
8. [Módulos del sistema](#8-módulos-del-sistema)
9. [Modelos de datos](#9-modelos-de-datos)
10. [Configuración y archivos de soporte](#10-configuración-y-archivos-de-soporte)
11. [Instalación y requisitos del sistema](#11-instalación-y-requisitos-del-sistema)
12. [Uso del programa](#12-uso-del-programa)
13. [Limitaciones conocidas y trabajo futuro](#13-limitaciones-conocidas-y-trabajo-futuro)
14. [Autoría y propiedad intelectual](#14-autoría-y-propiedad-intelectual)

---

## 1. Descripción general

Control de Facturas es una aplicación de escritorio desarrollada en C# con Windows Forms sobre la plataforma .NET 8. Su función principal es automatizar la lectura, extracción, validación y exportación de datos de facturas de servicios públicos (electricidad, gas y agua) recibidas en formato PDF, transformándolas en planillas Excel compatibles con el sistema SIDIF de administración financiera pública del Estado Nacional argentino.

El sistema elimina la carga de datos manual que anteriormente debía realizarse comprobante por comprobante, reduciendo el tiempo de procesamiento de un lote de facturas de horas a minutos y minimizando los errores de transcripción.

---

## 2. Propósito y contexto de uso

La aplicación fue desarrollada por iniciativa propia de su autor para el procesamiento de facturas de servicios públicos en el contexto de la administración de patrimonio de un organismo de seguridad del Estado Nacional argentino. Opera sobre lotes de PDFs descargados de los portales de los distintos prestadores (Edesur, Edenor, AySA, Metrogas, Camuzzi, etc.) y genera los archivos Excel necesarios para la carga en el sistema SIDIF.

El flujo de trabajo típico es:

1. El área recibe las facturas en formato PDF descargadas de los portales de las empresas prestadoras.
2. Se selecciona la carpeta que contiene los PDFs en la aplicación.
3. El sistema procesa automáticamente cada archivo, identifica la empresa emisora y extrae los datos relevantes.
4. El usuario revisa y, si es necesario, corrige manualmente los datos extraídos.
5. El sistema exporta las planillas Excel en formato SIDIF listas para su carga.

---

## 3. Tecnologías y dependencias

### Plataforma

| Tecnología | Detalle |
|---|---|
| Lenguaje | C# 13 |
| Framework | .NET 10 (Windows) — `net10.0-windows` |
| UI | Windows Forms (WinForms) |
| IDE recomendado | Visual Studio 2022 o superior |
| SO requerido | Windows 10 / 11 (64-bit) |

### Paquetes NuGet

| Paquete | Versión | Propósito | Notas |
|---|---|---|---|
| `PdfPig` | 0.1.13 | Lectura y extracción de texto de PDFs | Sin dependencias nativas |
| `ClosedXML` | 0.105.0 | Lectura y escritura de archivos `.xlsx` | API de alto nivel |
| `NPOI` | 2.7.5 | Conversión `.xlsx` → `.xls` (formato SIDIF) | Soporte HSSF/XSSF |

---

## 4. Arquitectura del sistema

La aplicación sigue una arquitectura en capas separada por responsabilidades.

### Capa de presentación (UI)

- **Form1**: formulario principal; gestiona la selección de carpeta, la barra de progreso, la grilla de facturas y los botones de exportación.
- **FormularioCargaManual**: formulario secundario para ingresar facturas que no pudieron procesarse automáticamente.

### Capa de servicios (orquestación)

- **ControladorFacturas**: núcleo del sistema. Coordina la lectura de PDFs, la identificación de empresa y la delegación al procesador correcto. Implementa también el filtrado, ordenamiento y modificación de facturas.
- **ExportadorExcel**: genera los archivos Excel en formato SIDIF (tres hojas: cabecera, detalle de comprobante y detalle presupuestario) y los informes por empresa.
- **ArmadoLotesPago**: agrupa facturas en lotes de pago con observaciones formateadas para los sistemas bancarios, respetando límites de caracteres.
- **GestorArchivos**: abstrae las operaciones de sistema de archivos (enumerar PDFs, leer texto, renombrar con nomenclatura estándar).
- **BuscadorCUIT**: consulta la planilla maestra de beneficiarios para resolver razones sociales a partir del CUIT.
- **BuscadorUD_UG**: resuelve la unidad de dependencia y ubicación geográfica a partir del CUIT del proveedor.
- **ConvertidorImportes**: parsea importes en formato argentino (separadores de miles y decimales variables) de manera robusta.
- **ConvertidorExcel**: convierte archivos `.xlsx` al formato `.xls` requerido por SIDIF.

### Capa de procesadores (por empresa)

Cada procesador implementa la lógica de extracción de datos específica para un proveedor mediante expresiones regulares sobre el texto del PDF.

| Procesador | Servicio | Empresa |
|---|---|---|
| `ProcesadorEdesur` | Electricidad | EDESUR S.A. |
| `ProcesadorEdenor` | Electricidad | EDENOR S.A. |
| `ProcesadorAYSA` | Agua | AySA S.A. |
| `ProcesadorMetrogasGrandes` | Gas | Metrogas (Grandes Clientes) |
| `ProcesadorMetrogasPequenios` | Gas | Metrogas (Pequeños Clientes) |
| `ProcesadorGasInterior` | Gas | Camuzzi, Naturgy, Litoralgas y otros |
| `ProcesadorAguaInterior` | Agua | OSMGP, Aguas del Tucumán, ASSA y otros |
| `ProcesadorElectricidadInterior` | Electricidad | EDEA, EDELAP, Edesal y otros |
| `ProcesadorCargaManual` | Todos | Cualquier empresa (carga manual) |

### Capa de modelos

- **Factura**: modelo central con todos los campos del comprobante y los métodos `EsValida()` y `CalcularImporteAbonable()`.
- **DatosBasicosExcel**: configuración fija para la generación del Excel SIDIF (SAF, jurisdicción, fuente de financiamiento, etc.).
- **DatosFactura, DatosCatalogo, Dependencias, Empresas, LotesPago**: modelos auxiliares.

---

## 5. Estructura de archivos del proyecto

```
Control_de_Facturas/
├── Assets/
│   └── Plantillas/
│       ├── PLANTILLA.xlsx                    ← Plantilla base SIDIF
│       ├── BENEFICIARIOS AGUA, GAS Y LUZ.xlsx
│       ├── UBICACIONES GEOGRAFICAS POR DEPENDENCIA Y EMPRESA.xlsx
│       └── Plantillas Pagos/
│           ├── AYSA.xlsx
│           ├── EDESUR.xlsx
│           ├── EDENOR.xlsx
│           ├── METROGAS PEQUEÑOS.xlsx
│           ├── METROGAS GRANDES.xlsx
│           └── INTERIOR.xlsx
├── Models/
│   ├── Factura.cs
│   ├── DatosBasicosExcel.cs
│   ├── DatosFactura.cs
│   ├── DatosCatalogo.cs
│   ├── Dependencias.cs
│   ├── Empresas.cs
│   └── LotesPago.cs
├── Processors/
│   ├── ProcesadorEdesur.cs
│   ├── ProcesadorEdenor.cs
│   ├── ProcesadorAYSA.cs
│   ├── ProcesadorMetrogasGrandes.cs
│   ├── ProcesadorMetrogasPequenios.cs
│   ├── ProcesadorGasInterior.cs
│   ├── ProcesadorAguaInterior.cs
│   ├── ProcesadorElectricidadInterior.cs
│   └── ProcesadorCargaManual.cs
├── Servicios/
│   ├── ControladorFacturas.cs
│   ├── ExportadorExcel.cs
│   ├── ArmadoLotesPago.cs
│   ├── GestorArchivos.cs
│   ├── BuscadorCUIT.cs
│   ├── BuscadorUD_UG.cs
│   ├── ConvertidorImportes.cs
│   ├── ConvertidorExcel.cs
│   ├── ConfiguracionExcel.cs
│   ├── ControladorCamuzzi.cs
│   └── ControladorEdesal.cs
├── Form1.cs / Form1.Designer.cs
├── FormularioCargaManual.cs / .Designer.cs
├── UsingsGlobales.cs
└── Program.cs
```

---

## 6. Flujo de procesamiento de PDFs

1. El usuario selecciona una carpeta mediante el botón "Seleccionar carpeta".
2. `GestorArchivos.ObtenerPDF()` enumera todos los archivos `.pdf` de la carpeta (incluyendo subcarpetas).
3. Por cada PDF, `GestorArchivos.LeerPDF()` extrae el texto completo usando PdfPig.
4. `ControladorFacturas` verifica si el PDF requiere división en bloques (facturas que incluyen múltiples clientes en un mismo archivo, como Aguas del Tucumán o EDEA).
5. Si requiere división: el texto se parte en bloques por expresión regular y se genera una copia física del PDF por cada bloque adicional.
6. Para cada bloque de texto, `IdentificarYProcesarFactura()` determina la empresa mediante búsqueda de palabras clave o CUITs en el texto.
7. Se delega al procesador correspondiente, que aplica sus expresiones regulares y construye el objeto `Factura`.
8. `GestorArchivos.RenombrarArchivo()` renombra el PDF con el formato `EMPRESA_CLIENTE_PUNTOVENTA-NUMEROFACTURA.pdf`.
9. La factura es agregada a la lista principal (evitando duplicados por número de factura).
10. Al finalizar, las facturas se muestran en la grilla de Form1 para revisión y edición.

---

## 7. Empresas y proveedores soportados

### Procesamiento automático — Buenos Aires y AMBA

| Empresa | Servicio | Identificación |
|---|---|---|
| EDESUR S.A. | Electricidad | Texto `"Edesur"` |
| EDENOR S.A. | Electricidad | Texto `"edenor"` o CUIT `30-65511620-2` |
| AySA S.A. | Agua | Texto `"aysa"` o CUIT `30-70956507-5` |
| Metrogas (Grandes Clientes) | Gas | Texto `"Metrogas Grandes Clientes"` |
| Metrogas (Pequeños Clientes) | Gas | CUIT `30-65786367-6` |

### Procesamiento automático — Interior del país

| Empresa / Grupo | Servicio | Identificación |
|---|---|---|
| Camuzzi Gas Sur | Gas | Números de cuenta específicos |
| Camuzzi Gas Pampeana | Gas | Números de cuenta específicos |
| Naturgy / Redengas / Litoral Gas / GasNEA | Gas | CUITs y dominios web |
| Distribuidoras varias (Cuyana, Centro, etc.) | Gas | CUITs y nombres |
| Aguas del Tucumán / ASSA / Aguas de Formosa | Agua | CUITs y nombres |
| Aguas de Catamarca / OSMGP Ushuaia | Agua | CUITs y palabras clave |
| EDEA / EDELAP / Edesal y otras distribuidoras | Electricidad | CUITs |

### Carga manual

Las facturas de empresas no reconocidas automáticamente pueden cargarse a través del `FormularioCargaManual`, que permite ingresar todos los datos del comprobante. El sistema busca automáticamente la razón social en la planilla de beneficiarios al ingresar el CUIT.

---

## 8. Módulos del sistema

### ControladorFacturas

Clase central de orquestación. Métodos principales:

- `ProcesarFacturasEnCarpeta(carpeta, progreso)`: procesa de forma asíncrona todos los PDFs de una carpeta. Soporta reporte de progreso mediante `IProgress<int>`.
- `IdentificarYProcesarFactura(texto, ruta)`: identifica la empresa y delega al procesador correspondiente.
- `corroborarInterior(texto)`: determina si un PDF corresponde a una distribuidora del interior y qué tipo de servicio.
- `FiltrarPorEmpresa` / `filtrarPorTipoServicio`: filtra la lista de facturas según criterio.
- `OrdenarSegunEmpresa`: aplica criterio de ordenamiento específico por empresa (Edesur/Edenor por tarifa y cliente; Metrogas por cliente y fecha).
- `ModificarFactura` / `ModificarMultiplesFacturas`: modifica propiedades de facturas vía reflexión, con conversión automática de tipos.

### ExportadorExcel

Genera los archivos de exportación. Trabaja con dos tipos de salidas:

- `generarLiquidacionIndividual`: genera el Excel SIDIF con las tres hojas requeridas (`Cabecera-Cpte`, `Detalle Cpte FacGS`, `Detalle Presupuestario FACGS`).
- **Informes por empresa**: utiliza plantillas predefinidas por empresa (`AYSA.xlsx`, `EDESUR.xlsx`, etc.) para generar reportes específicos.

> **Nota técnica:** el sistema SIDIF requiere el archivo en formato `.xls` (Excel 97-2003). `ConvertidorExcel` se encarga de la conversión desde `.xlsx` una vez generado el archivo.

### ArmadoLotesPago

Genera los bloques de texto de observaciones para órdenes de pago bancarias. Agrupa facturas del mismo cliente y corta el lote cuando se alcanza el límite de caracteres configurado (por defecto 250), generando un nuevo lote automáticamente. Cada lote incluye: encabezado de servicio, número de cliente, y por cada factura el punto de venta, número, período e importe.

### GestorArchivos

Servicio de sistema de archivos. Métodos:

- `ObtenerPDF(path)`: enumera archivos `.pdf` de forma recursiva usando `Directory.EnumerateFiles`.
- `LeerPDF(path)`: extrae el texto de todas las páginas del PDF usando PdfPig.
- `RenombrarArchivo(path, empresa, cliente, puntoVenta, numero)`: renombra el archivo con formato estándar. Si el nombre destino ya coincide con el actual, no hace nada.

### BuscadorCUIT y BuscadorUD_UG

Consultan planillas Excel de referencia en `Assets/Plantillas/` para resolver datos maestros:

- **BuscadorCUIT**: lee `BENEFICIARIOS AGUA, GAS Y LUZ.xlsx` y devuelve la razón social y datos del beneficiario dado un CUIT. También permite la búsqueda inversa (CUIT por razón social).
- **BuscadorUD_UG**: lee `UBICACIONES GEOGRAFICAS POR DEPENDENCIA Y EMPRESA.xlsx` y devuelve el código de dependencia y ubicación geográfica dado el CUIT del proveedor.

### ConvertidorImportes

Resuelve la ambigüedad de los separadores de decimales en importes argentinos. El método `ParseImporteFlexible` analiza la posición relativa de la última coma y el último punto para determinar cuál es el separador decimal, y normaliza el valor antes del parseo. Esto evita errores cuando distintas empresas usan formatos diferentes (`"1.234,56"` vs `"1,234.56"` vs `"1234.56"`).

---

## 9. Modelos de datos

### Factura

Modelo central del sistema.

| Propiedad | Tipo | Descripción |
|---|---|---|
| `Empresa` | `string` | Nombre del prestador |
| `NumeroCliente` | `string` | Número de cuenta del cliente |
| `TipoFactura` | `string` | Tipo de comprobante (B) |
| `PuntoVenta` | `string` | Punto de venta (4 dígitos) |
| `NumeroFactura` | `string` | Número de comprobante (8 dígitos) |
| `FechaEmision` / `FechaVencimiento` | `DateTime` | Fechas del comprobante |
| `Periodo` | `string` | Mes de facturación (ej: `ENERO`) |
| `ImportePrimerVencimiento` | `decimal` | Total a pagar al primer vencimiento |
| `ImporteSaldoAnterior` | `decimal` | Deuda anterior (negativo = saldo a favor) |
| `ImporteAbonable` | `decimal` | Importe neto a pagar |
| `CUIT` | `long` | CUIT del prestador (fijo por empresa) |
| `ObjetoGasto` / `CodigoCatalogo` | `string` | Clasificación presupuestaria SIDIF |
| `CodigoAutorizacion` | `string` | Código CESP (14 dígitos) |
| `VencimientoCodigoAutorizacion` | `DateTime` | Vencimiento del CESP |
| `TipoServicio` | `string` | `ELECTRICIDAD` / `GAS` / `AGUA` |
| `Tarifa` | `string` | Tarifa eléctrica (solo Edesur/Edenor) |
| `Archivo` | `string` | Ruta al PDF renombrado |
| `Seleccionada` | `bool` | Indica si está seleccionada en la grilla |

Métodos de la clase `Factura`:

- `EsValida()`: verifica que el número de factura, el número de cliente, la fecha de vencimiento y el importe primer vencimiento sean válidos y no vacíos.
- `CalcularImporteAbonable()`: devuelve `ImportePrimerVencimiento` menos el valor absoluto del saldo anterior (si el saldo anterior es negativo —saldo a favor—, se trata como cero).
- `obtenerAtributo(string)`: acceso dinámico a cualquier propiedad por nombre, usado en la grilla de edición.

### DatosBasicosExcel — Configuración SIDIF

Contiene los valores constantes requeridos por el formato de importación SIDIF. Valores configurados por defecto:

- SAF: `326` | Tipo comprobante: `FACGS` | Jurisdicción: `41` | SubJurisdicción: `4`
- Fuente de financiamiento: `11` | Medio de pago: `RC` | Tipo moneda: `ARP`
- Códigos impositivos: todos `EXE` (exento de IVA, Ganancias y SUSS)

---

## 10. Configuración y archivos de soporte

El sistema depende de tres archivos de configuración ubicados en `Assets/Plantillas/` que deben estar presentes para el correcto funcionamiento.

### PLANTILLA.xlsx

Plantilla base para la generación del Excel SIDIF. Debe contener tres hojas con los nombres exactos: `Cabecera-Cpte`, `Detalle Cpte FacGS` y `Detalle Presupuestario FACGS`, con el formato de columnas requerido por el sistema.

### BENEFICIARIOS AGUA, GAS Y LUZ.xlsx

Planilla maestra de beneficiarios. Hoja `"Hoja1"`. Columnas: (1) Número de beneficiario, (2) Denominación / Razón Social, (3) Tipo de documento, (4) Número de documento (CUIT), (5) Indicador de código de autorización (`"si"` / `"no"`). Se usa para autocompletar la razón social al ingresar un CUIT en la carga manual.

### UBICACIONES GEOGRAFICAS POR DEPENDENCIA Y EMPRESA.xlsx

Hoja `"UBICACIONES GEOGRAFICAS"`. Columnas: (1) Nombre empresa, (2) CUIT, (3) Código de dependencia, (4) Ubicación geográfica. El sistema la consulta para determinar qué unidad de dependencia y ubicación geográfica asignar a cada factura según el CUIT del proveedor.

### Plantillas de informes por empresa

Cada empresa tiene su plantilla específica (`AYSA.xlsx`, `EDESUR.xlsx`, etc.) en la subcarpeta `Plantillas Pagos/`. Estas plantillas definen el formato de los informes internos de pago generados para cada prestador.

---

## 11. Instalación y requisitos del sistema

### Requisitos

- Windows 10 o superior (64-bit)
- .NET 8 Runtime (se puede incluir en el instalador publicando como self-contained)
- Microsoft Excel **no requerido** (el sistema usa ClosedXML y NPOI)

### Instalación desde código fuente

1. Clonar o descomprimir el repositorio.
2. Abrir la solución en Visual Studio 2022 o superior.
3. Restaurar paquetes NuGet (automático al compilar).
4. Verificar que la carpeta `Assets/Plantillas/` esté presente con los archivos requeridos.
5. Compilar y ejecutar (`F5` para depuración, `Ctrl+F5` para ejecución directa).

### Publicación

Para generar un ejecutable autónomo sin requerir .NET instalado en el equipo destino:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -f net10.0-windows
```

> **Importante:** la carpeta `Assets/Plantillas/` debe copiarse manualmente junto al ejecutable publicado, ya que contiene archivos de datos que el programa accede en tiempo de ejecución mediante rutas relativas al directorio base de la aplicación.

---

## 12. Uso del programa

### Procesamiento automático

1. Hacer clic en "Seleccionar carpeta" y elegir la carpeta que contiene los PDFs.
2. Verificar la cantidad de archivos detectados en el contador.
3. Hacer clic en "Ejecutar". El sistema procesa los PDFs y muestra el progreso.
4. Al finalizar, las facturas aparecen en la grilla. Revisar los datos extraídos.
5. Para modificar un dato, hacer doble clic en la celda correspondiente.
6. Seleccionar las facturas a exportar mediante los checkboxes.
7. Elegir el tipo de exportación deseado (SIDIF, informe por empresa, lotes de pago).

### Carga manual

1. Hacer clic en el botón de carga manual.
2. Ingresar el CUIT del proveedor. El sistema busca automáticamente la razón social.
3. Completar los campos del comprobante.
4. Hacer clic en "Cargar". La factura se agrega a la lista principal.

---

## 13. Limitaciones conocidas y trabajo futuro

### Limitaciones actuales

- `generarLiquidacionUnica()` no está implementado aún (lanza `NotImplementedException`).
- La identificación de empresas por palabras clave puede dar falsos positivos si una factura contiene accidentalmente el nombre o CUIT de otra empresa.
- Las palabras clave `"SANTA FE"`, `"ROSARIO"` y `"RECONQUISTA"` aparecen en las listas tanto de AGUA como de LUZ, lo que puede generar clasificaciones incorrectas en casos de borde.
- Los números de cuenta de Camuzzi están duplicados entre `ControladorCamuzzi.cs` y la lista `corroborarInterior()` en `ControladorFacturas.cs`.
- La conversión de importes usa `decimal.Parse()` directo en algunos procesadores en lugar del `ConvertidorImportes` más robusto, lo que puede fallar en sistemas con cultura configurada en inglés.

### Mejoras planificadas

- Implementar clase base abstracta `ProcesadorBase` para eliminar duplicación de código entre procesadores.
- Centralizar las listas de palabras clave / CUITs de Camuzzi en un único lugar.
- Estandarizar el uso de `ConvertidorImportes` en todos los procesadores.
- Implementar `generarLiquidacionUnica()`.
- Agregar tests unitarios para los procesadores usando facturas de ejemplo anonimizadas.

---

## 14. Autoría y propiedad intelectual

Copyright © 2025. Todos los derechos reservados.

Este software fue desarrollado íntegramente por su autor por iniciativa propia, sin haber sido encargado, solicitado ni financiado por ningún organismo o empleador. El diseño arquitectónico, el código fuente, las soluciones técnicas implementadas y la presente documentación son obra exclusiva del desarrollador.

Queda prohibida la reproducción total o parcial del código fuente, su distribución, modificación o uso comercial sin autorización expresa y por escrito del autor.
