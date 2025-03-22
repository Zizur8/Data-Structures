using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleListTarea
{
    internal class Pasajero : IEquatable<Pasajero>
    {
		private string _strCurp = "";

		public string Curp
		{
			get { return _strCurp; }
			set { _strCurp = value; }
		}
		private int _intEdad;

		public int Edad
		{
			get { return _intEdad; }
			set { _intEdad = value; }
		}

		private string _strTipoDePasajero = "";

		public string TipoDePasajero
		{
			get { return _strTipoDePasajero; }
			set { _strTipoDePasajero = value; }
		}
		private int _intCantidadViajesRealizados;

		public int CantidadViajesRealizados
		{
			get { return _intCantidadViajesRealizados; }
			set { _intCantidadViajesRealizados = value; }
		}
		private string _strNombrePasajero = "";

		public string NombrePasajero
		{
			get { return _strNombrePasajero; }
			set { _strNombrePasajero = value; }
		}
		private DateTime _dtmHoraDeLlegada;

		public DateTime HoraDeLLegada
		{
			get { return _dtmHoraDeLlegada; }
			set { _dtmHoraDeLlegada = value; }
		}

        public bool Equals(Pasajero? otherPasajero)
        {
			if(otherPasajero == null) { return false; }
			return Curp == otherPasajero.Curp;

        }

    }
}
