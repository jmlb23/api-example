namespace api.Features;

public interface IOperation;

public abstract record Command : IOperation;

public abstract record Query<T> : IOperation;
