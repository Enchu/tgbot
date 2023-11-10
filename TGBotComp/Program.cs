using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TGBotComp.Handler;

namespace TGBotComp
{
    class Program
    {
        private static Dictionary<long, string> lastActions = new Dictionary<long, string>();
        
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var APITG = Environment.GetEnvironmentVariable("API_TG");

            var client = new TelegramBotClient(APITG);
            client.StartReceiving(Update, Error);
            
            Console.WriteLine("BOT START");
            
            Console.ReadLine();
            
        }
        
        private static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            if (message == null && message.Text == null)
            {
                message.Text = "/start";
            }
            
            HelpCommandHandler helpCommandHandler = new HelpCommandHandler();
            HelloCommandHandler helloCommandHandler = new HelloCommandHandler();
            InnCommandHandler innCommandHandler = new InnCommandHandler();
            FullCommandHandler fullCommandHandler = new FullCommandHandler();
            
            switch (message.Text.ToLower())
            {
                
                case var messageSW when messageSW.StartsWith(Commands.Start):
                        
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Привет, используй меню для навигации по боту", cancellationToken: cancellationToken);
                        
                    break;
                    
                case var messageSW when messageSW.StartsWith(Commands.Help):
                        
                    await helpCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                    SaveLastAction(message.Chat.Id, $"{message.Text}");
                        
                    break;
                    
                case var messageSW when messageSW.StartsWith(Commands.Hello):
                        
                    await helloCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                    SaveLastAction(message.Chat.Id, $"{message.Text}");
                        
                    break;
                    
                case var messageSW when messageSW.StartsWith(Commands.Inn):
                        
                    await innCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                    SaveLastAction(message.Chat.Id, $"{message.Text}");
                        
                    break;
                case var messageSW when messageSW.StartsWith(Commands.Last):

                    if (lastActions.TryGetValue(message.Chat.Id, out var userLastAction))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Повтор последнего действия: {userLastAction}",
                            cancellationToken: cancellationToken);
                        switch (userLastAction.ToLower())
                        {
                            case var messageSww when messageSww.StartsWith(Commands.Help):
                                await helpCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                                break;
                    
                            case var messageSww when messageSww.StartsWith(Commands.Hello):
                                await helloCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                                break;
                    
                            case var messageSww when messageSww.StartsWith(Commands.Inn):
                                await innCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                                break;
                                
                            case var messageSww when messageSww.StartsWith(Commands.Full):
                                await fullCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                                break;
                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Нет предыдущих действий для повторения.",
                            cancellationToken: cancellationToken);
                    }
                        
                    break;
                    
                case var messageSW when messageSW.StartsWith(Commands.Full):
                        
                    await fullCommandHandler.HandleCommandAsync(message, botClient, cancellationToken);
                    SaveLastAction(message.Chat.Id, $"{message.Text}");
                        
                    break;
                default:
                        
                    await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: $"Похоже вы ввели неправильную команду, нажмите на меню, чтобы посмотерть доступные команды", 
                        cancellationToken: cancellationToken);
                        
                    break;
            }
                
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }
        
        private static void SaveLastAction(long userId, string action)
        {
            lastActions[userId] = action;
        }

        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ошибка при выполнении запроса к Telegram API: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}