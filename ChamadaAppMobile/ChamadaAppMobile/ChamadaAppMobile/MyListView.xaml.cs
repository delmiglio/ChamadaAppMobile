using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ChamadaAppMobile
{
    public partial class MyListView : TabbedPage
    {
        private ViewModel _viewModel = new ViewModel();

        public MyListView()
        {
            InitializeComponent();

            this.BindingContext = this._viewModel;
        }

        public void MenuItemClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var item = menuItem.CommandParameter as ViewModel.Item;

            if (menuItem.IsDestructive)
                _viewModel.Items.Remove(item);
            else
                DisplayAlert("Info", item.Sobrenome, "OK");
        }
    }
}
