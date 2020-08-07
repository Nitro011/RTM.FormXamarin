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
using RTM.FormXamarin.Models.PesosEstilos;
using RTM.FormXamarin.Models.Estados;
using RTM.FormXamarin.Models.Estilos_Colores;
using RTM.FormXamarin.Models.Estilos_CategoriasEstilos;
using RTM.FormXamarin.Models.Estilos_MateriasPrimas;
using RTM.FormXamarin.Models.Estilos_Modelos;
using RTM.FormXamarin.Models.Estilos_PesosEstilos;
using RTM.FormXamarin.Models.Estilos_TiposEstilos;
using System.Collections.ObjectModel;

namespace RTM.FormXamarin.Views.Estilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarEstilos : ContentPage
    {
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
            ListaPesosEstilos();
            ListaEstados();
            this.btnGuardarEstilos.Clicked += BtnGuardarEstilos_Clicked;
        }

        private async void BtnGuardarEstilos_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];
            var ListaColores = new List<Estilos_Colore>();
            var ListaCategorias = new List<Estilos_CategoriasEstilo>();
            var ListaMateriaPrimas = new List<Estilos_MateriasPrima>();
            var ListaModelos = new List<Estilos_Modelo>();
            var ListaPesosEstilos = new List<Estilos_PesosEstilo>();
            var ListaTipoEstilos = new List<Estilos_TiposEstilo>();


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

                var coloresComboBoxVar = (IList<object>)coloresComboBox.SelectedItem;

                foreach (var item in coloresComboBoxVar)
                {
                    var colorId = (ColoresListView)item;
                    var ColoresLista = new Estilos_Colore() { ColorID = colorId.ColorID };
                    ListaColores.Add(ColoresLista);
                }


                var modelosComboBoxVar = (IList<object>)modelosComboBox.SelectedItem;

                foreach (var item in modelosComboBoxVar)
                {
                    var modeloId = (ModelosListView)item;
                    var ModeloLista = new Estilos_Modelo() { ModeloID = modeloId.ModeloID };
                    ListaModelos.Add(ModeloLista);
                }

                var tiposEstilosComboBoxVar = (IList<object>)tiposEstilosComboBox.SelectedItem;

                foreach (var item in tiposEstilosComboBoxVar)
                {
                    var tipoEstilos = (TiposCalzadosListView)item;
                    var TipoLista = new Estilos_TiposEstilo() { Tipo_CalzadoID = tipoEstilos.Tipo_CalzadoID };
                    ListaTipoEstilos.Add(TipoLista);
                }


                var categoriaComboBoxVar = (IList<object>)categoriaComboBox.SelectedItem;

                foreach (var item in categoriaComboBoxVar)
                {
                    var categoriaId = (CategoriasEstilosListView)item;
                    var CategoriaLista = new Estilos_CategoriasEstilo() { CategoriaEstiloID = categoriaId.CategoriaEstiloID };
                    ListaCategorias.Add(CategoriaLista);
                }

                var materialesComboBoxVar = (IList<object>)materialesComboBox.SelectedItem;

                foreach (var item in materialesComboBoxVar)
                {
                    var materiasPrimasId = (MateriasPrimasListView)item;
                    var MateriaPrimaLista = new Estilos_MateriasPrima() { Estilo_MateriaPrimaID = materiasPrimasId.Materia_PrimaID };
                    ListaMateriaPrimas.Add(MateriaPrimaLista);
                }

                var pesoComboBoxVar = (IList<object>)pesoComboBox.SelectedItem;

                foreach (var item in pesoComboBoxVar)
                {
                    var pesoId = (PesosEstilosListView)item;
                    var PesoLista = new Estilos_PesosEstilo() { PesoEstiloID = pesoId.PesoEstiloID };
                    ListaPesosEstilos.Add(PesoLista);
                }



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
                    Colores = ListaColores,
                    Modelos = ListaModelos,
                    TiposEstilos = ListaTipoEstilos,
                    CategoriasEstilos = ListaCategorias,
                    Materias = ListaMateriaPrimas,
                    PesosEstilos = ListaPesosEstilos

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

                    modelosComboBox.DataSource = listaView;


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

                    tiposEstilosComboBox.DataSource = listaView;


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

                    coloresComboBox.DataSource = listaView;


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

                    categoriaComboBox.DataSource = listaView;


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

        private async void ListaPesosEstilos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/PesosEstilos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<PesosEstilosListView>>(response.data.ToString());

                    pesoComboBox.DataSource = listaView;


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