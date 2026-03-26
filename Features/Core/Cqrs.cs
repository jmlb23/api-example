using System.Threading;
using System.Threading.Tasks;

namespace api.Features;

public sealed interface IOperation;

public interface Command : IOperation;

public interface Query<T> : IOperation;

public interface IHandler<in TCommand, TResult> where TCommand : IOperation
{
    Task<TResult> Handle(TCommand command);
}
