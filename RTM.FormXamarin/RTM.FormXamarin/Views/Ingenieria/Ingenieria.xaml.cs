﻿using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.OperacionesCalzados;
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
using RTM.FormXamarin.Views.OperacionesCalzados;
using RTM.FormXamarin.Models.BOMS;
using Android.OS;
using RTM.FormXamarin.Views.BOM;

namespace RTM.FormXamarin.Views.Ingenieria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ingenieria : TabbedPage
    {
        IngenieriaViewModel IngenieriaViewModel;
        public Ingenieria()
        {
            InitializeComponent();
            BindingContext = this.IngenieriaViewModel=new IngenieriaViewModel();
            agregarNuevoBOM.Clicked += AgregarNuevoBOM_Clicked;
            agregarNuevoOperacionesEstilos.Clicked += AgregarNuevoOperacionesEstilos_Clicked;
            ListaOperacionesCalzados();
            ListaBOMEncabezado();
            listaOperacionesCalzados.ItemSelected += ListaOperacionesCalzados_ItemSelected;
            listaBOM.ItemSelected += ListaBOM_ItemSelected;
            buscarOperacionesEstilos.TextChanged += BuscarOperacionesEstilos_TextChanged;
            buscarBOM.TextChanged += BuscarBOM_TextChanged;
        }

        private async void ListaBOM_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Ver Detalles", "Desea ver el detalle de este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (BOMEncabezadoListView)e.SelectedItem;

                    await Navigation.PushAsync(new InformacionBOMDetalles(item.BOMID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaBOMEncabezado();
            }
        }

        private void BuscarBOM_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarBOM.Text == "")
            {
                ListaBOMEncabezado();
            }
            else
            {
                string PatterN = buscarBOM.Text;
                string Cliente = buscarBOM.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/BOMS/ConsultarBOMEncabezadoPorPatterNCliente/{PatterN}/{Cliente}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<BOMEncabezadoListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaBOM.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private void BuscarOperacionesEstilos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarOperacionesEstilos.Text == "")
            {
                ListaOperacionesCalzados();
            }
            else
            {
                string PartNo = buscarOperacionesEstilos.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/OperacionesCalzados/ConsultarOperacionesCalzadosPorPartNo/{PartNo}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<OperacionesCalzadosListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaOperacionesCalzados.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarNuevoOperacionesEstilos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OperacionesCalzados.RegistrarOperacionesCalzados());
        }

        private async void ListaOperacionesCalzados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (OperacionesCalzadosListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarOperacionesCalzados(item.OperacionesCalzadosID));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaOperacionesCalzados();
            }
        }

        private async void AgregarNuevoBOM_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BOM.RegistrarBOM());
        }

        private async void ListaOperacionesCalzados()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/OperacionesCalzados/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<OperacionesCalzadosListView>>(response.data.ToString());

                    listaOperacionesCalzados.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaBOMEncabezado()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/BOMS/BOMEncabezadosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<BOMEncabezadoListView>>(response.data.ToString());

                    listaBOM.ItemsSource = listaView;


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