using Java.Sql;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacionDelEmpleado : ContentPage
    {
        public InformacionDelEmpleado(int id)
        {
            InitializeComponent();
            MostrarInformacionEmplesdo(id);
        }

        private void MostrarInformacionEmplesdo(int id) 
        {

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Empleados/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<UsuariosByID>(response.data.ToString());

                  /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;
                    IdEmpleado.Text = listaView.IdEmpleado.ToString();
                    Nombre.Text = listaView.Nombre;
                    puesto.Text = listaView.puesto;
                    sexo.Text = (listaView.sexo == true) ? "Masculino" : "Femenino";
                    cedula.Text = listaView.cedula;
                    edad.Text = ( DateTime.Now.Year - año).ToString();
                    fecha_nacimiento.Text = (listaView.fecha_nacimiento != null)?DateTime.Parse( listaView.fecha_nacimiento.ToString()).ToString("dd/MM/yyyy"):DateTime.MinValue.ToString();
                    direccion.Text = listaView.direccion;
                    telefono.Text = listaView.telefono;*/
                }

            }

        }
    }
}