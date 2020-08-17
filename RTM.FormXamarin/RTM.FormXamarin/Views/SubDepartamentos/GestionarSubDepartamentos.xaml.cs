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
using RTM.FormXamarin.Models.SubDepartamentos;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.SubDepartamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarSubDepartamentos : ContentPage
    {
        public GestionarSubDepartamentos()
        {
            InitializeComponent();
            ListaSubDepartamentos();
            buscarSubDepartamento.TextChanged += BuscarSubDepartamento_TextChanged;
            agregarNuevoSubDepartamento.Clicked += AgregarNuevoSubDepartamento_Clicked;
            
        }

        private async void AgregarNuevoSubDepartamento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubDepartamentos.RegistrarSubDepartamentos());
        }

        private void BuscarSubDepartamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarSubDepartamento.Text == "")
            {
                ListaSubDepartamentos();
            }
            else
            {
                string SubDepartamento = buscarSubDepartamento.Text;
                string Departamento = buscarSubDepartamento.Text; 

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/SubDepartamento/BuscarDepartamentosPorDepartamento/{SubDepartamento}/{Departamento}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {

                        var listaView = JsonConvert.DeserializeObject<List<SubDepartamentosListView>>(response.data.ToString());

                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        listaSubDepartamentos.ItemsSource = listaView;
                    }

                }
            }
        }

        private async void ListaSubDepartamentos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/SubDepartamento/SubDepartamentosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<SubDepartamentosListView>>(response.data.ToString());

                    listaSubDepartamentos.ItemsSource = listaView;


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