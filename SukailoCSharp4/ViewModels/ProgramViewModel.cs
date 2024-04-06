using GalaSoft.MvvmLight.Command;
using SukailoCSharp4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Serialization;

namespace SukailoCSharp4.ViewModels
{
    internal class ProgramViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _originalPersons;

        public ProgramViewModel()
        {
            _originalPersons = GenerateData();
        }

        public ObservableCollection<Person> OriginalPersons
        {
            get { return _originalPersons; }
            set
            {
                _originalPersons = value;
                NotifyPropertyChanged("OriginalPersons");
            }
        }



        private ObservableCollection<Person> GenerateData()
        {
            string[] names = { "Elias", "Felix", "Serena", "Valentina", "Cyrus", "Rafael", "Zara", "Mateo", "Leonidas" };
            string[] surnames = { "Anderson", "Petrov", "Gonzalez", "Dubois", "Ivanov", "Chen", "Lee", "Garcia", "Johnson", "Nguyen" };

            ObservableCollection<Person> userData = new ObservableCollection<Person>();
            for (int i = 0; i < 50; i++)
            {
                Person newPerson = new Person();
                newPerson.FirstName = names[RandomIndex(names.Length)];
                newPerson.LastName = surnames[RandomIndex(surnames.Length)];
                newPerson.Email = RandomEmail(new Random().Next(10) + 5);
                newPerson.DateOfBirth = RandomDateTime();
                newPerson.Index = i + 1;
                try
                {
                    newPerson.IsValid();
                    userData.Add(newPerson);
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            return userData;
        }

        private int RandomIndex(int arrayLenght)
        {
            var randomInx = new Random();
            return randomInx.Next(arrayLenght);
        }

        private DateTime RandomDateTime()
        {
            DateTime start = new DateTime(1901, 1, 1);
            var range = DateTime.Now.Date - start;
            var randomValue = new Random().Next((int)range.TotalDays);
            return start.AddDays(randomValue);
        }

        private string RandomEmail(int lenght)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var localPart = new string(Enumerable.Repeat(chars, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
            return $"{localPart}@ukma.edu.ua";
        }


        public ICommand SaveButton_Click => new RelayCommand(
        () =>
        {
            string fileName = "data.xml";
            SaveDataToFile(fileName);
        }
        );
        public ICommand LoadButton_Click => new RelayCommand(
        () =>
        {
            string fileName = "data.xml";
            LoadDataFromFile(fileName);
        }
        );




        private void SaveDataToFile(string fileName)
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(ObservableCollection<Person>));

            using (StreamWriter stWriter = new StreamWriter(fileName))
            {
                serilizer.Serialize(stWriter, _originalPersons);
            }
        }

        private void LoadDataFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                XmlSerializer serilizer = new XmlSerializer(typeof(ObservableCollection<Person>));
                ObservableCollection<Person> temporary = new ObservableCollection<Person>();

                using (StreamReader stReader = new StreamReader(fileName))
                {
                    temporary = (ObservableCollection<Person>)serilizer.Deserialize(stReader);

                }
                _originalPersons.Clear();
                foreach (Person person in temporary) {
                    _originalPersons.Add(person);
                }
            }
        }

































        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
