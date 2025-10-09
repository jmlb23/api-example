using System.Threading;
using System.Threading.Tasks;

namespace api.Features;

public interface IOperation;

public abstract record Command : IOperation;

public abstract record Query<T> : IOperation;

public interface IHandler<in TCommand, TResult>
{
    Task<TResult> Handle(TCommand command);
}