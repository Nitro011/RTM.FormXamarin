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
using RTM.FormXamarin.Models.ITEMS;
using Newtonsoft.Json;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.ITEMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarItems : ContentPage
    {
        public RegistrarItems()
        {
            InitializeComponent();
            btnGuardarItem.Clicked += BtnGuardarItem_Clicked;
        }

        private async void BtnGuardarItem_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreItemV = nombreItem.Text;


                if (string.IsNullOrEmpty(nombreItemV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Item", "Aceptar");
                    nombreItem.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var item = new ITEM()
                {
                    ItemID = 0,
                    item = nombreItemV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(item);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Item/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Item registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Item no pudo registrarse correctamente",
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
    }
}