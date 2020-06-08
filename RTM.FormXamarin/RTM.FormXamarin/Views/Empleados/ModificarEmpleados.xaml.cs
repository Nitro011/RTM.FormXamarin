using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Usuarios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarEmpleados : ContentPage
    {
        public ModificarEmpleados(int id)
        {
            InitializeComponent();
            mostrarInformacionEmpleado(id);
        }

        private void mostrarInformacionEmpleado(int id)
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

                    var listaView = JsonConvert.DeserializeObject<Emple>(response.data.ToString());

                    var año = (listaView.Fecha_Nacimiento != null) ? listaView.Fecha_Nacimiento.Value.Year : DateTime.MinValue.Year;
                    IdEmpleado.Text = listaView.EmpleadoId.ToString();
                    Nombre.Text = listaView.Nombres;
                    Apellido.Text = listaView.Apellidos;
                    Sexo.SelectedIndex = (listaView.Sexo == true) ? 1 : 0;
                    cedula.Text = listaView.Cedula;
                    FN.Date = listaView.Fecha_Nacimiento;
                    Edad.Text = (DateTime.Now.Year - año).ToString();
                    Direccion.Text = listaView.Direccion;
                    telefono.Text = listaView.Telefono;
                    nUsuario.Text = listaView.UserName;
                    Pass.Text = listaView.PasswordHash;
                    Roles.SelectedIndex = listaView.RolID;
                    Email.Text = listaView.Email;
                }

            }
        }
    }
}