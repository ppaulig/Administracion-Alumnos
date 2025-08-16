using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using static Administracion_Alumnos.Program;

namespace Administracion_Alumnos
{
    //VERSION NUEVA MEJORADA
    // TODO:
    // testing
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

        static string SerializarAlumno(Alumnos alumno) =>
            $"{alumno.indice},{alumno.nombre},{alumno.apellido},{alumno.dni},{alumno.fecha},{alumno.domicilio},{alumno.activo}";

        static string SerializarMateria(Materias materia) =>
            $"{materia.indice},{materia.nombre},{materia.activa}";

        static string SerializarAlumnoMateria(Alumno_Materias am) =>
            $"{am.indice},{am.indiceAlumno},{am.indiceMateria},{am.estado},{am.nota},{am.fecha}";


        // VARIABLES

        public static List<Alumnos> alumnos = new List<Alumnos>();

        public static List<Materias> materias = new List<Materias>();

        public static List<Alumno_Materias> alumno_materias = new List<Alumno_Materias>();

        public static string archivo_alumnos = "Archivos/Alumnos.txt";


        public static string archivo_alumno_materias = "Archivos/Alumno_Materias.txt";


        public static string archivo_materias = "Archivos/Materias.txt";

        public static int ultimoIdAlumno = 0;

        public static int ultimoIdMateria = 0;

        public static int ultimoIdAlumnoMateria = 0;



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

        static int Validar_Dni(string mensaje) // agregar que ese dni no exista ya
        {
            Console.Write(mensaje);
            int num = 0;
            bool esint = false;
            string entrada = "";
            while (!esint)
            {
                entrada = Console.ReadLine();
                if ((int.TryParse(entrada, out num)) && ((num > 9999999) && (num < 100000000)))
                {
                    esint = true;
                }
                else
                {

                    Console.WriteLine("El dato ingresado no es un número DNI valido, intente de nuevo: ");
                }
            }
            return num;
        }

        static string siOno(string mensaje)
        {
            string entrada = "";
            Console.Write(mensaje);
            bool esvalido = false;
            while (!esvalido)
            {
                entrada = Console.ReadLine();
                entrada = entrada.ToLower();
                if ((entrada != "si") && (entrada != "no"))
                {
                    Console.Write("Las opciones son si/no, intenta de nuevo: ");
                }
                else
                {
                    esvalido = true;
                }
            }
            return entrada;
        }

        static string Validar_Materia(string mensaje) // agregar que esa materia no exista ya
        {
            Console.WriteLine(mensaje);
            bool esvalida = false;
            int num = 0;
            string materia = "";
            while (!esvalida)
            {
                materia = Console.ReadLine();
                if (int.TryParse(materia, out num))
                {
                    Console.WriteLine("El nombre de una materia no puede ser un número, intente de nuevo: ");
                }
                else
                {
                    esvalida = true;
                }
            }
            return materia;
        }

        static double Validar_Nota_Final()
        {
            Console.Write("Ingrese la nota obtenida en el examen final: ");
            double nota = 0;
            string entrada = "";
            bool esvalida = false;
            while (!esvalida)
            {
                entrada = Console.ReadLine();
                if (!double.TryParse(entrada, out nota))
                {
                    Console.Write("El tipo de dato ingresado no corresponde a una nota numérica , intente de nuevo: ");
                }
                else
                {
                    if ((double.TryParse(entrada, out nota)) && ((nota >= 0) && (nota <= 10)))
                    {
                        esvalida = true;
                    }
                    else
                    {
                        Console.Write("La nota del examen no puede ser menor que 0 ni mayor que 10, intente de nuevo: ");
                    }
                }
            }
            return nota;
        }

        // ARCHIVOS

