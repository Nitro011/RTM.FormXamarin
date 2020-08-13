using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Modelos;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.Clientes;
using RTM.FormXamarin.Models.BOMS;

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
            ListaModelos();
            ListaClientes();
            btnGuardarBOMDetalles.Clicked += BtnGuardarBOMDetalles_Clicked;
            btnGuardarBOM.Clicked += BtnGuardarBOM_Clicked;
            
        }

        private async void BtnGuardarBOM_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var FechaCreacionV = fechaCreacion.Date;
                var PatterNV=PatterN.Text;
                var ModeloIDV=(ModelosListView)modelosComboBox.SelectedItem;
                var ClienteIDV = (ClientesListView)customerComboBox.SelectedItem;

                if (string.IsNullOrEmpty(FechaCreacionV.ToString()))
                {
                    await DisplayAlert("Validacion", "Verificar que este seleccionada la Fecha de Creación", "Aceptar");
                    fechaCreacion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(PatterNV))
                {
                    await DisplayAlert("Validacion", "Ingrese el PatterN", "Aceptar");
                    PatterN.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ModeloIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Verificar que este seleccionado el Modelo", "Aceptar");
                    modelosComboBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ClienteIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Verificar que este seleccionado el Cliente", "Aceptar");
                    customerComboBox.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var BOMS = new BOMEncabezado()
                {
                    BOMID = 0,
                    FechaCreacion = FechaCreacionV,
                    PatterN=PatterNV,
                    ModeloID=ModeloIDV.ModeloID,
                    ClienteID=ClienteIDV.ClienteID
                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(BOMS);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/BOMS/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Encabezado del BOM registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Encabezado del BOM no pudo registrarse correctamente",
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
                                    title: "Error",
                                    acknowledgementText: "Aceptar");
            }
            limpiarCampos();
            await Navigation.PushAsync(new Ingenieria.Ingenieria());
        }

        private async void ListaModelos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Modelos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ModelosListView>>(response.data.ToString());

                    modelosComboBox.DataSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaClientes()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Clientes/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ClientesListView>>(response.data.ToString());

                    customerComboBox.DataSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void BtnGuardarBOMDetalles_Clicked(object sender, EventArgs e)
        {
          await Navigation.PushAsync(new BOM.RegistrarBOMDetalles());
        }

        private void limpiarCampos()
        {
            PatterN.Text = "";
            modelosComboBox.Text = "";
            customerComboBox.Text = "";
        }
    }
}