using PCLAppConfig;
using RTM.FormXamarin.Models.Estilos;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using RTM.FormXamarin.Models;
using System.Collections.Generic;
using System.Linq;

namespace RTM.FormXamarin.ViewModels
{
    public class EstilosListViewViewModel
    {
        public ObservableCollection<Lol> EstilosListViewsList { get; set; }
        private int estiloId;

        public EstilosListViewViewModel(int estiloId)
        {
            EstilosListViewsList = new ObservableCollection<Lol>();
            this.estiloId = estiloId;
            GetData();
        }

        private void GetData()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/EstilosNuevos/ObtenerEstilosPorEstiloID/{this.estiloId}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var data = JsonConvert.DeserializeObject<List<EstilosListView>>(response.data.ToString()).ElementAtOrDefault(0);
                    foreach(var item in data.Colores1)
                    {
                        EstilosListViewsList.Add(new Lol(item));
                    }
                }
            }
        }
    }

    public class Lol
    {
        public string Color { get; set; }
        public Lol(string color)
        {
            Color = color;
        }
    }
} 
