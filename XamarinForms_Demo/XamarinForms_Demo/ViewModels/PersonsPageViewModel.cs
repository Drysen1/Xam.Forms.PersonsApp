using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_Demo.Models;
using XamarinForms_Demo.Services;

namespace XamarinForms_Demo.ViewModels
{
    public class PersonsPageViewModel : INotifyPropertyChanged
    {
        private PersonServices pService;
        private List<PersonModel> personList = new List<PersonModel>();

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

        //Property and field for an obserable collection / List for personmodel objects.
        private ObservableCollection<PersonModel> _personsObservableList = new ObservableCollection<PersonModel>();
        public ObservableCollection<PersonModel> PersonsObservableList
        {
            get
            { return _personsObservableList; }
            set
            {
                _personsObservableList = value;
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

        //Field and property to be able to prompt the user with a message.
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

        //IsPopUpEditShowing
        private bool _isPopUpEditShowing;
        public bool IsPopUpEditShowing
        {
            get { return _isPopUpEditShowing; }

            private set
            {
                if (_isPopUpEditShowing != value)
                {
                    _isPopUpEditShowing = value;
                    OnPropertyChanged();
                }
            }
        }

        //IsPopUpMessageBoxShowing
        private bool _isPopUpMessageBoxShowing;
        public bool IsPopUpMessageBoxShowing
        {
            get { return _isPopUpMessageBoxShowing; }

            private set
            {
                if (_isPopUpMessageBoxShowing != value)
                {
                    _isPopUpMessageBoxShowing = value;
                    OnPropertyChanged();
                }
            }
        }

        //btnLoadList_Command
        public ICommand btnLoadList_Command
        {
            get
            {
                return new Command( async() => {                   
                    PersonsObservableList = await GetAllPersons();
                });
            }
        }

        //btnSaveChanges_Command
        public ICommand btnSaveChanges_Command
        {
            get
            {
                return new Command(async () => {
                    if(!ValidatePersonValues())
                    {
                        Message = "All fields are required!";
                        IsPopUpMessageBoxShowing = true;
                    }
                    else
                    {
                        IsLoading = true;
                        bool ok = await pService.UpdatePersonAsync(Person);
                        if (ok)
                        {
                            IsLoading = false;
                            IsPopUpEditShowing = false;
                            IsPopUpMessageBoxShowing = true;
                            Message = "Person saved!";
                            PersonsObservableList = await GetAllPersons(); //update list with new values.
                        }
                        else
                        {
                            IsLoading = false;
                            IsPopUpMessageBoxShowing = true;
                            Message = "Something went wrong. Try again later.";
                        }
                    }
                });
            }
        }

        //btnDelete_Command
        public ICommand btnDelete_Command
        {
            get
            {
                return new Command(async () => {

                    IsLoading = true;
                    int idPerson = Person.IDPerson;
                    bool ok = await pService.DeletePersonAsync(idPerson);

                    if (ok)
                    {
                        IsLoading = false;
                        IsPopUpEditShowing = false;
                        IsPopUpMessageBoxShowing = true;
                        Message = "Person Deleted!";
                        PersonsObservableList = await GetAllPersons(); //update list with new values.
                    }
                    else
                    {
                        IsLoading = false;
                        IsPopUpMessageBoxShowing = true;
                        Message = "Something went wrong. Try again later." + idPerson.ToString();
                    }
                });
            }
        }

        //btnClosPopUps
        public ICommand btnClosPopUps
        {
            get
            {
                return new Command(() => {
                    IsPopUpEditShowing = false;
                });
            }
        }

        //btnOK
        public ICommand btnOK
        {
            get
            {
                return new Command(() => {                   
                    IsPopUpMessageBoxShowing = false;
                });
            }
        }

        /// <summary>
        /// This method retrieves all the persons that are currently in the persons database. 
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<PersonModel>> GetAllPersons()
        {
            IsLoading = true;
            personList = await pService.GetAllPersonsAsync();

            if(personList != null || personList.Count > 0)
            {
                ObservableCollection<PersonModel> tempList = new ObservableCollection<PersonModel>(personList);
                IsLoading = false;
                return tempList;
            }
            else
            {
                ObservableCollection<PersonModel> tempList = new ObservableCollection<PersonModel>();
                tempList = null;
                IsLoading = false;
                return tempList;
            }                      
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PersonsPageViewModel()
        {
            pService = new PersonServices();
            IsLoading = false;
        }

        //Validates the model so that there is no empty values.
        private bool ValidatePersonValues()
        {
            bool ok = false;
            ok = !string.IsNullOrEmpty(Person.FirstName) &&
                !string.IsNullOrEmpty(Person.LastName) &&
                !string.IsNullOrEmpty(Person.Age.ToString());
            return ok;
        }

        //Command for when user presses item in persons listview. 
        private Command _itemSelectedCommand;
        public ICommand ItemSelectedCommand
        {
            get
            {
                if (_itemSelectedCommand == null)
                {
                    _itemSelectedCommand = new Command(HandleItemSelected);
                }

                return _itemSelectedCommand;
            }
        }

        //This method executes when a user has pressed something in the persons listview. 
        private void HandleItemSelected(object parameter)
        {
            PersonModel person = parameter as PersonModel;

            Person = person;
            IsPopUpEditShowing = true;
        }

        //Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
