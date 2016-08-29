using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_Demo.Models;
using XamarinForms_Demo.Services;

namespace XamarinForms_Demo.ViewModels
{
    public class OnePersonViewModel : INotifyPropertyChanged
    {
        private PersonServices pService;
        //Property and field for Personmodel
        private PersonModel _person;
        public PersonModel Person
        {
            get
            { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        //Field and property to be able to hide or show loading screen.
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }

            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        //Field and property to be able to hide or show loading screen.
        private string _message;
        public string Message
        {
            get { return _message; }

            private set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public OnePersonViewModel()
        {
            pService = new PersonServices();
            Person = new PersonModel();
        }

        //btnLoadPerson_Command
        public ICommand btnLoadPerson_Command
        {
            get
            {
                return new Command( async() => {
                    
                    if (string.IsNullOrEmpty(Person.IDPerson.ToString()))
                    {
                        Message = "You have to enter a id!";
                    }
                    else
                    {
                        IsLoading = true;
                        Person = await pService.GetOnePerson(Person.IDPerson);
                        IsLoading = false;

                        if (Person == null)
                        {
                            Message = "No person with id " + Person.IDPerson.ToString() + " could be found.";
                        }
                        else
                        {
                            Message = "";
                        }
                    }
                });
            }
        }

        //Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
