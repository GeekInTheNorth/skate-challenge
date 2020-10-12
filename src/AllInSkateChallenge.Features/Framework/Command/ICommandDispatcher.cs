using System.Threading.Tasks;

namespace AllInSkateChallenge.Features.Framework.Command
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : class;
    }
}
