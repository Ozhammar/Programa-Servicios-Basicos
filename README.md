# Programa Servicios Básicos

# Descripción

# 

# Programa Servicios Básicos es una aplicación de escritorio desarrollada en C# (.NET Windows Forms) orientada al procesamiento, control y análisis de facturas de servicios básicos (electricidad, gas y agua).

# 

# La aplicación permite cargar facturas desde archivos PDF, extraer automáticamente información relevante, organizarla por empresa y período, realizar modificaciones manuales controladas y generar informes y exportaciones en formato Excel para uso administrativo y contable.

# 

# El objetivo principal del sistema es reducir tareas manuales repetitivas, minimizar errores operativos y estructurar el control de facturación de forma escalable.

# 

# Funcionalidades actuales

# Procesamiento de facturas

# 

# Carga masiva desde carpetas.

# 

# Parseo automático mediante expresiones regulares.

# 

# Identificación de empresa emisora.

# 

# Extracción de:

# 

# Períodos

# 

# Importes

# 

# Saldos anteriores

# 

# Importe abonable

# 

# CUIT y otros datos fiscales

# 

# Empresas soportadas

# 

# Edesur

# 

# Edenor

# 

# Metrogas (segmento chicos y grandes)

# 

# AySA

# 

# Camuzzi Gas del Sur

# 

# Camuzzi Gas Pampeana

# 

# Edición y control

# 

# Modificación individual de facturas desde el DataGridView.

# 

# Implementación de modificación múltiple sobre facturas seleccionadas.

# 

# Recalculo automático de importe abonable cuando corresponde.

# 

# Corrección en la lógica de cálculo para evitar errores con saldos negativos.

# 

# Informes

# 

# Generación de informes estructurados por período.

# 

# Reestructuración del algoritmo para contemplar división por períodos.

# 

# Exportaciones a Excel mediante plantillas.

# 

# Exportador específico para Edenor.

# 

# Nota: actualmente existe una limitación en el cotejo de múltiples carpetas de distintos años cuando los períodos se superponen. Se encuentra pendiente una mejora en el algoritmo de validación.

# 

# Arquitectura general

# 

# El proyecto sigue una estructura modular basada en separación de responsabilidades:

# 

# Forms: Interfaz gráfica y eventos.

# 

# Clases: Modelos de dominio (Factura y entidades asociadas).

# 

# Servicios / Procesadores: Lógica de negocio y procesamiento de archivos.

# 

# Controladores: Coordinación entre UI y modelo.

# 

# Assets: Plantillas Excel y recursos estáticos.

# 

# La lógica de modificación de entidades se encuentra centralizada en el controlador, utilizando reflexión para permitir edición genérica de propiedades sin acoplar la UI al modelo.

# 

# Tecnologías

# 

# C#

# 

# .NET Desktop (Windows Forms)

# 

# OpenXML para exportación a Excel

# 

# Expresiones Regulares (Regex) para parseo de texto

# 

# CultureInfo controlado para manejo consistente de importes y fechas

# 

# Cómo ejecutar

# 

# Clonar el repositorio:

# git clone https://github.com/Ozhammar/Programa-Servicios-Basicos.git

# 

# Abrir la solución en Visual Studio.

# 

# Verificar que el proyecto apunte a una versión compatible de .NET Desktop.

# 

# Restaurar dependencias si fuese necesario.

# 

# Compilar y ejecutar.

# 

# Estado del proyecto

# 

# En desarrollo activo.

# 

# Actualmente el foco está en:

# 

# Mejorar robustez del algoritmo de informes.

# 

# Optimizar separación de responsabilidades.

# 

# Reducir duplicación de lógica.

# 

# Preparar la base para futura persistencia en base de datos.

# 

# Próximas mejoras

# 

# Persistencia en base de datos.

# 

# Mejor manejo de superposición de períodos entre años.

# 

# Filtros y búsquedas avanzadas.

# 

# Reportes gráficos.

# 

# Instalador para distribución.

# 

# Autor

# 

# Lucas Povolo

# https://github.com/Ozhammar

