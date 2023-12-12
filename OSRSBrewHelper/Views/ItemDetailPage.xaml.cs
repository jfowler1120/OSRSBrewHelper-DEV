using OSRSBrewHelper.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace OSRSBrewHelper.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}