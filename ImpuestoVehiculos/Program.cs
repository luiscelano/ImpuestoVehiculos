using System;
using System.Collections.Generic;
using System.IO;
namespace ImpuestoVehiculos
{
    class Program
    {
        public static string filename = "\\vehiculos.txt";
        public static string directory = Directory.GetCurrentDirectory();
        public static string path = directory + filename;
        public static Acciones.AccionesVehiculo accionesVehiculo = new Acciones.AccionesVehiculo();

        static void Main(string[] args)
        {
            int opt = 0;

            do
            {
                Console.Clear();
                Menu();
                opt = int.Parse(Console.ReadLine());
                switch(opt)
                {
                    case 1:
                        CrearVehiculo();
                        Console.ReadKey();
                        break;
                    case 2:
                        VerImpuestosYVehiculos();
                        Console.ReadKey();
                        break;
                    case 3:
                        ActualizarPrecioVehiculo();
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Ingrese la marca, modelo o anio del vehiculo a modificar: ");
                        string search = Console.ReadLine();
                        RemoverVehiculo(search);
                        Console.ReadKey();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Seleccione una opcion correcta");
                        Console.ReadKey();
                        break;
                }
            } while (opt != 5);
        }

        static void Menu()
        {
            Console.WriteLine("Control de vehiculos");
            Console.WriteLine("Opción 1: Registrar un vehiculo");
            Console.WriteLine("Opción 2: Consultar impuestos");
            Console.WriteLine("Opción 3: Modificar precio(Marca, modelo, anio)");
            Console.WriteLine("Opción 4: Eliminar vehiculo");
            Console.WriteLine("Opción 5: Salir");
            Console.WriteLine("Seleccione una opción");
        }

        static void ValidarArchivo()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(path))
            {
                FileStream fileStream = File.Create(path);
                fileStream.Close();
            }
        }

        static void CrearVehiculo()
        {
            try
            {
                Modelos.Vehiculo vehiculo = accionesVehiculo.RegistrarVehiculo();
                ValidarArchivo();

                StreamWriter stream = File.AppendText(path);

                stream.WriteLine(vehiculo.getLine());
                stream.Close();
                Console.WriteLine("Vehiculo registrado!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void VerImpuestosYVehiculos()
        {
            try {
                string[] lines = File.ReadAllLines(path);

                List<Modelos.Vehiculo> vehiculos = accionesVehiculo.ListadoVehiculos(lines);

                foreach (Modelos.Vehiculo vehiculo in vehiculos)
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Tipo de placa: " + vehiculo.GetTipoPlaca().ToString());
                    Console.WriteLine("Marca: " + vehiculo.GetMarca());
                    Console.WriteLine("Modelo: " + vehiculo.GetModelo());
                    Console.WriteLine("Anio: " + vehiculo.GetAnio().ToString());
                    Console.WriteLine("Precio: " + vehiculo.GetPrecio().ToString());
                    Console.WriteLine("Tarifa: " + vehiculo.GetTarifa().ToString());
                    Console.WriteLine("----------------------------------------");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ActualizarPrecioVehiculo()
        {
            try
            {
                Console.WriteLine("Ingrese la marca, modelo o anio del vehiculo a modificar: ");
                string search = Console.ReadLine();

                Console.WriteLine("Ingrese el precio del vehiculo: ");
                double precio = double.Parse(Console.ReadLine());

                string[] lines = File.ReadAllLines(path);

                string[] updatedLines = accionesVehiculo.ActualizarPrecioVehiculo(search, precio, lines);

                File.WriteAllLines(path, updatedLines);
                Console.WriteLine("Vehiculo actualizado!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void RemoverVehiculo(string search)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                bool match = false;
                List<Modelos.Vehiculo> vehiculos = new List<Modelos.Vehiculo>();

                foreach (string line in lines)
                {
                    Modelos.Vehiculo vehiculo = new Modelos.Vehiculo();
                    string[] value = line.Split(",");

                    vehiculo.SetLine(value);

                    if (search == vehiculo.GetMarca() || search == vehiculo.GetAnio().ToString() || search == vehiculo.GetModelo())
                    {
                        match = true;
                    }
                    else
                    {
                        vehiculos.Add(vehiculo);
                    }
                }

                Console.WriteLine(vehiculos.Count);

                if (!match) throw new Exception("Ningun elemento coincide con tu busqueda");

                File.WriteAllLines(path, ListToArray(vehiculos));
                Console.WriteLine("Vehiculo eliminado!");
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static string[] ListToArray(List<Modelos.Vehiculo> vehiculos)
        {
            List<string> lines = new List<string>();

            foreach(Modelos.Vehiculo vehiculo in vehiculos)
            {
                lines.Add(vehiculo.getLine());
            }

            return lines.ToArray();
        }
    }
}
