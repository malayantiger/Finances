using Finances.SharedKernel.Domain.Commands;
using System.Threading.Tasks;

namespace Finances.SharedKernel.Application.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
