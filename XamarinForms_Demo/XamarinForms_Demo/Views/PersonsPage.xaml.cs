using XamarinForms_Demo.ViewModels;
using Xamarin.Forms;
using System.Windows.Input;

namespace XamarinForms_Demo.Views
{
    public partial class PersonsPage : ContentPage
    {
        //To be able to implement MVVM this is the itemselectedcommand that binds via event further down.
        public const string ItemSelectedCommandPropertyName = "ItemSelectedCommand";
        public static BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
            propertyName: "ItemSelectedCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(PersonsPage),
            defaultValue: null);

        //To be able to implement MVVM this is the itemselectedcommand that binds via event further down.
        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }
    
        public PersonsPage()
        {

            InitializeComponent(); 
            PopUpEdit.BackgroundColor = new Color(0, 0, 0, 0.7);
            PopUpMessageBox.BackgroundColor = new Color(0, 0, 0, 0.7);
            BindingContext = new PersonsPageViewModel();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            RemoveBinding(ItemSelectedCommandProperty);
            SetBinding(ItemSelectedCommandProperty, new Binding(ItemSelectedCommandPropertyName));
        }

        //PersonListView_ItemSelected
        private void PersonListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var command = ItemSelectedCommand;
            if (command != null && command.CanExecute(e.SelectedItem))
            {
                command.Execute(e.SelectedItem);
            }
        }
    }
}
