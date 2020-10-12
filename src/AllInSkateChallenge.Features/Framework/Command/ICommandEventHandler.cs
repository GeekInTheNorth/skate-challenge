using System.Threading.Tasks;

namespace AllInSkateChallenge.Features.Framework.Command
{
    public interface ICommandEventHandler<TCommand> where TCommand : class
    {
        Task HandleAsync(TCommand command, CommandResult result);
    }
}
