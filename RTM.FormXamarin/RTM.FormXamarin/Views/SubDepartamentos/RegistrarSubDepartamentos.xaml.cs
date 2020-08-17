using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Departamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.SubDepartamentos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.SubDepartamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarSubDepartamentos : ContentPage
    {
        public RegistrarSubDepartamentos()
        {
            InitializeComponent();
            ListaDepartamentos();
            btnGuardarSubDepartamento.Clicked += BtnGuardarSubDepartamento_Clicked;
        }

        private async void BtnGuardarSubDepartamento_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreSubDepartamentoV = nombreSubDepartamento.Text;
                var tipoSubDepartamentoIDV = (DepartamentosListView)DepartamentosComboBox.SelectedItem;


                if (string.IsNullOrEmpty(nombreSubDepartamentoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Sub-Departamento", "Aceptar");
                    nombreSubDepartamento.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tipoSubDepartamentoIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Asegurarse de seleccionar el Sub-Departamento", "Aceptar");
                    DepartamentosComboBox.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var SubDepartamentos = new SubDepartamentoss()
                {
                    SubDepartamentoID = 0,
                    SubDepartamento = nombreSubDepartamentoV,
                    DepartamentoID = tipoSubDepartamentoIDV.DepartamentoID,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(SubDepartamentos);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/SubDepartamento/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Sub-Departamento registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Sub-Departamento no pudo registrarse correctamente",
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

        private async void ListaDepartamentos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Departamento/DepartamentosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<DepartamentosListView>>(response.data.ToString());

                    DepartamentosComboBox.DataSource = listaView;


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