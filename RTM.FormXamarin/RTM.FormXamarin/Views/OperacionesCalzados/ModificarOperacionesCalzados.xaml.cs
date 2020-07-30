using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.OperacionesCalzados;
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

namespace RTM.FormXamarin.Views.OperacionesCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarOperacionesCalzados : ContentPage
    {
        public int operacionesCalzadosID;
        public ModificarOperacionesCalzados(int OperacionesCalzadosID)
        {
            InitializeComponent();
            mostrarInformacionOperacionCalzado(OperacionesCalzadosID);
            btnModificarOperacionesCalzados.Clicked += BtnModificarOperacionesCalzados_Clicked;
        }

        private async void BtnModificarOperacionesCalzados_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var OperacionesCalzadosIDV = operacionesCalzadosID;
                var PartNoV = PartNo.Text;
                var CantidadOperacionesV = cantidadOperaciones.Text;
                var DescripcionV = descripcion.Text;
                var CostoOperacionalV = costoOperacional.Text;

                if (string.IsNullOrEmpty(PartNoV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el PartNo de la Operacion del Estilo este ingresado", "Aceptar");
                    PartNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CantidadOperacionesV))
                {
                    await DisplayAlert("Validacion", "Asegurar que la Cantidad de Operaciones del Estilo este ingresado", "Aceptar");
                    cantidadOperaciones.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(DescripcionV))
                {
                    await DisplayAlert("Validacion", "Asegurar que la Descripcion de la Operacion del Estilo este ingresado", "Aceptar");
                    descripcion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CostoOperacionalV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Costo de la Operacion del Estilo este ingresado", "Aceptar");
                    costoOperacional.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var operacionesCalzados = new OperacionesCalzado()
                {
                    OperacionesCalzadosID = OperacionesCalzadosIDV,
                    PartNo=PartNoV,
                    CantidadOperaciones=Convert.ToInt32(CantidadOperacionesV),
                    Descripcion=DescripcionV,
                    CostoOperacional=Convert.ToDecimal(CostoOperacionalV)
                };

                var json = JsonConvert.SerializeObject(operacionesCalzados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/OperacionesCalzados/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Operacion del Estilo se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Operacion del Estilo no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new Ingenieria.Ingenieria());
        }

        private void mostrarInformacionOperacionCalzado(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/OperacionesCalzados/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<OperacionesCalzadosListView>(response.data.ToString());

                    operacionesCalzadosID=listaView.OperacionesCalzadosID;
                    PartNo.Text=listaView.PartNo;
                    cantidadOperaciones.Text = listaView.CantidadOperaciones.ToString();
                    descripcion.Text = listaView.Descripcion;
                    costoOperacional.Text = listaView.CostoOperacional.ToString();

                }

            }
        }
    }
}