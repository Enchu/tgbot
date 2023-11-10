using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TGBotComp.Handler.Interfaces;

namespace TGBotComp.Handler
{
    public class FullCommandHandler: ICommandHandler
    {
        public async Task HandleCommandAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            DotNetEnv.Env.Load();
            
            var apiEgrul = Environment.GetEnvironmentVariable("API_EGR");
            var apiKey = Environment.GetEnvironmentVariable("YOUR_API_KEY");
            
            string inn = "7707083893";
            string apiUrl = $"{apiEgrul}?inn={inn}&key={apiKey}";
                    
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                    
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                            
                    FNS fnsResponse = JsonConvert.DeserializeObject<FNS>(jsonResponse);
                    
                    foreach (var item in fnsResponse.Items)
                    {
                        Console.WriteLine($"ИНН: {item.Inn.INN}");
                        Console.WriteLine($"ОГРН: {item.Inn.Ogrn}");
                        Console.WriteLine($"Наименование: {item.Inn.NaimFullYul}");
                        Console.WriteLine($"Статус: {item.Inn.Status}");
                        Console.WriteLine($"Адрес: {item.Inn.AddressFull}");
                        Console.WriteLine("-------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                }
            }
        }
    }
}