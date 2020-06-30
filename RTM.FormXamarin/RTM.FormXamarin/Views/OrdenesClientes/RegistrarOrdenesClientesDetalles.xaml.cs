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
using RTM.FormXamarin.Models.Modelos;
using RTM.FormXamarin.Models.OrdenesClientesDetalles;

namespace RTM.FormXamarin.Views.OrdenesClientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarOrdenesClientesDetalles : ContentPage
    {
        public RegistrarOrdenesClientesDetalles()
        {
            InitializeComponent();
            btnColores.Clicked += BtnColores_Clicked;
            btnDimensiones.Clicked += BtnDimensiones_Clicked;
            btnModelos.Clicked += BtnModelos_Clicked;
            btnTiposCalzados.Clicked += BtnTiposCalzados_Clicked;
            btnGuardarOrdenesClientesDetalles.Clicked += BtnGuardarOrdenesClientesDetalles_Clicked;
        }

        private async void BtnGuardarOrdenesClientesDetalles_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var ordenClienteIDV = "aqui va a ir el id de la ordencliente encabezado";
                var ordenClienteV = ordenCliente.Text;
                var marcasV = pickerMarcas.SelectedIndex;
                var coloresV = btnColores.Text;
                var dimensionesV = btnDimensiones.Text;
                var modelosV = btnModelos.Text;
                var tiposCalzadosV = btnTiposCalzados.Text;

                if (string.IsNullOrEmpty(ordenClienteV))
                {
                    await DisplayAlert("Validacion", "Verifique que tenga la Orden del Cliente", "Aceptar");
                    ordenCliente.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(marcasV.ToString()))
                {
                    await DisplayAlert("Validacion", "Verifique que tenga seleccionado la Marca del Calzado", "Aceptar");
                    pickerMarcas.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(coloresV))
                {
                    await DisplayAlert("Validacion", "Verifique que tenga seleccionado los Colores", "Aceptar");
                    btnColores.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(dimensionesV))
                {
                    await DisplayAlert("Validacion", "Verifique que tenga seleccionado las Dimensiones", "Aceptar");
                    btnDimensiones.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(modelosV))
                {
                    await DisplayAlert("Validacion", "Verifique que tenga seleccionado los Modelos", "Aceptar");
                    btnModelos.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tiposCalzadosV))
                {
                    await DisplayAlert("Validacion", "Verificar que tenga seleccionado los Tipos de Calzados", "Aceptar");
                    btnTiposCalzados.Focus();
                    return;
                }

                //Con HttpClient se realiza la conexion a la base de datos:
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var ordenesClientesDetalles = new OrdenesClientesDetalle()
                {
                    Orden_Cliente_DetalleID = 0,
                    Orden_ClienteID = Convert.ToInt32(ordenClienteIDV),
                    MarcaID = marcasV,
                    Ordenes_Clientes_Detalles_ColoresID = Convert.ToInt32(coloresV),
                    Ordenes_Clientes_Detalles_DimensionesID = Convert.ToInt32(dimensionesV),
                    Ordenes_Clientes_Detalles_ModelosID=Convert.ToInt32(modelosV),
                    Ordenes_Clientes_Detalles_Tipos_CalzadosID=Convert.ToInt32(tiposCalzadosV),

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(ordenesClientesDetalles);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/OrdenesClientesDetalles/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Orden de Cliente Detalle registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Orden de Cliente Detalle no pudo registrarse correctamente",
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

        private async void BtnTiposCalzados_Clicked(object sender, EventArgs e)
        {
            var tipoCalzados = new string[]
            {
              "Zapatos",
              "Botin",
              "Botas",
              "Zapatillas",
              "Tennis",
              "Zandalias",
              "Tacos",
             
             };

            await MaterialDialog.Instance.SelectChoicesAsync(title: "Seleccionar Tipos de Calzados",
                                                              choices: tipoCalzados);
        }

        private async void BtnModelos_Clicked(object sender, EventArgs e)
        {
            var Modelos = new string[]
            {
              "Air Jordan",
              "Prueba",
              "Prueba 1",
              "Bugatti 321345351014",
              "Brabion",
              "Under Amour 2x2",

             };

            await MaterialDialog.Instance.SelectChoicesAsync(title: "Seleccionar Modelos",
                                                              choices: Modelos);
        }

        private async void BtnDimensiones_Clicked(object sender, EventArgs e)
        {
            var dimensiones = new string[]
            {
              "Bebes, USA: 8.5C",
              "Bebes, USA: 9.0C",
              "Bebes, USA: 9.5C",
              "Bebes, USA: 10.0C",
              "Niños, USA: 8.5C",
              "Niños, USA: 9.0C",
              "Niños, USA: 9.5C",
              "Hombres, USA: 6.0",
              "Hombres, USA: 6.5",
              "Hombres, USA: 7.0",
              "Hombres, USA: 7.5",
              "Mujeres, USA: 4.0",
              "Mujeres, USA: 4.5",
              "Mujeres, USA: 5.0",
              "Mujeres, USA: 5.5",

             };

            await MaterialDialog.Instance.SelectChoicesAsync(title: "Seleccionar Dimensiones",
                                                              choices: dimensiones );
        }

        private async void BtnColores_Clicked(object sender, EventArgs e)
        {
            var colores = new string[]
            {
              "Rojo",
              "Azul",
              "Blanco",
              "Verde",
             "Negro",
             "Morado",
             "Naranja",
             "Amarrillo",
             "Rosado",
             "Gris",
             "Aceituna",
             "Naranja Limonsillo",
             "Azul Limonsillo",
             "Fusia"

             };

            await MaterialDialog.Instance.SelectChoicesAsync(title: "Seleccionar los Colores",
                                                              choices: colores);
        }
    }
}