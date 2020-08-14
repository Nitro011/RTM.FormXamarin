using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.DivisionesMateriasPrimas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.DivisionesMateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarDivisionesMateriasPrimas : ContentPage
    {
        public int modificarDivisionMateriaPrimaID;
        public ModificarDivisionesMateriasPrimas(int ModificarMateriaPrimaID)
        {
            InitializeComponent();
            btnModificarDivisionMateriaPrima.Clicked += BtnModificarDivisionMateriaPrima_Clicked;
        }

        private async void BtnModificarDivisionMateriaPrima_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var ModificarDivisioMateriaPrimaIDV = modificarDivisionMateriaPrimaID;
                var ModificarDivisionMateriaPrimaV = nombreModificarDivisionMateriaPrima.Text;

                if (string.IsNullOrEmpty(ModificarDivisionMateriaPrimaV))
                {
                    await DisplayAlert("Validacion", "Asegurar que la división esté ingresada", "Aceptar");
                    nombreModificarDivisionMateriaPrima.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var ModificarDivisionMateriaPrima = new DivisionesMateriasPrima()
                {
                    DivisionMateriaPrimaID = modificarDivisionMateriaPrimaID,
                    Division = ModificarDivisionMateriaPrimaV
                };

                var json = JsonConvert.SerializeObject(ModificarDivisionMateriaPrima);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/ModificarDivisionMateriaPrima/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La división se modificó correctamente",
                                   title: "Modificación",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La división no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new DivisionesMateriasPrimas.GestionarDivisionesMateriasPrimas());
        }

        private void mostrarInformacionModificarDivisionMateriaPrima(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/ModificarDivisionMateriaPrima/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<DivisionesMateriasPrimasListView>(response.data.ToString());
                    modificarDivisionMateriaPrimaID = listaView.DivisionMateriaPrimaID;
                    nombreModificarDivisionMateriaPrima.Text = listaView.Division;
                }

            }
        }

    }
    }
