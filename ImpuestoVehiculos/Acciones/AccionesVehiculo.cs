using System;
using System.Collections.Generic;

namespace ImpuestoVehiculos.Acciones
{
    public class AccionesVehiculo
    {
        public AccionesVehiculo()
        {
        }
        public Modelos.Vehiculo RegistrarVehiculo()
        {
            Modelos.Vehiculo vehiculo = new Modelos.Vehiculo();

            Console.WriteLine("Ingrese el tipo de placa");
            vehiculo.SetTipoPlaca(getTipoPlaca(Console.ReadLine()));
            Console.WriteLine("Ingrese marca del vehiculo:");
            vehiculo.SetMarca(Console.ReadLine());
            Console.WriteLine("Ingrese modelo del vehiculo:");
            vehiculo.SetModelo(Console.ReadLine());
            Console.WriteLine("Agregar anio de vehiculo:");
            vehiculo.SetAnio(int.Parse(Console.ReadLine()));
            Console.WriteLine("Ingrese precio: ");
            vehiculo.SetPrecio(double.Parse(Console.ReadLine()));

            return vehiculo;
        }

        public TipoPlaca getTipoPlaca(string tipoPlaca)
        {
            if(tipoPlaca.ToLower() == "particular")
            {
                return TipoPlaca.Particular;
            }
            if (tipoPlaca.ToLower() == "moto")
            {
                return TipoPlaca.Moto;
            }
            if (tipoPlaca.ToLower() == "camion")
            {
                return TipoPlaca.Camion;
            }

            return TipoPlaca.Particular;
        }

        public List<Modelos.Vehiculo> ListadoVehiculos(string []lines) {
            List<Modelos.Vehiculo> vehiculos = new List<Modelos.Vehiculo>();

            foreach(string line in lines)
            {
                Modelos.Vehiculo vehiculo = new Modelos.Vehiculo();
                string[] value = line.Split(",");
                vehiculo.SetLine(value);

                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }

        public string[] ActualizarPrecioVehiculo(string search, double nuevoPrecio, string[]lines)
        {
            bool match = false;

            for(int x=0; x< lines.Length; x++)
            {
                Modelos.Vehiculo vehiculo = new Modelos.Vehiculo();
                string[] value = lines[x].Split(",");
                vehiculo.SetLine(value);

                if(search == vehiculo.GetMarca() || search == vehiculo.GetAnio().ToString() || search == vehiculo.GetModelo())
                {
                    vehiculo.SetPrecio(nuevoPrecio);
                    lines[x] = vehiculo.getLine();
                    match = true;
                }
            }

            if (!match) throw new Exception("Ningun elemento coincide con tu busqueda");

            return lines;
        }
    }
}
