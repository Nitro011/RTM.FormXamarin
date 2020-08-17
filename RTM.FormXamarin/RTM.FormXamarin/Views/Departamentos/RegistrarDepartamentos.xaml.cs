using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.TiposDepartamentos;
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
using RTM.FormXamarin.Models.Departamentos;

namespace RTM.FormXamarin.Views.Departamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarDepartamentos : ContentPage
    {
        public RegistrarDepartamentos()
        {
            InitializeComponent();
            ListaTiposDepartamentos();
            btnGuardarDepartamento.Clicked += BtnGuardarDepartamento_Clicked;
        }

        private async void BtnGuardarDepartamento_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreDepartamentoV = nombreDepartamento.Text;
                var tipoDepartamentoIDV = (TiposDepartamentosListView)TiposDepartamentosComboBox.SelectedItem;


                if (string.IsNullOrEmpty(nombreDepartamentoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Departamento", "Aceptar");
                    nombreDepartamento.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tipoDepartamentoIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Asegurarse de seleccionar el Tipo de Departamento", "Aceptar");
                    TiposDepartamentosComboBox.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var departamentos = new Departamentoss()
                {
                    DepartamentoID = 0,
                    Departamento=nombreDepartamentoV,
                    TipoDepartamentoID = tipoDepartamentoIDV.TipoDepartamentoID,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(departamentos);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Departamento/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Departamento registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Departamento no pudo registrarse correctamente",
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
        }

        private async void ListaTiposDepartamentos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposDepartamentos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposDepartamentosListView>>(response.data.ToString());

                    TiposDepartamentosComboBox.DataSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }
                await Navigation.PushAsync(new Departamentos.GestionarDepartamento());

            }

        }
    }
}