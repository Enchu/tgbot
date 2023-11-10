using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TGBotComp.Handler.Interfaces
{
    public interface ICommandHandler
    {
        Task HandleCommandAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken);
    }
}