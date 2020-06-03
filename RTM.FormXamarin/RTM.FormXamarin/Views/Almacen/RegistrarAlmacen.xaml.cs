using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Almacen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarAlmacen : ContentPage
    {
        AlmacenViewModel Almacen;
        public RegistrarAlmacen()
        {
            InitializeComponent();
            BindingContext = this.Almacen = new AlmacenViewModel();
        }
    }
}