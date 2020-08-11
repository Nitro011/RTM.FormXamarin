using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Empleados;
using RTM.FormXamarin.Models.Usuarios;
using RTM.FormXamarin.ViewModels;
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
        InformacionEmpleadoViewModel InformacionEmpleadoViewModel;
        public InformacionDelEmpleado(int id)
        {
            InitializeComponent();
            BindingContext = this.InformacionEmpleadoViewModel = new InformacionEmpleadoViewModel();
            MostrarInformacionEmplesdo(id);
        }

        private void MostrarInformacionEmplesdo(int id) 
        {

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Empleados/EmpleadoPorCodigo/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<EmpleadoPorCodigo>(response.data.ToString());

                    /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                    empleadoID.Text = listaView.empleadoID.ToString();
                    nombresApellidos.Text = listaView.nombresApellidos;
                    sexo.Text = listaView.sexo;
                    cedula.Text = listaView.cedula;
                    puesto.Text = listaView.puesto;
                    Fecha_Nacimiento.Text = listaView.fecha_Nacimiento;
                    edad.Text = listaView.edad.ToString();
                    direccion.Text = listaView.direccion;
                    telefono.Text = listaView.telefono;
                    puesto.Text = listaView.puesto;
                    nombreUsuario.Text = listaView.nombreUsuario;
                    rol.Text = listaView.rol;
                }

            }

        }
    }
}