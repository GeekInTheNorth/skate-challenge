using System.Threading.Tasks;

namespace AllInSkateChallenge.Features.Framework.Command
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<CommandResult> HandleAsync(TCommand command);
    }
}
