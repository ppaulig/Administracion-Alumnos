# 📚 Sistema de Administración de Alumnos

Este proyecto es una aplicación de **consola en C#** que gestiona la administración de alumnos de una universidad.  
Fue desarrollado como **proyecto final de la materia Programación I**, y luego mejorado con nuevas validaciones, estructuras y organización.

---

## ⚙️ Lógica General del Programa

El sistema permite administrar:
- **Alumnos**
- **Materias**
- **Notas de alumnos**

Toda la información se maneja con **archivos de texto plano** (`.txt`), los cuales funcionan como base de datos simple para almacenar los datos.

### Estructuras (Structs)
El programa define tres estructuras principales:
- `Alumnos`: contiene índice, nombre, apellido, DNI, fecha de nacimiento, domicilio y estado (activo = true/false).
- `Materias`: contiene índice, nombre y estado (activa = true/false).
- `Alumno_Materias`: relaciona alumnos con materias y registra estado (Aprobado/Desaprobado/Anotado), nota final y fecha del examen.

### Manejo de Archivos
- **Archivos usados:**
  - `Archivos/Alumnos.txt`
  - `Archivos/Materias.txt`
  - `Archivos/Alumno_Materias.txt`
- Cada archivo se carga en memoria al iniciar el programa y se reescribe con los cambios al cerrarlo.

### Menús del Sistema
El programa funciona con un sistema de **menús interactivos**, donde cada entrada se valida para evitar errores:

1. **Menú Principal**
   - 1. Alumnos  
   - 2. Materias  
   - 3. Archivo de notas  
   - 4. Salir  

2. **Menú Alumnos**
   - Alta de alumno  
   - Baja de alumno  
   - Modificación de alumno  
   - Listado de alumnos activos  
   - Listado de alumnos inactivos
   - Volver al inicio

3. **Menú Materias**
   - Alta de materia  
   - Baja de materia  
   - Modificación de materia
   - Volver al inicio

4. **Menú Notas**
   - Registrar nota de un alumno  
   - Leer archivo de notas
   - Volver al inicio

### Validaciones
El programa incluye validaciones para:
- Entradas de texto (no vacías ni numéricas cuando no corresponde).  
- DNI (8 dígitos).  
- Respuestas de tipo **sí/no**.  
- Notas finales (números entre 0 y 10).  

---

## 🚀 Guía de Uso


