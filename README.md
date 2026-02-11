# Programa Servicios Básicos

## 📌 Descripción general

**Programa Servicios Básicos** es una aplicación de escritorio desarrollada en **C# (.NET – Windows Forms)** cuyo objetivo es **centralizar, procesar y analizar facturas de servicios básicos** como **luz, gas y agua**. El sistema permite cargar facturas, extraer información relevante, organizarla por empresa y período, y generar reportes y archivos de salida (por ejemplo Excel) para su posterior uso administrativo y contable.

El proyecto surge como una herramienta práctica para **automatizar tareas repetitivas**, reducir errores manuales y facilitar el control de facturación en entornos administrativos.

---

## 🎯 Objetivos del sistema

* Centralizar la gestión de facturas de servicios básicos.
* Automatizar la **lectura y procesamiento de datos** provenientes de archivos (PDF / texto).
* Permitir la **edición y validación manual** de facturas cargadas.
* Organizar facturas por **empresa, tipo de servicio y período**.
* Generar **reportes y exportaciones a Excel** (ej. SIDIF u otros formatos).
* Servir como base extensible para futuras mejoras.

---

## 🛠️ Tecnologías utilizadas

* **Lenguaje:** C#
* **Framework:** .NET (Windows)
* **Tipo de aplicación:** Windows Forms
* **IDE recomendado:** Visual Studio 2022/2026
* **Librerías destacadas:**

  * OpenXML / Excel (para exportaciones)
  * Expresiones regulares (Regex) para parseo de datos

---

## 🧱 Estructura general del proyecto

* **Forms /**

  * Formularios principales (UI, DataGridView, eventos, navegación)
* **Clases /**

  * Modelos de dominio (Factura, Empresa, etc.)
  * Lógica de negocio y servicios
* **Servicios /**

  * Procesamiento de facturas
  * Búsquedas (CUIT, períodos, importes, etc.)
* **Assets /**

  * Plantillas de Excel
  * Recursos estáticos
* **Program.cs / Form1.cs**

  * Punto de entrada y formulario principal

---

## ⚙️ Funcionalidades principales

* 📄 **Carga de facturas** desde archivos
* 🔍 **Parseo automático de datos** (fechas, importes, CUIT, período)
* 🧾 **Edición manual de facturas** desde la interfaz
* 🗂️ **Clasificación por tipo de servicio** mediante pestañas
* 📊 **Generación de reportes y liquidaciones**
* 📤 **Exportación a Excel** con plantillas configurables
* 🧠 Manejo de cultura numérica y fechas (`InvariantCulture`)

---

## ▶️ Cómo ejecutar el proyecto

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/Ozhammar/Programa-Servicios-Basicos.git
   ```
2. Abrir el archivo de solución en **Visual Studio**.
3. Verificar que el proyecto apunte a una versión compatible de **.NET Desktop**.
4. Restaurar dependencias si fuera necesario.
5. Compilar y ejecutar (`F5`).

---

## 📦 Requisitos

* Windows 10/11
* Visual Studio (con carga de trabajo **Desarrollo de escritorio .NET**)
* .NET Desktop Runtime compatible

---

## 🚧 Estado del proyecto

🟡 **En desarrollo activo**

El proyecto se encuentra en evolución constante, con mejoras continuas en:

* Robustez del parseo de facturas
* Modularización del código
* Experiencia de usuario (UI)
* Manejo de errores y validaciones

---

## 🔮 Mejoras futuras previstas

* Soporte para más formatos de factura
* Persistencia en base de datos
* Filtros y búsquedas avanzadas
* Reportes gráficos
* Instalador y distribución del ejecutable

---

## 👤 Autor

**Lucas Povolo**
GitHub: [https://github.com/Ozhammar](https://github.com/Ozhammar)

---

## 📄 Licencia

Este proyecto se distribuye con fines educativos y prácticos. La licencia podrá definirse en futuras versiones.
