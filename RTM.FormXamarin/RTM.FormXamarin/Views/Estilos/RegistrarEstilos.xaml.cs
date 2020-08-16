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
using RTM.FormXamarin.Models.Marcas;
using RTM.FormXamarin.Models.Colores;
using RTM.FormXamarin.Models.TiposCalzados;
using RTM.FormXamarin.Models.Modelos;
using RTM.FormXamarin.Models.MateriasPrimas;
using RTM.FormXamarin.Models.CategoriasEstilos;
using RTM.FormXamarin.Models.Divisiones;
using RTM.FormXamarin.Models.UnidadesMedidasEstilos;
using RTM.FormXamarin.Models.Estados;
using RTM.FormXamarin.Models.Estilos_MateriasPrimas;
using System.Collections.ObjectModel;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using Android.Graphics;
using System.IO;
using PCLStorage;
using Android.Widget;
using Android.Content;
using RTM.FormXamarin.Helpers;

namespace RTM.FormXamarin.Views.Estilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarEstilos : ContentPage // vamos a ver 
    {
        protected string imageURL = "";
        protected string folderName = "images";
        protected FileData imageData = null;

        public RegistrarEstilos()
        {
            InitializeComponent();
            ListaColores();
            ListaMarcas();
            ListaModelos();
            ListaTiposCalzados();
            ListaMateriasPrimas();
            ListaCategoriasEstilo();
            ListaDivisiones();
            ListaUnidadesMedidasEstilos();
            ListaEstados();
            this.btnGuardarEstilos.Clicked += BtnGuardarEstilos_Clicked;
            uploadFile.Clicked += UploadFile_Clicked;
            
        }

        private async void UploadFile_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile().ContinueWith(a => imageData = a.Result);
                if (fileData == null) return;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

        private async Task SavingImage()
        {
            IFolder mainFolder = FileSystem.Current.LocalStorage;
            mainFolder = await mainFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            await ImageUploaderHelper.SaveImage(imageData.FileName, mainFolder, imageData.DataArray).ContinueWith(res => imageURL = res.Result);
        }

        private async void BtnGuardarEstilos_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            var ListaMateriaPrimas = new List<Estilos_MateriasPrima>();



            try
            {

                var estiloNoVar = estiloNo.Text;
                var pickerMarcasVar = (MarcasListView)pickerMarcas.SelectedItem;
                var divisionesComboBoxVar = (DivisionesListView)divisionesComboBox.SelectedItem;
                var DescripcionVar = Descripcion.Text;
                var costoVar = costo.Text;
                var gananciaVar = ganancia.Text;
                var fechaCreacionVar = fechaCreacion.Date;
                var patternNumVar = patternNum.Text;
                var pickerEstadosVar = (EstadosListView)pickerEstados.SelectedItem;
                var lastVar = last.Text;
                var unidadesMedidasComboBoxVar = (UnidadesMedidasListView)unidadesMedidasComboBox.SelectedItem;
                var ComentarioVar = Comentario.Text;
                var pesoV = peso.Text;
                var coloresComboBoxVar = (ColoresListView)coloresComboBox.SelectedItem;
                var modelosComboBoxVar = (ModelosListView)modelosComboBox.SelectedItem;
                var tiposEstilosComboBoxVar = (TiposCalzadosListView)tiposEstilosComboBox.SelectedItem;
                var categoriaComboBoxVar = (CategoriasEstilosListView)categoriaComboBox.SelectedItem;

                var materialesComboBoxVar = (IList<object>)materialesComboBox.SelectedItem;

                foreach (var item in materialesComboBoxVar)
                {
                    var materiasPrimasId = (MateriasPrimasListView)item;
                    var MateriaPrimaLista = new Estilos_MateriasPrima() { Materia_PrimaID = materiasPrimasId.Materia_PrimaID };
                    ListaMateriaPrimas.Add(MateriaPrimaLista);
                }

                await SavingImage();

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var estilos = new Estilo()
                {

                    MarcaID = pickerMarcasVar.MarcaID,
                    Estilo_No = Convert.ToInt32(estiloNoVar),
                    DivisionID = divisionesComboBoxVar.DivisionID,
                    Descripcion = DescripcionVar,
                    CostoSTD = Convert.ToDecimal(costoVar),
                    Ganancia = Convert.ToDecimal(gananciaVar),
                    FechaCreacion = DateTime.Parse(fechaCreacionVar.ToString()),
                    PattenNo = patternNumVar,
                    EstadoID = pickerEstadosVar.EstadoID,
                    Last = lastVar,
                    UnidadMedidaEstiloID = unidadesMedidasComboBoxVar.UnidadMedidaEstiloID,
                    Comentarios = ComentarioVar,
                    ColorID = coloresComboBoxVar.ColorID,
                    ModeloID = modelosComboBoxVar.ModeloID,
                    Tipo_CalzadoID = tiposEstilosComboBoxVar.Tipo_CalzadoID,
                    CategoriaEstiloID = categoriaComboBoxVar.CategoriaEstiloID,
                    Materias = ListaMateriaPrimas,
                    PesoEstilos = pesoV,
                    ImageURL = imageURL // Imagen OP -- ya prueba eso

                };


                //Convetir a Json
                var json = JsonConvert.SerializeObject(estilos);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/EstilosNuevos/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Estilos registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar").ContinueWith(a => imageURL = ""); // Para borrar la ultima imagen agregada y que se resetee XD
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Estilos no pudo registrarse correctamente",
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

        private async void ListaMarcas()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Marcas/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<MarcasListView>>(response.data.ToString());

                    pickerMarcas.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaModelos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Modelos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ModelosListView>>(response.data.ToString());

                    modelosComboBox.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaTiposCalzados()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposCalzados/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposCalzadosListView>>(response.data.ToString());

                    tiposEstilosComboBox.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaColores()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Colores/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ColoresListView>>(response.data.ToString());

                    coloresComboBox.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaMateriasPrimas()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/MateriasPrimas/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<MateriasPrimasListView>>(response.data.ToString());

                    materialesComboBox.DataSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaCategoriasEstilo()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/CategoriasEstilo/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<CategoriasEstilosListView>>(response.data.ToString());

                    categoriaComboBox.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaDivisiones()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Division/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<DivisionesListView>>(response.data.ToString());

                    divisionesComboBox.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaUnidadesMedidasEstilos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/UnidadesMedidasEstilo/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<UnidadesMedidasListView>>(response.data.ToString());

                    unidadesMedidasComboBox.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaEstados()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Estados/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<EstadosListView>>(response.data.ToString());

                    pickerEstados.ItemsSource = listaView;


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