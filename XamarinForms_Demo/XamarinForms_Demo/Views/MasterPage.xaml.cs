using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_Demo.ViewModels;
using Xamarin.Forms;
using XamarinForms_Demo.Models;

namespace XamarinForms_Demo.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        MasterPageViewModel mpViewModel;
        public MasterPage()
        {
            InitializeComponent();
            InitilizeGUI();       
        }

        private void InitilizeGUI()
        {
            mpViewModel = new MasterPageViewModel();
            MenuListView.ItemsSource = mpViewModel.FillMenuList();
        }

        private void MenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItemModel;
            if (item != null)
            {
                Detail = (Page)Activator.CreateInstance(item.TargetType);
                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
