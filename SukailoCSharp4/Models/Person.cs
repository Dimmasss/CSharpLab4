using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SukailoCSharp4.Tools.Exeptions;

namespace SukailoCSharp4.Models
{
    public class Person : INotifyPropertyChanged
    {
  
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime _dateOfBirth;
        private int _age = 0;
        private bool _isAdult = false;
        private string _sunSign = "";
        private string _chineseSign = "";
        private bool _isBirthDay = false;
        private int _index = 0;

        public string FirstName
        {
            get {return _firstName;}
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        public string LastName
        {
            get {return _lastName;}
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        public string Email
        {
            get {return _email;}
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public bool IsAdult
        {
            get {return _isAdult;}
        }
        public string SunSign
        {
            get {return _sunSign;}
        }
        public string ChineseSign
        {
            get {return _chineseSign;}
        }

        public DateTime DateOfBirth
        {
            get {return _dateOfBirth;}
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    ReCalculate();

                    OnPropertyChanged(nameof(DateOfBirth));
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(SunSign));
                    OnPropertyChanged(nameof(ChineseSign));
                    OnPropertyChanged(nameof(IsAdult));
                    OnPropertyChanged(nameof(IsBirthDay));
                }
            }
        }

        public bool IsBirthDay
        {
            get {return _isBirthDay;}
        }
        public int Index
        {
            get {return _index;}
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                }
            }
        }
        public int Age
        {
            get {return _age;}
        }

        public bool CalculateIsBirthDay()
        {
            DateTime now = DateTime.Now;
            if (DateOfBirth.Date.Day == now.Date.Day && DateOfBirth.Date.Month == now.Date.Month) {return true;}
            return false;
        }

        public void IsValid()
        {
            IsValidDate();
            IsValidEmail();
        }

        private void IsValidEmail()
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);
            if (!regex.IsMatch(Email))
            {
                throw new InvalidEmailAddressException(" ");
            }
        }

        private void IsValidDate()
        {
            DateTime timeNow = DateTime.Now;
            _age = (int)((timeNow.Date - DateOfBirth.Date).TotalDays / 365.2425);
            if (DateOfBirth > timeNow.Date)
            {
                throw new DateOfBirthInTheFutureException(" ");
            }
            if (Age > 135)
            {
                throw new DateOfBirthTooFarInThePastException(" ");
            }
        }

        public void ReCalculate()
        {
            DateTime timeNow = DateTime.Now;
            _age = (int)((timeNow.Date - DateOfBirth.Date).TotalDays / 365.2425);
            _chineseSign = CalculateChineseSign();
            _sunSign = CalculateSunSign();
            _isAdult = CalculateIsAdult();
            _isBirthDay = CalculateIsBirthDay();
        }

        private bool CalculateIsAdult() { return Age >= 18; }

        public string CalculateSunSign()
        {
            int month = DateOfBirth.Month;
            int day = DateOfBirth.Day;

            switch (month)
            {
                case 1:
                    return day <= 19 ? "Capricorn" : "Aquarius";
                case 2:
                    return day <= 18 ? "Aquarius" : "Pisces";
                case 3:
                    return day <= 20 ? "Pisces" : "Aries";
                case 4:
                    return day <= 20 ? "Aries" : "Taurus";
                case 5:
                    return day <= 20 ? "Taurus" : "Gemini";
                case 6:
                    return day <= 20 ? "Gemini" : "Cancer";
                case 7:
                    return day <= 22 ? "Cancer" : "Leo";
                case 8:
                    return day <= 22 ? "Leo" : "Virgo";
                case 9:
                    return day <= 22 ? "Virgo" : "Libra";
                case 10:
                    return day <= 22 ? "Libra" : "Scorpio";
                case 11:
                    return day <= 21 ? "Scorpio" : "Sagittarius";
                default:
                    return day <= 21 ? "Sagittarius" : "Capricorn";
            }
        }

        public string CalculateChineseSign()
        {
            int year = DateOfBirth.Year;
            string[] chineseHoroscop = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

            return chineseHoroscop[year % 12];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
