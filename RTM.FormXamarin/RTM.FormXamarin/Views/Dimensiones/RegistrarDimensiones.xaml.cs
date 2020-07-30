using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Dimensiones;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.ViewModels;
using RTM.FormXamarin.Models.CategoriasSizes;
using RTM.FormXamarin.Models.Sizes;

namespace RTM.FormXamarin.Views.Dimensiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarDimensiones : ContentPage
    {
        RegistrarSizesViewModel RegistrarSizesViewModel;
        public RegistrarDimensiones()
        {
            InitializeComponent();
            BindingContext = this.RegistrarSizesViewModel=new RegistrarSizesViewModel();
            btnGuardarDimensiones.Clicked += BtnGuardarDimensiones_Clicked;
            ListaCategoriasSizes();
        }

        private async void BtnGuardarDimensiones_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var USAV = USA.Text;
                var UKV = UK.Text;
                var EUROV = EURO.Text;
                var CMV= CM.Text;
                var CategoriaSizeV = pickerCategoriaSizes.SelectedIndex + 1;


                if (string.IsNullOrEmpty(USAV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Size de USA", "Aceptar");
                    USA.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(UKV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Size de UK", "Aceptar");
                    UK.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(EUROV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Size de EURO", "Aceptar");
                    EURO.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CMV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Size por CM","Aceptar");
                    CM.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CategoriaSizeV.ToString()))
                {
                    await DisplayAlert("Validacion", "Seleccione la Categoria del Sizes", "Aceptar");
                    pickerCategoriaSizes.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var sizes = new Sizes()
                {
                    SizeID = 0,
                    USA= USAV,
                    UK = UKV,
                    EURO=EUROV,
                    CM=CMV,
                    CategoriaSizeID=CategoriaSizeV

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(sizes);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Size/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Sizes registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Sizes no pudo registrarse correctamente",
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
            await Navigation.PushAsync(new Dimensiones.GestionarDimensiones());
        }

        private async void ListaCategoriasSizes()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/CategoriaSize/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<CategoriaSizesListView>>(response.data.ToString());

                    pickerCategoriaSizes.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }
    }
}