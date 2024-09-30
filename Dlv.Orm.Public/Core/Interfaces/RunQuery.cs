namespace Dlv.Orm.Core.Interfaces;

public interface RunQuery<TQueryBuilder, TBindCollector, TSqlConnection>: QueryFragment<TQueryBuilder, TBindCollector> {
    public Task<List<T>> Load<T>(TSqlConnection conn) where T: Queryable<T>;
    public IAsyncEnumerable<T> LoadStream<T>(TSqlConnection conn) where T: Queryable<T>;
    /// <exception cref="InvalidOperationException">The source result is empty</exception>
    public Task<T> GetResult<T>(TSqlConnection conn) where T: Queryable<T>;
    public Task<T?> GetResultOptional<T>(TSqlConnection conn) where T: Queryable<T>;
    public Task<int> Execute(TSqlConnection conn);
    public Task<List<T>> LoadScalars<T>(TSqlConnection conn);
    public IAsyncEnumerable<T> LoadScalarStream<T>(TSqlConnection conn);
    /// <exception cref="InvalidOperationException">The source result is empty</exception>
    public Task<T> GetScalar<T>(TSqlConnection conn);
    public Task<T?> GetScalarOptional<T>(TSqlConnection conn);
}
