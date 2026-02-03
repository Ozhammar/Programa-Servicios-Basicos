# Programa Servicios Básicos

## 📌 Descripción

**Programa Servicios Básicos** es una aplicación de escritorio desarrollada en **C# (.NET / Windows Forms)** cuyo objetivo es facilitar el **control, procesamiento y análisis de facturas de servicios básicos** (por ejemplo: electricidad, agua, gas, etc.).

El sistema permite cargar facturas desde archivos (principalmente PDF), procesarlas, organizarlas por distintos criterios y generar reportes, incluyendo exportaciones a **Excel**.

---

## 🎯 Objetivos del proyecto

* Centralizar el manejo de facturas de servicios básicos.
* Automatizar la lectura y procesamiento de datos desde archivos PDF.
* Facilitar el análisis por períodos, proveedores y montos.
* Generar reportes claros y exportables.

---

## 🛠️ Tecnologías utilizadas

* **Lenguaje:** C#
* **Framework:** .NET (Windows)
* **Tipo de aplicación:** Windows Forms
* **IDE:** Visual Studio
* **Librerías destacadas:**

  * Manejo de archivos (System.IO)
  * Procesamiento de PDF
  * Exportación a Excel (OpenXML u otras)

---

## 📂 Estructura del proyecto

```
Programa-Servicios-Basicos/
│
├── Control de Facturas/        # Proyecto principal
│   ├── Forms/                 # Formularios de la aplicación
│   ├── Clases/                # Clases de dominio (Factura, Gestores, etc.)
│   ├── Assets/                # Plantillas y recursos (Excel, etc.)
│   ├── Program.cs             # Punto de entrada
│   └── ...
│
├── Programa Servicios Basicos.slnx
├── .gitignore
├── .gitattributes
└── README.md
```

*(La estructura puede variar levemente según la versión del proyecto)*

---

## ⚙️ Funcionalidades principales

* 📄 **Carga de facturas** desde carpetas locales.
* 🔍 **Lectura y análisis de PDFs**.
* 🗂️ **Agrupación por período, proveedor o tipo de servicio**.
* 📊 **Generación de reportes**.
* 📤 **Exportación a Excel** utilizando plantillas.
* ⏳ Indicadores visuales de progreso durante el procesamiento.

---

## ▶️ Ejecución del proyecto

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/Ozhammar/Programa-Servicios-Basicos.git
   ```

2. Abrir el archivo de solución en **Visual Studio**:

   ```
   Programa Servicios Basicos.slnx
   ```

3. Restaurar dependencias (si aplica).

4. Compilar y ejecutar el proyecto (`F5`).

---

## 📌 Requisitos

* Windows 10 o superior
* .NET Desktop Runtime compatible
* Visual Studio 2022 o superior

---

## 🚧 Estado del proyecto

Proyecto en **desarrollo activo**, con mejoras continuas en:

* Manejo de errores
* Diseño de interfaz
* Optimización del procesamiento de archivos
* Modularización del código

---

## 📄 Licencia

Este proyecto se distribuye con fines **educativos y personales**. La licencia puede definirse en futuras versiones.

---

## 👤 Autor

**Lucas Povolo**
GitHub: [https://github.com/Ozhammar](https://github.com/Ozhammar)

---

## 💡 Notas adicionales

Si utilizás plantillas de Excel o archivos externos, asegurate de que la carpeta **Assets** esté correctamente incluida en el directorio de salida (`bin/Debug` o `bin/Release`).
