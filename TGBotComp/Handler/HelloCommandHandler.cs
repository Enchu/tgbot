using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TGBotComp.Handler.Interfaces;

namespace TGBotComp.Handler
{
    public class HelloCommandHandler: ICommandHandler
    {
        public async Task HandleCommandAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: $"Германов Алексей\nEmail - aleck.germanov@yandex.ru\nGithub - https://github.com/Enchu", 
                cancellationToken: cancellationToken);
        }
    }
}