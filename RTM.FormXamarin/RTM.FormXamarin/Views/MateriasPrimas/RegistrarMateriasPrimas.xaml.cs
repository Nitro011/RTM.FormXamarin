using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.TiposMateriales;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.MateriasPrimas;
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.MateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarMateriasPrimas : ContentPage
    {
        RegistrarMateriasPrimasViewModel RegistrarMateriasPrimasViewModel;
        public RegistrarMateriasPrimas()
        {
            InitializeComponent();
            BindingContext = this.RegistrarMateriasPrimasViewModel=new RegistrarMateriasPrimasViewModel();
            ListaTiposMateriales();
            btnGuardarMateriasPrimas.Clicked += BtnGuardarMateriasPrimas_Clicked;
        }

        private async void BtnGuardarMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreMateriaPrimalV = Nombre_Materia_Prima.Text;
                var tipoMateriaPrimaV = listaTiposMateriales.SelectedIndex+1;


                if (string.IsNullOrEmpty(nombreMateriaPrimalV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de la Materia Prima", "Aceptar");
                    Nombre_Materia_Prima.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tipoMateriaPrimaV.ToString()))
                {
                    await DisplayAlert("Validacion", "Verfique la seleccion del Tipo de Material", "Aceptar");
                    listaTiposMateriales.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var materiasPrimas = new MateriaPrima()
                {
                    Materia_PrimaID = 0,
                    Nombre_Materia_Prima = nombreMateriaPrimalV,
                    Tipo_MaterialID=tipoMateriaPrimaV

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(materiasPrimas);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/MateriasPrimas/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Materia Prima registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Materia Prima no pudo registrarse correctamente",
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

        private async void ListaTiposMateriales()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposMateriales/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposMaterialesListView>>(response.data.ToString());

                    listaTiposMateriales.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        async void ObtenerRoles(object sender, EventArgs e)
        {
            var pickerRol = listaTiposMateriales.SelectedIndex+1;

            await DisplayAlert("Mostrar Roles", pickerRol.ToString(), "Aceptar");
        }
    }
}