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
using System.ComponentModel;
using RTM.FormXamarin.Models.DivisionesMateriasPrimas;

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
            ListaDivisionesMateriasPrimas();
            btnGuardarMateriasPrimas.Clicked += BtnGuardarMateriasPrimas_Clicked;
        }

        private async void BtnGuardarMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var PartNoV = PartNo.Text;
                var tipoMateriaPrimaV = (TiposMaterialesListView)listaTiposMateriales.SelectedItem;
                var DescripcionV = Descripcion.Text;
                var UnitV = Unit.Text;
                var CostV = Cost.Text;
                var tipoDivisionesV = (DivisionesMateriasPrimasListView)listaDivisionesMateriasPrimas.SelectedItem;

                if (string.IsNullOrEmpty(PartNoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el PartNo de la Materia Prima", "Aceptar");
                    PartNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tipoMateriaPrimaV.ToString()))
                {
                    await DisplayAlert("Validacion", "Verfique la seleccion del Tipo de Material", "Aceptar");
                    listaTiposMateriales.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(DescripcionV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de la Materia Prima", "Aceptar");
                    Descripcion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CostV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Cost de la Materia Prima", "Aceptar");
                    Cost.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tipoDivisionesV.ToString()))
                {
                    await DisplayAlert("Validacion", "Asegurarse de seleccionar la Division", "Aceptar");
                    listaDivisionesMateriasPrimas.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(UnitV.ToString()))
                {
                    await DisplayAlert("Validacion", "Ingrese el Unit de la Materia Primas", "Aceptar");
                    Unit.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var materiasPrimas = new MateriaPrima()
                {
                    Materia_PrimaID = 0,
                    Tipo_MaterialID = tipoMateriaPrimaV.Tipo_MaterialID,
                    DivisionMateriaPrimaID=tipoDivisionesV.DivisionMateriaPrimaID,
                    PartNo =PartNoV,
                    Descripcion = DescripcionV,
                    Unit=UnitV,
                    Cost=Convert.ToDecimal(CostV)
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
            limpiarCampos();
            await Navigation.PushAsync(new MateriasPrimas.GestionarMateriasPrimas());
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

        private async void ListaDivisionesMateriasPrimas()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/DivisionesMateriasPrima/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<DivisionesMateriasPrimasListView>>(response.data.ToString());

                    listaDivisionesMateriasPrimas.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }


        private void limpiarCampos()
        {
            PartNo.Text = "";
            Descripcion.Text = "";
            Cost.Text = "";
            Unit.Text = "";
        }
    }
}