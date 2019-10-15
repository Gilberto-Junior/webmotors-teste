using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMotors.Teste.Entities;
using WebMotors.Teste.Entities.Exceptions;

namespace WebMotors.Teste.Services
{
    public class CarsService : IDisposable
    {
        private readonly HttpClient _client;

        public CarsService()
        {
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<IEnumerable<Make>> GetMakesAsync()
        {
            try
            {
                var uri = new Uri("http://desafioonline.webmotors.com.br/api/OnlineChallenge/Make");

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var makes = JsonConvert.DeserializeObject<IEnumerable<Make>>(result);

                    if (makes != null && makes.Count() > 0)
                    {
                        return makes;
                    }

                    throw new CarException("Nenhum carro foi encontrado.");
                }
                else
                {
                    throw new CarException("Não foi possível buscar os carros.");
                }
            }
            catch(CarException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new CarException("Não foi possível buscar as marcas disponíveis na WebMotors.", ex);
            }
        }

        public async Task<IEnumerable<Model>> GetModelsAsync(int id)
        {
            try
            {
                var uri = new Uri($"http://desafioonline.webmotors.com.br/api/OnlineChallenge/Model?MakeID={ id }");

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var models = JsonConvert.DeserializeObject<IEnumerable<Model>>(result);

                    if (models != null && models.Count() > 0)
                    {
                        return models;
                    }

                    throw new CarException("Nenhum modelo foi encontrado.");
                }
                else
                {
                    throw new CarException("Não foi possível buscar os modelos.");
                }
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CarException("Não foi possível buscar os modelos disponíveis na WebMotors.", ex);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehicliesAsync(int id)
        {
            try
            {
                var uri = new Uri($"http://desafioonline.webmotors.com.br/api/OnlineChallenge/Vehicles?Page={ id }");

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(result);

                    if (vehicles != null && vehicles.Count() > 0)
                    {
                        return vehicles;
                    }

                    throw new CarException("Nenhum veículo foi encontrado.");
                }
                else
                {
                    throw new CarException("Não foi possível buscar os veículos.");
                }
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CarException("Não foi possível buscar os veículos disponíveis na WebMotors.", ex);
            }
        }
    }
}
