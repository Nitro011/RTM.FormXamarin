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