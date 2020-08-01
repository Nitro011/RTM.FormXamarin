using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RTM.FormXamarin.Models.Colores;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Colores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarColores : ContentPage
    {
        public int colorID;
        public ModificarColores(int ColorID)
        {
            InitializeComponent();
            mostrarInformacionColores(ColorID);
            btnModificarColor.Clicked += BtnModificarColor_Clicked;
        }

        private async void BtnModificarColor_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var ColorIDV = colorID;
                var ColorV = nombreColor.Text;

                if (string.IsNullOrEmpty(ColorV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Color este ingresado", "Aceptar");
                    nombreColor.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var Colores = new Colore()
                {
                    ColorID = ColorIDV,
                    Color = ColorV
                };

                var json = JsonConvert.SerializeObject(Colores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Colores/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Color se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Color no pudo modificarse correctamente",
                                  title: "Modificacion",
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
            await Navigation.PushAsync(new Colores.GestionarColores());
        }

        private void mostrarInformacionColores(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Colores/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                { 
                    var listaView = JsonConvert.DeserializeObject<ColoresListView>(response.data.ToString());
                    colorID = listaView.ColorID;
                    nombreColor.Text = listaView.Color;
                }

            }
        }
    }
}