using Org.Apache.Http.Impl;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarSuplidores : ContentPage
    {
        public RegistrarSuplidores()
        {
            InitializeComponent();
            btnGuardarSuplidores.Clicked += BtnGuardarSuplidores_Clicked;
        }

        private async void BtnGuardarSuplidores_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreEmpresaV = nombreEmpresa.Text;
                var nombreSuplidorV = nombreSuplidor.Text;
                var telefonoV = telefono.Text;
                var correoElectronicoV = correoElectronico.Text;
                var paisV = pais.Text;
                var ciudadV = ciudad.Text;
                var direccionV = direccion.Text;

                if (string.IsNullOrEmpty(nombreEmpresaV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Nombre de la Empresa", "Aceptar");
                    nombreEmpresa.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(nombreSuplidorV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Nombre del Suplidor", "Aceptar");
                    nombreSuplidor.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(telefonoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el No de Telefono", "Aceptar");
                    telefono.Focus();
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}