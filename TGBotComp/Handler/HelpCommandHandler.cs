using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TGBotComp.Handler.Interfaces;

namespace TGBotComp.Handler
{
    public class HelpCommandHandler: ICommandHandler
    {
        public async Task HandleCommandAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: $"/start – начать общение с ботом.\n" +
                      $"/help – вывести справку о доступных командах.\n" +
                      $"/hello – вывести имя и фамилию, email и ссылку на github.\n" +
                      $"/inn – получить наименования и адреса компаний по ИНН.\n" +
                      $"/last – повторить последнее действие бота.", 
                cancellationToken: cancellationToken);
        }
    }
}