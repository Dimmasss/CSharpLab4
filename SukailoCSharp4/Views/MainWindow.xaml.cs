using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using SukailoCSharp4.Models;
using SukailoCSharp4.ViewModels;

namespace SukailoCSharp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramViewModel _modelView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _modelView = new ProgramViewModel();
            table.ItemsSource = _modelView.OriginalPersons;
        }

        private void AddUserToTable(Person p)
        {
            p.Index = _modelView.OriginalPersons.Last().Index + 1;
            _modelView.OriginalPersons.Add(p);
            table.ItemsSource = _modelView.OriginalPersons;
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_modelView.OriginalPersons == null)
                return;

            string indexFilter = IndexFilter.Text.ToLower();
            string firstNameFilter = FirstNameFilter.Text.ToLower();
            string lastNameFilter = LastNameFilter.Text.ToLower();
            string emailFilter = EmailFilter.Text.ToLower();
            string ageFilter = AgeFilter.Text.ToLower();
            string isAdultFilter = IsAdultFilter.Text.ToLower();
            string sunSignFilter = SunSignFilter.Text.ToLower();
            string chineseSignFilter = ChineseSignFilter.Text.ToLower();
            string isBirthdayFilter = IsBirthdayFilter.Text.ToLower();

            var filteredList = _modelView.OriginalPersons.Where(person =>
                person.Index.ToString().Contains(indexFilter) &&
                person.FirstName.ToLower().Contains(firstNameFilter) &&
                person.LastName.ToLower().Contains(lastNameFilter) &&
                person.Email.ToLower().Contains(emailFilter) &&
                person.Age.ToString().Contains(ageFilter) &&
                person.IsAdult.ToString().ToLower().Contains(isAdultFilter) &&
                person.SunSign.ToLower().Contains(sunSignFilter) &&
                person.ChineseSign.ToLower().Contains(chineseSignFilter) &&
                person.IsBirthDay.ToString().ToLower().Contains(isBirthdayFilter)
            ).ToList();

            table.ItemsSource = new ObservableCollection<Person>(filteredList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstname = firstnameInput.Text;
            string lastname = lastnameInput.Text;
            string email = emailInput.Text;

            if (dateOfBirthPicker.SelectedDate == null || firstname == "" || lastname == "" || email == "")
            {
                output.Content = " ";
            }
            else
            {
                Person person = new Person();
                output.Content = "";
                DateTime dateOfBirth = (DateTime)dateOfBirthPicker.SelectedDate;

                person.FirstName = firstname;
                person.LastName = lastname;
                person.Email = email;
                person.DateOfBirth = dateOfBirth;

                try
                {
                    person.IsValid();

                    if (person.IsBirthDay)
                    {
                        MessageBox.Show("Happy Birthday!");
                    }

                    AddUserToTable(person);

                }
                catch (Exception ex)
                {
                    output.Content = ex.Message;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete this", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button button = (Button)sender;
                Person personToDelete = (Person)button.DataContext;

                _modelView.OriginalPersons.Remove(personToDelete);

                ObservableCollection<Person> persons = (ObservableCollection<Person>)table.ItemsSource;
                persons.Remove(personToDelete);
            }
        }
    }
}
