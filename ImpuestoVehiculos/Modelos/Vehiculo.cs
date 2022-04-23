using System;

public enum TipoPlaca
{
    Particular,
    Moto,
    Camion
}

namespace ImpuestoVehiculos.Modelos
{
    public class Vehiculo
    {
        private TipoPlaca tipoPlaca;
        private string marca;
        private string modelo;
        private int anio;
        private double precio;

        public void SetLine(string[] value) {

            this.tipoPlaca = getTipoPlaca(value[0]);
            this.marca = value[1];
            this.modelo = value[2];
            this.anio = int.Parse(value[3]);
            this.precio = double.Parse(value[4]);
        }

        public TipoPlaca GetTipoPlaca()
        {
            return tipoPlaca;
        }

        public void SetTipoPlaca(TipoPlaca tipoPlaca)
        {
            this.tipoPlaca = tipoPlaca;
        }

        public string GetMarca()
        {
            return marca;
        }

        public void SetMarca(string marca)
        {
            this.marca = marca;
        }

        public string GetModelo()
        {
            return modelo;
        }

        public void SetModelo(string modelo)
        {
            this.modelo = modelo;
        }

        public int GetAnio()
        {
            return anio;
        }

        public void SetAnio(int anio)
        {
            this.anio = anio;
        }

        public double GetPrecio()
        {
            return precio;
        }

        public void SetPrecio(double precio)
        {
            this.precio = precio;
        }

        public double GetTarifa()
        {
            if(tipoPlaca == TipoPlaca.Moto) {

                return precio * 0.015;
            }

            if(tipoPlaca == TipoPlaca.Camion) {

                return precio * 0.005;
            }

            if(tipoPlaca == TipoPlaca.Particular)
            {
                if(precio >=0 && precio < 16000)
                {
                    return precio * 0.015;
                }
                else if (precio >= 16000 && precio < 50000)
                {
                    return precio * 0.025;
                }
                else if (precio >= 50000)
                {
                    return precio * 0.035;
                }
            }

            return 0;
        }

        public TipoPlaca getTipoPlaca(string tipoPlaca)
        {
            if (tipoPlaca.ToLower() == "particular")
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

        public string getLine()
        {
            return tipoPlaca.ToString() + ","+ marca +","+modelo +","+anio + ","+precio;
        }
    }
}
