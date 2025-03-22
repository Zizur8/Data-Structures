using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class CoffeeShop: IEquatable<CoffeeShop>, IComparable<CoffeeShop>
    {
        //static int intCountID = 0;
        public CoffeeShop()
        {
            _intID = 0;
            _strName = "";
            _strPathImageOfCoffeeShop = "";
            _strTypeCoffee = "";
        }

        
        //public  delegate int ComparableAttribute(CoffeeShop x, CoffeeShop y);
        private int _intID;

        public int ID
        {
            get { return _intID; }
            set { _intID = value; }
        }

        private string _strName;

        public string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }

        private double _dblCapital;

        public double Capital
        {
            get { return _dblCapital; }
            set { _dblCapital = value; }
        }

        private bool _blnServersAlcohol;

        public bool ServersAlcohol
        {
            get { return _blnServersAlcohol; }
            set { _blnServersAlcohol = value; }
        }

        private TimeOnly _timeOpeningTime;

        public TimeOnly OpeningTime
        {
            get { return _timeOpeningTime; }
            set { _timeOpeningTime = value; }
        }
        private TimeOnly _timeClosingTime;

        public TimeOnly ClosingTime
        {
            get { return _timeClosingTime; }
            set { _timeClosingTime = value; }
        }

        private char _chrClassification;

        public char Classification
        {
            get { return _chrClassification; }
            set { _chrClassification = value; }
        }

        private string _strPathImageOfCoffeeShop;

        public string PathImageOfCoffeeShop
        {
            get { return _strPathImageOfCoffeeShop; }
            set { _strPathImageOfCoffeeShop = value; }
        }
        
        private Image? _imgPhotoOfCoffeeShop;

        public Image? PhotoOfCoffeeShop
        {
            get { return _imgPhotoOfCoffeeShop; }
            set { _imgPhotoOfCoffeeShop = value; }
        }

        private string _strTypeCoffee;  

        public string TypeCoffee
        {
            get { return _strTypeCoffee; }
            set { _strTypeCoffee = value; }
        }

        private DateTime _dtmArrivalTime;

        public DateTime ArrivalTime
        {
            get { return _dtmArrivalTime; }
            set { _dtmArrivalTime = value; }
        }

        //private DateTime _dtmDepartureTime;

        //public DateTime DepartureTime
        //{
        //    get { return _dtmDepartureTime; }
        //    set { _dtmDepartureTime = value; }
        //}

        public TimeSpan WaitingTime()
        {
            return  (DateTime.Now - _dtmArrivalTime);
        }

        public override string ToString()
        {

            return ID.ToString();

        }
        public string DataCoffeeShop()
        {
            return $"ID: {ID}\nCoffee Shop: {Name}\nCapital: {Capital.ToString("C")}\nServersAlcohol: {((ServersAlcohol) ? "Yes" : "No")}\nClassification: {Classification}\nOpening: {OpeningTime}\nClosing: {ClosingTime}\nPath Photo: {PathImageOfCoffeeShop}\nType Coffee: {TypeCoffee}";

        }

        public bool Equals(CoffeeShop? otherCoffeeShop)
        {
            if(otherCoffeeShop == null) {  return false; }
            return ID == otherCoffeeShop!.ID;//&& Name == otherCoffeeShop.Name && Capital == otherCoffeeShop.Capital && ServersAlcohol == otherCoffeeShop.ServersAlcohol && Classification == otherCoffeeShop.Classification && OpeningTime == otherCoffeeShop.OpeningTime && ClosingTime == otherCoffeeShop.ClosingTime && PathImageOfCoffeeShop == otherCoffeeShop.PathImageOfCoffeeShop && TypeCoffee == otherCoffeeShop.TypeCoffee;
        }

        public int CompareTo(CoffeeShop? otherCoffeeShop)
        {
            if (otherCoffeeShop == null) { return 0; }
            return ID.CompareTo(otherCoffeeShop.ID);
        }
    }
}
