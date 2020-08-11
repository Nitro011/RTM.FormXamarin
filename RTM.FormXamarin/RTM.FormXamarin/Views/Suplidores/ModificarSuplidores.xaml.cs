using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Suplidores;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;


namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarSuplidores : ContentPage
    {
        public ModificarSuplidores(int id)
        {
            InitializeComponent();
            MostrarInformacionSuplidor(id);

            btnModificarSuplidores.Clicked += BtnModificarSuplidores_Clicked;
        }

        private async void BtnModificarSuplidores_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var id = suplidorID.Text;
                var empresaV = empresa.Text;
                var nombre_SuplidorV = nombre_Suplidor.Text;
                var no_TelefonoV = no_Telefono.Text;
                var correo_ElectronicoV = correo_Electronico.Text;
                var paisV = pais.Text;
                var ciudadV = ciudad.Text;
                var direccionV = direccion.Text;




                if (string.IsNullOrEmpty(empresaV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de la Empresa", "Aceptar");
                    empresa.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(nombre_SuplidorV))
                {
                    await DisplayAlert("Validacion", "Ingresar el Nombre del Suplidor", "Aceptar");
                    nombre_Suplidor.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(no_TelefonoV))
                {
                    await DisplayAlert("Validacion", "Ingreser el Numero de Telefono", "Aceptar");
                    no_Telefono.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(correo_ElectronicoV))
                {
                    await DisplayAlert("Validacion", "Ingresar el Correo Electronico", "Aceptar");
                    correo_Electronico.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(paisV))
                {
                    await DisplayAlert("Validacion", "Ingresar el Pais", "Aceptar");
                    pais.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ciudadV))
                {
                    await DisplayAlert("Validacion", "Ingresar la Ciudad", "Aceptar");
                    ciudad.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(direccionV))
                {
                    await DisplayAlert("Validacion", "Ingresar la Direccion", "Aceptar");
                    direccion.Focus();
                    return;
                }


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var suplidores = new Suplidore()
                {
                    SuplidorID = int.Parse(id),
                    Empresa = empresaV,
                    Nombre_Suplidor = nombre_SuplidorV,
                    No_Telefono=no_TelefonoV,
                    Correo_Electronico=correo_ElectronicoV,
                    Pais=paisV,
                    Ciudad=ciudadV,
                    Direccion = direccionV,

                };

                var json = JsonConvert.SerializeObject(suplidores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Suplidores/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Suplidor se modifico correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Suplidor no pudo modificarse correctamente",
                                  title: "Registro",
                                  acknowledgementText: "Aceptar");

                    }

                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                    title: "Error",
                                    acknowledgementText: "Aceptar");
                }

            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message,
                                    title: ex.Message,
                                    acknowledgementText: "Aceptar");
            }
        }

        private void MostrarInformacionSuplidor(int id)
        {

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Suplidores/SuplidorPorCodigo/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<SuplidorPorCodigo>(response.data.ToString());

                    /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                    suplidorID.Text = listaView.suplidorID.ToString();
                    empresa.Text = listaView.empresa;
                    nombre_Suplidor.Text = listaView.nombre_Suplidor;
                    no_Telefono.Text = listaView.no_Telefono;
                    correo_Electronico.Text = listaView.correo_Electronico;
                    pais.Text = listaView.pais;
                    ciudad.Text = listaView.ciudad;
                    direccion.Text = listaView.direccion;
                }

            }

        }
    }
}