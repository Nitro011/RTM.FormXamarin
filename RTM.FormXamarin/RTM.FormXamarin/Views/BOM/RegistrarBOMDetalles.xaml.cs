using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.BOMS;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.BOMSDetalles;
using Android.Views;
using System.Text;

namespace RTM.FormXamarin.Views.BOM
{
    public partial class RegistrarBOMDetalles : ContentPage
    {
        public int bomID;
        public decimal usage;
        public decimal cost;
        public RegistrarBOMDetalles()
        {
            InitializeComponent();
            btnGuardarBOMSDetalles.Clicked += BtnGuardarBOMSDetalles_Clicked;
            buscarBOM.TextChanged += BuscarBOM_TextChanged;
            Usage.TextChanged += Usage_TextChanged;
            Costo.TextChanged += Costo_TextChanged;
        }

        private void Costo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Costo.Text))
            {
                cost = Convert.ToDecimal(Costo.Text);
                calcularEXT();
            }
        }

        private void Usage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Usage.Text))
            {
                usage = Convert.ToDecimal(Usage.Text);
                calcularEXT();
            }
        }

        private void calcularEXT()
        {
            if (cost * usage == 0)
            {
                Ext.Text = "0.00";
            }
            else
            {
                decimal resultado = cost * usage;
                Ext.Text = Math.Round(resultado,4).ToString();
            }
        }

        private void BuscarBOM_TextChanged(object sender, TextChangedEventArgs e)
        {
            string PatterN = buscarBOM.Text;

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/BOMS/BuscarBOMEncabezadoPorPatterNCliente/{PatterN}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    if (response.data != null)
                    {
                        var listaView = JsonConvert.DeserializeObject<ObtenerBOMEncabezadoPorPatterNCliente>(response.data.ToString());


                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        bomID = listaView.BOMID;
                        patterN.Text = listaView.PatterN;
                        cliente.Text = listaView.Cliente;
                    }

                }
                else
                {
                    MaterialDialog.Instance.AlertAsync(message: "Error",
                                                       title: "Error",
                                                       acknowledgementText: "Aceptar");
                }

            }
        }


        private async void BtnGuardarBOMSDetalles_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var BOMDIDV = bomID;
                var PartNOV = PartNo.Text;
                var DIEV = DIE.Text;
                var ItemV = Item.Text;
                var DescriptionV = Descripcion.Text;
                var UnitV = Unit.Text;
                var UsageV = Usage.Text;
                var CostV = Costo.Text;
                var ExtV = Ext.Text;

                if (string.IsNullOrEmpty(PartNOV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el PartNo", "Aceptar");
                    PartNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(DIEV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el DIE", "Aceptar");
                    DIE.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ItemV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el Item", "Aceptar");
                    Item.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(DescriptionV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar la Descripcion", "Aceptar");
                    Descripcion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(UnitV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el Unit", "Aceptar");
                    Unit.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(UsageV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el Usage", "Aceptar");
                    Usage.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CostV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el Cost", "Aceptar");
                    Costo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ExtV))
                {
                    await DisplayAlert("Validacion", "Asegurarse de Ingresar el Ext", "Aceptar");
                    Ext.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var BOMSDetalles = new BOMDetalles()
                {
                    BOMDetalleID = 0,
                    BOMID=BOMDIDV,
                    PartNo=PartNOV,
                    DIE=DIEV,
                    Item=ItemV,
                    Description=DescriptionV,
                    Unit=UnitV,
                    Usage=Convert.ToDecimal(UsageV),
                    Cost=Convert.ToDecimal(CostV),
                    Ext=Convert.ToDecimal(ExtV)
                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(BOMSDetalles);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/BOMDetalle/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Detalle del BOM se registro correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Detalle del BOM no pudo registrarse correctamente",
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
            await Navigation.PushAsync(new Ingenieria.Ingenieria());
        }
    }
}
