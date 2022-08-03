using System.Net.Http.Headers;
using System.Text.Json;
using WEB_API_Curency_Handler.Model;

namespace WEB_API_Curency_Handler.Services
{
    public class GetJsonAndValuteToConvert : IGetJsonService
    {
        private HttpClient httpClient = new HttpClient();
        private const string jsonUri = "https://www.cbr-xml-daily.ru/daily_json.js";
     public GetJsonAndValuteToConvert(){                                     //Добавляю в HttpClient Возможность считывать Json запрос
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
            );


     }
       public async Task<IEnumerable<Valuta>> GetDataValutesAsync()
         {
             var result = await httpClient.GetAsync(jsonUri); //получаем Json из строки 

             if(result.IsSuccessStatusCode) // если все верно и Json нормальный то считываем его, для начала считываем дату, по которой приходит валюта
             {
                 using var resultStream = await result.Content.ReadAsStreamAsync();
                 var dailyCurrency = await JsonSerializer.DeserializeAsync<DailyCurrency>(resultStream); //парсим Json строку
                var CurrenciesResultFromJson = dailyCurrency.Valute
                  .GetType()
                  .GetProperties()
                  .Select(prop =>dailyCurrency.Valute.GetType().GetProperty(prop.Name).GetValue(dailyCurrency.Valute, null) as Valuta
                  );
                return CurrenciesResultFromJson;
            }
             else throw new ArgumentNullException(result.ToString());
         }
         
    }
}
