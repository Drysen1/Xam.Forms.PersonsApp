using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_Demo.Models;
using XamarinForms_Demo.Services;

namespace XamarinForms_Demo.ViewModels
{
    public class CreatePersonViewModel : INotifyPropertyChanged
    {
        private PersonServices pService;

        //Field and property for personmodel
        private PersonModel _person;
        public PersonModel Person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        //Constructor
        public CreatePersonViewModel()
        {
            Person = new PersonModel();
        }

        //Field 
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
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

        // btnAdd_Command
        public ICommand btnAdd_Command
        {
            get
            {
                return new Command(async() => {
                    if(!ValidatePersonValues())
                    {
                        Message = "All fields are required!";
                    }
                    else
                    {
                        pService = new PersonServices();
                        IsLoading = true;
                        bool ok = await pService.CreateNewPersonAsync(Person);
                        IsLoading = false;

                        if (ok)
                        {
                            Message = "Saved!";
                        }
                        else
                        {
                            Message = "Something went wrong try again";
                        }
                    }
                });
            }
        }

        //Validates so that nothing is empty.
        private bool ValidatePersonValues()
        {
            bool ok = false;
            ok = !string.IsNullOrEmpty(Person.FirstName) &&
                !string.IsNullOrEmpty(Person.LastName) &&
                !string.IsNullOrEmpty(Person.Age.ToString());
            return ok;
        }
        //Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
