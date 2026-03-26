using System.Threading;
using System.Threading.Tasks;

namespace api.Features;

public interface IOperation;

public interface Command : IOperation;

public interface Query<T> : IOperation;

public interface IHandler<in TOperation, TResult> where TOperation : IOperation
{
    Task<TResult> Handle(TOperation command);
}
