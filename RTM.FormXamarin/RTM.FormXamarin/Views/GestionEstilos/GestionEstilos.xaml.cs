using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionEstilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionEstilos : ContentPage
    {
        GestionarEstilosViewModel GestionarEstilosViewModel;
        public GestionEstilos()
        {
            InitializeComponent();
            BindingContext = this.GestionarEstilosViewModel=new GestionarEstilosViewModel();

        }
    }
}