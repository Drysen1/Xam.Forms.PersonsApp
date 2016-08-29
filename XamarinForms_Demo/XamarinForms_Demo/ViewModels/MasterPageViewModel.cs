using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_Demo.Models;
using XamarinForms_Demo.Views;

namespace XamarinForms_Demo.ViewModels
{
    public class MasterPageViewModel
    {
        public List<MenuPageItemModel> FillMenuList()
        {
            List<MenuPageItemModel> menuItemList = new List<MenuPageItemModel>();
            menuItemList.Add(new MenuPageItemModel
            {
                Title = "Home",
                TargetType = typeof(HomePage)
            });

            menuItemList.Add(new MenuPageItemModel
            {
                Title = "All Persons",
                TargetType = typeof(PersonsPage)
            });

            menuItemList.Add(new MenuPageItemModel
            {
                Title = "Add Person",
                TargetType = typeof(CreatePersonPage)
            });

            menuItemList.Add(new MenuPageItemModel
            {
                Title = "One Person",
                TargetType = typeof(OnePersonPage)
            });

            return menuItemList;
        }

    }
}
