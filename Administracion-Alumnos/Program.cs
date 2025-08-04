using System;
using System.Reflection.Metadata.Ecma335;
using static Administracion_Alumnos.Program;

namespace Administracion_Alumnos
{
    internal class Program
    {

    // STRUCTS - Definimos Structs que serian nuestros objetos con sus atributos
        public struct Alumnos
        {
            public int indice;
            public string nombre;
            public string apellido;
            public int dni;
            public string fecha;
            public string domicilio;
            public bool activo;
        }
        public struct Materias
        {
            public int indice;
            public string nombre;
            public bool activa;
        }
        public struct Alumno_Materias
        {
            public int indice;
            public int indiceAlumno;
            public int indiceMateria;
            public string estado; // "aprobado" - "desaprobado" - "en curso"
            public double nota; // nota del parcial
            public string fecha; // fecha del parcial
        }
       

    // MENUS - Definimos los menus para cada seccion.

        static void Menu_Principal()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("      ¿A QUE SECTOR DESEA INGRESAR?");
            Console.WriteLine("             1. Alumnos");
            Console.WriteLine("             2. Materias");
            Console.WriteLine("             3. Archivo de notas");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("  (Para salir del programa ingrese 4)");
            Console.WriteLine();
            Console.Write(": ");
            Console.ResetColor();
        }

        static void Menu_Alumnos()
        {
            //Se tiene que generar un menú que pueda ingresar alta, baja y modificación de alumno, mostrar alumnos activos, mostrar alumnos inactivos.

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         SECTOR ALUMNOS   ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("       ¿QUE DESEA HACER?");
            Console.WriteLine("   1. Dar de alta un alumno");
            Console.WriteLine("   2. Dar de baja un alumno");
            Console.WriteLine("   3. Modificar un alumno");
            Console.WriteLine("   4. Ver alumnos activos");
            Console.WriteLine("   5. Ver alumnos inactivos");
            Console.WriteLine("      6. Volver al inicio");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            Console.Write(": ");
            Console.ResetColor();
        }

        static void Menu_Materias()
        {
            //Se tiene que generar un menú que se pueda realizar alta, baja y modificación de las materias.

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         SECTOR MATERIAS   ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("       ¿QUE DESEA HACER?");
            Console.WriteLine("   1. Dar de alta una materia");
            Console.WriteLine("   2. Dar de baja una materia");
            Console.WriteLine("   3. Modificar una materia");
            Console.WriteLine("      4. Volver al inicio");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            Console.Write(": ");
            Console.ResetColor();
        }

        static void Menu_Notas()
        {
            // Alumno materias será el archivo encargado de mantener las notas, si la materia esta cursada y si la nota del final con la fecha. Habrá un menú para anotar al usuario.

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("           SECTOR NOTAS   ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("        ¿QUE DESEA HACER?");
            Console.WriteLine("  1. Agregar nota de un alumno");
            Console.WriteLine("         2. Leer el archivo");
            Console.WriteLine("        3. Volver al inicio");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            Console.Write(": ");
            Console.ResetColor();
        }

        static void Menu_Modificar_Alumno()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         ¿Que desea modificar?");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("           1. Nombre");
            Console.WriteLine("           2. Apellido");
            Console.WriteLine("           3. Dni");
            Console.WriteLine("           4. Feha de nacimiento");
            Console.WriteLine("           5. Domicilio");
            Console.WriteLine("--------------------------------------");
            Console.Write(": ");
            Console.ResetColor();
        }

    // OPCIONES MENU - Cuando entramos en cada menu tenemos un submenu con varias opciones. Nos encargamos de validar y capturar la opcion elegida por el usuario.

        static int Validar_Opcion(int primerOpcion, int ultimaOpcion) // Funcion para validar las opciones de cada menu/submenu.
        {
            int opcion = 0;
            bool esvalida = false;
            while (!esvalida)
            {
                string entrada = Console.ReadLine();
                if ((int.TryParse(entrada, out opcion)) && ((opcion >= primerOpcion) && (opcion <= ultimaOpcion)))
                {
                    esvalida = true;
                }
                else
                {
                    Console.WriteLine("La opción no es válida, intente de nuevo:");
                }
            }
            return opcion;
        }

        static int Opcion_Principal() // Desde Main llamamos al menu principal para arrancar el programa y que el usuario elija a que seccion entrar. (opMenu1)
        {
            Menu_Principal();
            int opcion = Validar_Opcion(1, 4);
            return opcion;
        }

        static int Opcion_Alumnos() // Desde el menu principal el usuario elige la opcion 1 'Alumnos'. Entramos en esa seccion y tomamos la opcion que elige el usuario. (opMenu2)
        {
            Menu_Alumnos();
            int opcion = Validar_Opcion(1, 6);
            return opcion;
        }

        static int Opcion_Materias() // Desde el menu principal el usuario elige la opcion 2 'Materias'. Entramos en esa seccion y tomamos la opcion que elige el usuario. (opMenuu3)
        {
            Menu_Materias();
            int opcion = Validar_Opcion(1, 4);
            return opcion;
        }

        static int Opcion_Notas() // Desde el menu principal el usuario elige la opcion 3 'Archivo de notas'. Entramos en esa seccion y tomamos la opcion que elige el usuario. (opMenu4)
        {
            Menu_Notas();
            int opcion = Validar_Opcion(1,3);
            return opcion;
        }

        static int Opcion_Modificar_Alumno() // Desde la seccion 'Alumnos' el usuario elige la opcion 3 'Modificar un alumno'. Entramos en esa seccion y tomamos la opcion que elige el usuario. (opMMenu3)
        { 
            Menu_Modificar_Alumno();
            int opcion = Validar_Opcion(1, 5);
            return opcion;
        }

        // VALIDACIONES DE ENTRADA
        static string Validar_Nombre(string mensaje) // El nombre o apellido no puede ser un numero. AGREGAR: NO DEBE SER VACIO ""
        {
            Console.Write(mensaje);
            bool esnombre = false;
            string entrada = "";
            while (!esnombre)
            {
                entrada = Console.ReadLine();
                if (int.TryParse(entrada, out int num))
                {
                    Console.Write("Un nombre o apellido no debe contener números, intente de nuevo: ");
                }
                else
                {
                    esnombre = true;
                }
            }
            return entrada;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
