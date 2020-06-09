using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data_Layer
{
    public static class ProcessAPIData
    {
        public static async Task<InrDTO> LoadInrData(string id)
        {
            string url = string.Format("https://eurocomfontyshealthservice.azurewebsites.net/api/inr/{0}/", id);

            using (HttpResponseMessage response = await APICaller.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    InrDTO inrDTO = await response.Content.ReadAsAsync<InrDTO>();

                    return inrDTO;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<MeasurementDTO>> GetMeasurementData(string id)
        {
            string url = string.Format("https://eurocomfontyshealthservice.azurewebsites.net/api/inr/{0}/measurements",
                id);

            using (HttpResponseMessage response = await APICaller.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string measurementDtos = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<MeasurementDTO>>(measurementDtos);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<InrDTO>> GetAllDevices()
        {
            string url = "https://eurocomfontyshealthservice.azurewebsites.net/api/inr";

            using (HttpResponseMessage response = await APICaller.ApiClient.GetAsync(url))
            {
                string inrDtos = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<InrDTO>>(inrDtos);
            }
        }

        public static string GetUserDevice(List<InrDTO> inrDtos, string userid)
        {
            foreach (InrDTO inrDto in inrDtos)
            {
                if (inrDto.client.id == userid)
                {
                    return inrDto.id;
                }
            }

            return null;
        }

        public static MeasurementDTO GetMostRecentDate(List<MeasurementDTO> measurementDtos)
        {
            DateTime mostRecentDateTime = DateTime.MinValue;
            MeasurementDTO mostRecentMeasurement = new MeasurementDTO();
            foreach (MeasurementDTO measurement in measurementDtos)
            {
                if (measurement.measurementDate.Date > mostRecentDateTime)
                {
                    mostRecentDateTime = measurement.measurementDate;
                    mostRecentMeasurement = measurement;
                }
            }

            if (mostRecentMeasurement.measurementSucceeded)
            {
                return mostRecentMeasurement;
            }
            measurementDtos.Remove(mostRecentMeasurement);
            var mostRecent = GetMostRecentDate(measurementDtos);
            return mostRecent;
        }

        public static string GetClient(List<InrDTO> allDevices, string name)
        {
            foreach (InrDTO inrDto in allDevices)
            {
                if (inrDto.client.name == name)
                {
                    var measurement = inrDto;
                    return measurement.id;
                }
            }

            return null;
        }
    }
}