        static void Cargar_Alumnos() // Lee el archivo y guarda los alumnos en una lista.
        {
            if (!File.Exists(archivo_alumnos)) return;

            using (StreamReader lector = new StreamReader(archivo_alumnos))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    string[] campos = linea.Split(',');
                    Alumnos alumno = new Alumnos
                    {
                        indice = int.Parse(campos[0]),
                        nombre = campos[1],
                        apellido = campos[2],
                        dni = int.Parse(campos[3]),
                        fecha = campos[4],
                        domicilio = campos[5],
                        activo = bool.Parse(campos[6])
                    };
                    alumnos.Add(alumno);

                    if (alumno.indice > ultimoIdAlumno)
                        ultimoIdAlumno = alumno.indice;
                }
            }
        }

        static void Cargar_Materias() // Lee el archivo y guarda las materias en una lista.
        {
            if (!File.Exists(archivo_materias)) return;

            using (StreamReader lector = new StreamReader(archivo_materias))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    string[] campos = linea.Split(',');
                    Materias materia = new Materias
                    {
                        indice = int.Parse(campos[0]),
                        nombre = campos[1],
                        activa = bool.Parse(campos[2])
                    };
                    materias.Add(materia);

                    if (materia.indice > ultimoIdMateria)
                        ultimoIdMateria = materia.indice;
                }
            }
        }

        static void Cargar_Alumno_Materias() // Lee el archivo y guarda las notas de los alumnos en una lista.
        {
            if (!File.Exists(archivo_alumno_materias)) return;

            using (StreamReader lector = new StreamReader(archivo_alumno_materias))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    string[] campos = linea.Split(',');
                    Alumno_Materias alumnoMateria = new Alumno_Materias
                    {
                        indice = int.Parse(campos[0]),
                        indiceAlumno = int.Parse(campos[1]),
                        indiceMateria = int.Parse(campos[2]),
                        estado = campos[3],
                        nota = double.Parse(campos[4]),
                        fecha = campos[5]
                    };
                    alumno_materias.Add(alumnoMateria);

                    if (alumnoMateria.indice > ultimoIdAlumnoMateria)
                        ultimoIdAlumnoMateria = alumnoMateria.indice;
                }
            }
        }

        static void Guardar_Archivo<T>(string rutaArchivo, List<T> lista, Func<T, string> serializar)
        {
            using (StreamWriter escritor = new StreamWriter(rutaArchivo, false))
            {
                foreach (var item in lista)
                {
                    escritor.WriteLine(serializar(item));
                }
            }
        }



        // LOGICA DE NEGOCIO
        static void Alumnos_Activos() // Busca en la lista de alumnos lo que su propiedad activo sea true y los muestra uno por uno.
        {
            bool listavalida = false;
            Console.WriteLine("ALUMNOS ACTIVOS");
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].activo == true)
                {
                    listavalida = true;
                    Console.WriteLine($"{alumnos[i].indice};{alumnos[i].nombre};{alumnos[i].apellido};{alumnos[i].dni};{alumnos[i].fecha};{alumnos[i].domicilio};{alumnos[i].activo}");
                }
            }
            if (!listavalida)
            {
                Console.WriteLine("No se encontraron alumnos activos");
            }
        }

        static void Alumnos_Inactivos() // Busca en la lista de alumnos lo que su propiedad activo sea true y los muestra uno por uno.
        {
            
            bool listavalida = false;
            Console.WriteLine("ALUMNOS INACTIVOS");
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].activo == false)
                {
                    listavalida = true;
                    Console.WriteLine($"{alumnos[i].indice};{alumnos[i].nombre};{alumnos[i].apellido};{alumnos[i].dni};{alumnos[i].fecha};{alumnos[i].domicilio};{alumnos[i].activo}");
                }
            }
            if (!listavalida)
            {
                Console.WriteLine("No se encontraron alumnos inactivos");
            }
        }

        static void Alta_Alumno()
        {
            Alumnos nuevoAlumno = new Alumnos();
            ultimoIdAlumno++;
            nuevoAlumno.indice = ultimoIdAlumno;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Complete los datos del alumno solicitados a continuación ");
            Console.ResetColor();
            nuevoAlumno.nombre = Validar_Nombre("Nombre: ");
            nuevoAlumno.apellido = Validar_Nombre("Apellido: ");
            nuevoAlumno.dni = Validar_Dni("Dni: ");
            Console.Write("Fecha de nacimento: ");
            nuevoAlumno.fecha = Console.ReadLine();
            Console.Write("Domicilio: ");
            nuevoAlumno.domicilio = Console.ReadLine();
            nuevoAlumno.activo = true;
            bool existe = false;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if ((alumnos[i].dni == nuevoAlumno.dni) && (alumnos[i].activo == true))
                {
                    Console.WriteLine("El alumno que desea ingresar ya es un alumno activo");
                    existe = true;
                }
                else
                {
                    if ((alumnos[i].dni == nuevoAlumno.dni) && (alumnos[i].activo == false))
                    {
                        existe = true;
                        string respuesta = siOno("El alumno que desea dar de alta ya se encuentra ingresado, pero no activo. Desea activarlo? si/no: ");
                        if (respuesta == "si")
                        {
                            var alumnoActualizado = alumnos[i];
                            alumnoActualizado.activo = true;
                            alumnos[i] = alumnoActualizado;
                            Console.WriteLine("El alumno se actualizó con éxito");
                        }
                    }
                }
            }
            if (!existe)
            {
                alumnos.Add(nuevoAlumno);
                Console.WriteLine("Alumno agregado correctamente");
                Guardar_Archivo(archivo_alumnos, alumnos, SerializarAlumno);
            }
        }

        static void Baja_Alumno()
        {
            int dni = Validar_Dni("Ingrese el número dni del alumno que desea dar de baja: ");
            bool existe = false;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if ((alumnos[i].dni == dni) && (alumnos[i].activo == false))
                {
                    existe = true;
                    Console.WriteLine("El alumno que desea ingresar ya fue dado de baja");
                }
                else
                {
                    if ((alumnos[i].dni == dni) && (alumnos[i].activo == true))
                    {
                        existe = true;
                        var alumnoActualizado = alumnos[i];
                        alumnoActualizado.activo = false;
                        alumnos[i] = alumnoActualizado;
                        Console.WriteLine("El alumno se dió de baja con éxito");
                        Guardar_Archivo(archivo_alumnos, alumnos, SerializarAlumno);
                    }
                }
            }
            if (!existe)
            {
                Console.WriteLine("El alumno que desea dar de baja no existe");
            }
        }

        static void Modificar_Alumno()
        {
            int dni = Validar_Dni("Ingrese el dni del alumno que desea modificar: ");
            int opcion = Opcion_Modificar_Alumno();
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].dni != dni)
                {
                    Console.WriteLine("El dni ingresado no corresponde a un alumno existente");
                }
                else
                {
                    if (opcion == 1)
                    {
                        string nuevoNombre = Validar_Nombre("Ingrese el nuevo nombre del alumno: ");
                        var alumnoActualizado = alumnos[i];
                        alumnoActualizado.nombre = nuevoNombre;
                        alumnos[i] = alumnoActualizado;
                        Console.WriteLine("El alumno se actualizó con éxito");
                    }
                    else
                    {
                        if (opcion == 2)
                        {
                            string nuevoApellido = Validar_Nombre("Ingrese el nuevo apellido del alumno: ");
                            var alumnoActualizado = alumnos[i];
                            alumnoActualizado.apellido = nuevoApellido;
                            alumnos[i] = alumnoActualizado;
                            Console.WriteLine("El alumno se actualizó con éxito");
                        }
                        else
                        {
                            if (opcion == 3)
                            {
                                int nuevoDni = Validar_Dni("Ingrese el nuevo dni del alumno: ");
                                var alumnoActualizado = alumnos[i];
                                alumnoActualizado.dni = nuevoDni;
                                alumnos[i] = alumnoActualizado;
                                Console.WriteLine("El alumno se actualizó con éxito");
                            }
                            else
                            {
                                if (opcion == 4)
                                {
                                    Console.Write("Ingrese la nueva fecha: ");
                                    string nuevaFecha = Console.ReadLine();
                                    var alumnoActualizado = alumnos[i];
                                    alumnoActualizado.fecha = nuevaFecha;
                                    alumnos[i] = alumnoActualizado;
                                    Console.WriteLine("El alumno se actualizó con éxito");
                                }
                                else
                                {
                                    if (opcion == 5)
                                    {
                                        Console.Write("Ingrese el nuevo domicilio: ");
                                        string nuevoDomicilio = Console.ReadLine();
                                        var alumnoActualizado = alumnos[i];
                                        alumnoActualizado.domicilio = nuevoDomicilio;
                                        alumnos[i] = alumnoActualizado;
                                        Console.WriteLine("El alumno se actualizó con éxito");
                                    }
                                }
                            }
                        }
                    }
                    Guardar_Archivo(archivo_alumnos, alumnos, SerializarAlumno);
                }
            }
        }

        static void Alta_Materia()
        {
            Materias materia = new Materias();
            ultimoIdMateria++;
            materia.indice = ultimoIdMateria;
            materia.nombre = Validar_Materia("Ingrese el nombre de la materia que desea dar de alta: ");
            materia.activa = true;
            bool existe = false;
            for (int i = 0; i < materias.Count; i++)
            {
                string nombreExistente = (materias[i].nombre).ToLower();
                string nuevoNombre = (materia.nombre).ToLower();

                if ((nombreExistente == nuevoNombre) && (materias[i].activa == true))
                {
                    Console.WriteLine("La materia que desea ingresar ya es una materia activa");
                    existe = true;
                }
                else
                {
                    if ((nombreExistente == nuevoNombre) && (materias   [i].activa == false))
                    {
                        existe = true;
                        string respuesta = siOno("La materia que desea dar de alta ya se encuentra ingresada pero no activa, desea activarla? si/no: ");
                        if (respuesta == "si")
                        {
                            var materiaActualizada = materias[i];
                            materiaActualizada.activa = true;
                            materias[i] = materiaActualizada;
                            Console.WriteLine("La materia se actualizó correctamente");
                        }
                    }
                }
            }
            if (!existe)
            {
                materias.Add(materia);
                Console.WriteLine("Materia agregada con éxito");
                Guardar_Archivo(archivo_materias, materias, SerializarMateria);
            }
        }

        static void Baja_Materia()
        {
            string materia = Validar_Materia("Ingrese el nombre de la materia que desea dar de baja: ");
            bool existe = false;
            for (int i = 0; i < materias.Count; i++)
            {
                string materiaExistente = (materias[i].nombre).ToLower();
                materia = materia.ToLower();

                if ((materia == materiaExistente) && (materias[i].activa == false))
                {
                    Console.WriteLine("La materia que desea ingresar ya está dada de baja");
                    existe = true;
                }
                else
                {
                    if ((materia == materiaExistente) && (materias[i].activa == true))
                    {
                        existe = true;
                        var materiaActualizada = materias[i];
                        materiaActualizada.activa = false;
                        materias[i] = materiaActualizada;
                        Console.WriteLine("La materia se dió de baja correctamente");
                        Guardar_Archivo(archivo_materias, materias, SerializarMateria);
                    }
                }
            }
            if (!existe)
            {
                Console.WriteLine("La materia que desea dar de baja no existe");
            }
        }

        static int Convertir_Int(string entrada)
        {
            int dni = 0;
            bool esdni = false;
            do
            {
                if (int.TryParse(entrada, out dni))
                {
                    esdni = true;
                }
                else
                {
                    Console.Write("El dato ingresado no corresponde a un dni, intente de nuevo: ");
                    entrada = Console.ReadLine();
                }
            } while (!esdni);
            return dni;
        }

        static void Modificar_Materia()
        {
            string materiaAmodificar = Validar_Materia("Ingrese el nombre de la materia que desea modificar: ");
            for (int i = 0; i < materias.Count; i++)
            {
                string materiaExistente = (materias[i].nombre).ToLower();
     
                if (materiaExistente != materiaAmodificar)
                {
                    Console.WriteLine("La materia que desea modificar no existe");
                }
                else
                {
                    string nuevaMateria = Validar_Materia("Ingrese el nuevo nombre de la materia: ");
                    var materiaActualizada = materias[i];
                    materiaActualizada.nombre = nuevaMateria;
                    materias[i] = materiaActualizada;
                    Console.WriteLine("La materia se modificó correctamente");
                    Guardar_Archivo(archivo_materias, materias, SerializarMateria);
                }
            }
        }

        static int Indice_Materia(string mensaje)
        {
            Console.Write(mensaje);
            string materia = "";
            bool esindice = false;
            int indice = 0;
            while (!esindice)
            {
                materia = Console.ReadLine();
                materia = materia.ToLower();
                for (int i = 0; i < materias.Count; i++)
                {
                    string materiaexist = materias[i].nombre;
                    materiaexist = materiaexist.ToLower();
                    if (materiaexist == materia)
                    {
                        indice = materias[i].indice;
                        esindice = true;
                    }
                    else
                    {
                        Console.Write("La materia ingresada no existe, intente de nuevo: ");

                    }
                }
            }
            return indice;
        }

        static int Indice_Alumno(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = "";
            bool esindice = false;
            int dni = 0;
            int indice = 0;
            while (!esindice)
            {
                entrada = Console.ReadLine();
                dni = Convertir_Int(entrada);

                for (int i = 0; i < alumnos.Count; i++)
                {
                    if (alumnos[i].dni == dni)
                    {
                        indice = alumnos[i].indice;
                        esindice = true;
                    }
                    else
                    {
                        Console.Write("El dni ingresado no existe, intente de nuevo: ");

                    }
                }
            }
            return indice;
        }

        static void Nota_Alumno()
        {
            int indiceMateria = Indice_Materia("Ingrese el nombre de la materia que cursa el alumno: ");
            int indiceAlumno = Indice_Alumno("Ingrese el dni del alumno: ");
            string fechaFinal = "-";
            string estado = "-";
            string rindio = siOno("El alumno rindió el examen final? si/no: ");
            double notaFinal = 00;
            if (rindio == "si")
            {
                notaFinal = Validar_Nota_Final();
                Console.Write("Ingrese la fecha en la que rindió del examen final: ");
                fechaFinal = Console.ReadLine();
                if (notaFinal >= 6)
                {
                    estado = "Aprobado";
                }
                else
                {
                    estado = "Desaprobado";
                }
            }
            else
            {
                string siOnoFecha = siOno("¿Sabe la fecha del examen final?");
                if (siOnoFecha == "si")
                {
                    Console.Write("Ingrese la fecha del examen final: ");
                    fechaFinal = Console.ReadLine();
                    estado = "Anotado";
                }
            }
            Alumno_Materias notasAlumno = new Alumno_Materias();
            ultimoIdAlumnoMateria++;
            notasAlumno.indice = ultimoIdAlumnoMateria;
            notasAlumno.indiceAlumno = indiceAlumno;
            notasAlumno.indiceMateria = indiceMateria;
            notasAlumno.estado = estado;
            notasAlumno.nota = notaFinal;
            notasAlumno.fecha = fechaFinal;
            alumno_materias.Add(notasAlumno);
            Console.WriteLine("Registro del alumno agregado con exito ");
            Guardar_Archivo(archivo_alumno_materias, alumno_materias, SerializarAlumnoMateria);
        }

        static void Leer_Notas()
        {
            if (alumno_materias.Count == 0)
            {
                Console.WriteLine("No hay notas registradas.");
                return;
            }
            Console.WriteLine("Notas de los alumnos:");
            foreach (var registro in alumno_materias)
            {
                Console.WriteLine($"Indice: {registro.indice}, Indice-Alumno: {registro.indiceAlumno}, Indice-Materia: {registro.indiceMateria}, Estado: {registro.estado}, Nota: {registro.nota}, Fecha: {registro.fecha}");
            }
        }


        static void Main(string[] args)
        { 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("¡BIENVENIDO/A AL PROGRAMA DE ADMINISTRACIÓN DE ALUMNOS!");
            Console.ResetColor();
            Cargar_Alumnos();
            Cargar_Materias();
            Cargar_Alumno_Materias();
            int opcion1, opcion2, opcion3, opcion4;
            do
            {
                Console.WriteLine();
                opcion1 = Opcion_Principal();
                if (opcion1 == 1)// Sector alumnos
                {
                    do
                    {
                        Console.WriteLine();
                        opcion2 = Opcion_Alumnos();
                        if (opcion2 == 1) 
                        {
                            Alta_Alumno();
                        }
                        else
                        {
                            if (opcion2 == 2)
                            {
                                Baja_Alumno();
                            }
                            else
                            {
                                if (opcion2 == 3)
                                {
                                    Modificar_Alumno();
                                }
                                else
                                {
                                    if (opcion2 == 4)
                                    {
                                        Alumnos_Activos();
                                    }
                                    else
                                    {
                                        if (opcion2 == 5)
                                        {
                                            Alumnos_Inactivos();
                                        }
                                    }
                                }
                            }
                        }
                    } while (opcion2 != 6);
                }
                else
                {
                    if (opcion1 == 2)// Sector materias
                    {
                        do
                        {
                            Console.WriteLine();
                            opcion3 = Opcion_Materias();
                            if (opcion3 == 1)
                            {
                                Alta_Materia();
                            }
                            else
                            {
                                if (opcion3 == 2)
                                {
                                    Baja_Materia();
                                }
                                else
                                {
                                    if (opcion3 == 3)
                                    {
                                        Modificar_Materia();
                                    }
                                }
                            }
                        } while (opcion3 != 4);
                    }
                    else
                    {
                        if (opcion1 == 3)// Sector notas de alumnos
                        {
                            do
                            {
                                Console.WriteLine();
                                opcion4 = Opcion_Notas();
                                if (opcion4 == 1)
                                {
                                    Nota_Alumno();
                                }
                                else
                                {
                                    if (opcion4 == 2)
                                    {
                                        Leer_Notas();
                                    }
                                }
                            } while (opcion4 != 3);
                        }

                    }
                }
            } while (opcion1 != 4);
        }
    
    }
}
