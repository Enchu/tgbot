using System;
using System.Threading;
using System.Threading.Tasks;
using Dadata;
using Telegram.Bot;
using Telegram.Bot.Types;
using TGBotComp.Handler.Interfaces;

namespace TGBotComp.Handler
{
    public class InnCommandHandler: ICommandHandler
    {
        public async Task HandleCommandAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            DotNetEnv.Env.Load();
            var APIDADATA = Environment.GetEnvironmentVariable("API_DADATA");
            
            string[] parts = message.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                string innCommand = parts[0].ToLower();
                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (parts[i].Length == 10)
                            {
                                string innDigits = parts[i];
                                try
                                {
                                    var api = new SuggestClientAsync(APIDADATA);
                                    var response1 = await api.FindParty(innDigits, cancellationToken);
                                    var result = response1.suggestions[0];
                    
                                    if (response1.suggestions[0].data != null)
                                    {
                                        await botClient.SendTextMessageAsync(message.Chat.Id, 
                                            $"Компания с INN: {innDigits} - {result.unrestricted_value}, {result.data.address.unrestricted_value}:", 
                                            cancellationToken: cancellationToken);
                                    }
                                    else
                                    {
                                        await botClient.SendTextMessageAsync(message.Chat.Id, 
                                            $"Компания с INN: {innDigits} не найдена", 
                                            cancellationToken: cancellationToken);
                                    }
                                }
                                catch(Exception ex){}

                               
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, 
                                    $"INN не заполнен", 
                                    cancellationToken: cancellationToken);
                            }
                        }
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, 
                    $"Для того, чтобы заработала команда надо ввести команду /inn и INN компании", 
                    cancellationToken: cancellationToken);
            }

            return;
        }
    }
}