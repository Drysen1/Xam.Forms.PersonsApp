using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_Demo.ViewModels;
using Xamarin.Forms;

namespace XamarinForms_Demo.Views
{
    public partial class CreatePersonPage : ContentPage
    {
        public CreatePersonPage()
        {
            InitializeComponent();
            BindingContext = new CreatePersonViewModel();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

    }
}
