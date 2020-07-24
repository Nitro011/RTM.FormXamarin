using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Ingenieria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ingenieria : TabbedPage
    {
        IngenieriaViewModel IngenieriaViewModel;
        public Ingenieria()
        {
            InitializeComponent();
            BindingContext = this.IngenieriaViewModel=new IngenieriaViewModel();
            agregarNuevoBOM.Clicked += AgregarNuevoBOM_Clicked;
        }

        private async void AgregarNuevoBOM_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BOM.RegistrarBOM());
        }
    }
}