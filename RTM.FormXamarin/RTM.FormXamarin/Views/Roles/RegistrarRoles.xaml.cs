using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Roles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarRoles : ContentPage
    {
        RegistrarRolesViewModel Rol;
        public RegistrarRoles()
        {
            InitializeComponent();
            BindingContext = this.Rol = new RegistrarRolesViewModel();
        }
    }
}