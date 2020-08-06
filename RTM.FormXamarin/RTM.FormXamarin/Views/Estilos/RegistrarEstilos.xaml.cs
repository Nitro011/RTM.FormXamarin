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
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.Estilos;

namespace RTM.FormXamarin.Views.Estilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarEstilos : ContentPage
    {
        public RegistrarEstilos()
        {
            InitializeComponent();
            this.btnGuardarEstilos.Clicked += BtnGuardarEstilos_Clicked;
        }

        private async void BtnGuardarEstilos_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {

                var estiloNoVar = estiloNo.Text;
                var pickerMarcasVar = pickerMarcas.SelectedItem;
                var modelosComboBoxVar = modelosComboBox.SelectedItem;
                var tiposEstilosComboBoxVar = tiposEstilosComboBox.SelectedValue;
                var categoriaComboBoxVar = categoriaComboBox.SelectedValue;
                var materialesComboBoxVar = materialesComboBox.SelectedValue;
                var pesoComboBoxVar = pesoComboBox.SelectedValue;
                var lastVar = last.Text;
                var unidadesMedidasComboBoxVar = unidadesMedidasComboBox.SelectedValue;
                var coloresComboBoxVar = coloresComboBox.SelectedValue;
                var divisionesComboBoxVar = divisionesComboBox.SelectedValue;
                var DescripcionVar = Descripcion.Text;
                var ComentarioVar = Comentario.Text;
                var costoVar = costo.Text;
                var gananciaVar = ganancia.Text;
                var fechaCreacionVar = fechaCreacion.Date;
                var pickerEstadosVar = pickerEstados.SelectedItem; 


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var estilos = new Estilo() { };
                //Convetir a Json
                var json = JsonConvert.SerializeObject(estilos);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Usuarios/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Usuario registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Usuario no pudo registrarse correctamente",
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