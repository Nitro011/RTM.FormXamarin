using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

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
              "Longitud: 17cm, Anchura: 12cm, Altura: 10cm",
              "Longitud: 13cm, Anchura: 11cm, Altura: 15cm",
              "Longitud: 23cm, Anchura: 21cm, Altura: 25cm",
              "Longitud: 15cm, Anchura: 37cm, Altura: 44cm",
              "Longitud: 25cm, Anchura: 35cm, Altura: 45cm",
              "Longitud: 55cm, Anchura: 65cm, Altura: 75cm",

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