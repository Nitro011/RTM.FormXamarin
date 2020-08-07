using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.BOM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarBOM : ContentPage
    {
        BOMViewModel BOMViewModel;
        public RegistrarBOM()
        {
            InitializeComponent();
            BindingContext = this.BOMViewModel = new BOMViewModel();
        }
    }
}