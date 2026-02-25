# Programa Servicios Básicos

## 📌 Descripción general

**Programa Servicios Básicos** es una aplicación de escritorio desarrollada en **C# (.NET – Windows Forms)** cuyo objetivo es **centralizar, procesar y analizar facturas de servicios básicos** como **luz, gas y agua**. El sistema permite cargar facturas, extraer información relevante, organizarla por empresa y período, editar datos manualmente y generar reportes y archivos de salida (por ejemplo Excel) para su posterior uso administrativo y contable.

El proyecto surge como una herramienta práctica para **automatizar tareas repetitivas**, reducir errores manuales y facilitar el control de facturación en entornos administrativos.

---

## 🎯 Objetivos del sistema

* Centralizar la gestión de facturas de servicios básicos.
* Automatizar la **lectura y procesamiento de datos** provenientes de archivos (PDF / texto).
* Permitir la **edición individual y múltiple** de facturas cargadas.
* Organizar facturas por **empresa, tipo de servicio y período**.
* Generar **reportes estructurados por período**.
* Exportar información a **Excel** (SIDIF y otros formatos).
* Servir como base extensible para futuras mejoras.

---

## 🛠️ Tecnologías utilizadas

* **Lenguaje:** C#
* **Framework:** .NET (Windows Desktop)
* **Tipo de aplicación:** Windows Forms
* **IDE recomendado:** Visual Studio 2022/2026
* **Librerías destacadas:**

  * OpenXML / Excel (para exportaciones)
  * Expresiones regulares (Regex) para parseo de datos
  * Uso de reflexión (`Reflection`) para edición dinámica de propiedades

---

## 🧱 Estructura general del proyecto

* **Forms /**

  * Formularios principales (UI, DataGridView, eventos, navegación)
  * Gestión de selección y modificación múltiple
* **Clases /**

  * Modelos de dominio (Factura, Empresa, etc.)
  * Lógica de negocio
* **Controladores /**

  * Coordinación entre UI y modelo
  * Modificación dinámica de facturas
* **Servicios /**

  * Procesamiento de facturas por empresa
  * Generación de informes
  * Exportadores específicos
* **Assets /**

  * Plantillas de Excel
  * Recursos estáticos
* **Program.cs / Form1.cs**

  * Punto de entrada y formulario principal

---

## ⚙️ Funcionalidades principales

* 📄 **Carga masiva de facturas** desde carpetas

* 🔍 **Parseo automático de datos** (fechas, importes, CUIT, período)

* 🏢 Soporte para múltiples empresas:

  * Edesur
  * Edenor
  * Metrogas (segmentos chicos y grandes)
  * AySA
  * Camuzzi Gas del Sur
  * Camuzzi Gas Pampeana

* 🧾 **Edición manual individual** mediante doble click en el grid

* ✅ **Modificación múltiple** de facturas seleccionadas

* 🔄 Recalculo automático de **ImporteAbonable**

* 🧠 Corrección de cálculo cuando el saldo anterior es negativo

* 📊 **Generación de informes divididos por período**

* 📤 **Exportación a Excel** con plantillas configurables

* ⚡ Exportador específico para Edenor

* 🌍 Manejo de cultura numérica y fechas (`InvariantCulture`)

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

* Optimización del algoritmo de informes por período
* Mejora en el control de superposición de períodos entre años
* Modularización y separación de responsabilidades
* Experiencia de usuario (UI)
* Validaciones y manejo de errores

---

## 🔮 Mejoras futuras previstas

* Persistencia en base de datos
* Mejor manejo de carpetas con múltiples años y períodos superpuestos
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
---


