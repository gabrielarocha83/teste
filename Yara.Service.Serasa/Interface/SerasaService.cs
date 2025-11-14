using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yara.Service.Serasa.Interface
{
    public abstract class SerasaService<T> where T:class
    {
        public abstract string Header();

        public abstract T Serializar(string retorno);

        public async Task<string> Send(string urlSerasa)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(new Uri(urlSerasa + this.Header()), null);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new SerasaException(string.Format("Ocorreu um erro na comunicação com o Serasa: {0}", String.IsNullOrEmpty(ex.InnerException.Message) ? ex.Message : ex.InnerException.Message));
            }
        }
    }
}