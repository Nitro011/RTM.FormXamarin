﻿using Newtonsoft.Json;
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
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.Colores;
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.Colores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarColores : ContentPage
    {
        GestionarColoresViewModel GestionarColoresViewModel;
        public GestionarColores()
        {
            InitializeComponent();
            BindingContext = this.GestionarColoresViewModel=new GestionarColoresViewModel();
            agregarNuevosColores.Clicked += AgregarNuevosColores_Clicked;
            ListaColores();
        }

        private async void AgregarNuevosColores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Colores.RegistrarColores());
        }

        private async void ListaColores()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Colores/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ColoresListView>>(response.data.ToString());

                    listaColores.ItemsSource = listaView;


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