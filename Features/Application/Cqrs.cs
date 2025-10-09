namespace api.Features.Application;

public interface IOperation;

public abstract record Command : IOperation;

public abstract record Query<T> : IOperation;
