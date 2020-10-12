using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace AllInSkateChallenge.Features.Framework.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class
        {
            Type handler = typeof(ICommandHandler<>);
            Type handlerType = handler.MakeGenericType(command.GetType());

            Type[] concreteTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.GetInterfaces().Contains(handlerType)).ToArray();

            foreach (Type concreteType in concreteTypes)
            {
                var concreteHandler = ActivatorUtilities.CreateInstance(serviceProvider, concreteType) as ICommandHandler<TCommand>;

                if (concreteHandler != null)
                {
                    var commandResult = await concreteHandler.HandleAsync(command);

                    await DispatchEventAsync(command, commandResult);
                }
            }
        }

        private async Task DispatchEventAsync<TCommand>(TCommand command, CommandResult commandResult) where TCommand : class
        {
            Type handler = typeof(ICommandEventHandler<>);
            Type handlerType = handler.MakeGenericType(command.GetType());

            Type[] concreteTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.GetInterfaces().Contains(handlerType)).ToArray();

            foreach (Type concreteType in concreteTypes)
            {
                var concreteHandler = ActivatorUtilities.CreateInstance(serviceProvider, concreteType) as ICommandEventHandler<TCommand>;

                if (concreteHandler != null)
                {
                    await concreteHandler.HandleAsync(command, commandResult);
                }
            }
        }
    }
}
